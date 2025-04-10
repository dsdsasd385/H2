using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace Puzzle2048
{
    public class PuzzleTile : MonoBehaviour
    {
        #region Pool
        private static ObjectPool<PuzzleTile> _pool;
        private static ObjectPool<PuzzleTile> Pool
        {
            get
            {
                if (_pool == null)
                {
                    _pool = new
                    (
                        createFunc: () =>
                        {
                            var prefab = PuzzleSettings.GetTilePrefab();
                            return Instantiate(prefab);
                        },
                        actionOnGet:     tile => tile.gameObject.SetActive(true),
                        actionOnRelease: tile => tile.gameObject.SetActive(false)
                    );
                }

                return _pool;
            }
        }
        #endregion

        public static void ClearPool()
        {
            Pool.Clear();
        }

        public static PuzzleTile Get(Transform parent, Vector2 sizeDelta, Vector2 anchoredPos, int value)
        {
            var tile = Pool.Get();
            tile.transform.SetParent(parent);
            tile.rect.sizeDelta = sizeDelta;
            tile.imgIcon.rectTransform.sizeDelta = sizeDelta * 0.5f;
            tile.rect.anchoredPosition = anchoredPos;
            tile.Value = value;
            tile.StartIdleFloat();
            return tile;
        }
    
        /******************************************************************************************************************/
        /******************************************************************************************************************/
        
        [SerializeField] private List<Sprite>  coinSprites;
        [SerializeField] private RectTransform rect;
        [SerializeField] private Image         imgTile;
        [SerializeField] private Image         imgIcon;

        private readonly Dictionary<int, int> _valueDict = new()
        {
            { 2, 0 }, { 4, 1 }, { 8, 2 }, { 16, 3 }
        };

        private int _value;
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                imgTile.sprite = coinSprites[_valueDict[value]];
            }
        }

        private Tween _tween;
        private Tween _idleTween;
        
        private void StartIdleFloat()
        {
            _idleTween.Kill();

            _idleTween = rect.DOAnchorPosY(rect.anchoredPosition.y + PuzzleSettings.TileFloatAmp, PuzzleSettings.TileFloatDuration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void Awake()
        {
            imgTile.preserveAspect = true;
        }

        public void FadeIn()
        {
            _tween.Kill();

            _tween = imgTile.DOFade(1f, PuzzleSettings.FadeInDuration)
                .SetEase(PuzzleSettings.FadeInEase);
        }

        public void Move(Vector2 to)
        {
            _tween.Kill();
            _idleTween.Kill();

            _tween = rect.DOAnchorPos(to, PuzzleSettings.MoveDuration)
                .SetEase(PuzzleSettings.MoveEase)
                .OnComplete(StartIdleFloat);
        }

        public void ReleaseOnMerge(Vector2 to)
        {
            _tween.Kill();

            _tween = imgTile.DOFade(0f, PuzzleSettings.FadeInDuration)
                .OnStart(() =>
                {
                    rect.DOAnchorPos(to, PuzzleSettings.MoveDuration)
                        .SetEase(PuzzleSettings.MoveEase);
                })
                .OnComplete(Release);
        }

        public IEnumerator OnRemoved()
        {
            yield return new WaitForSeconds(PuzzleSettings.FadeInDuration);

            yield return rect.DOJumpAnchorPos(rect.anchoredPosition, PuzzleSettings.JumpOnMerge, 1, PuzzleSettings.RemoveDuration)
                .SetEase(PuzzleSettings.RemoveEase)
                .OnStart(() =>
                {
                    imgTile.DOFade(0f, PuzzleSettings.RemoveDuration);
                    rect.DOLocalRotate(Vector3.up, 1200, RotateMode.FastBeyond360)
                        .SetEase(Ease.InOutQuart)
                        .SetSpeedBased(true)
                        .SetLoops(-1, LoopType.Incremental);
                })
                .OnComplete(Release)
                .WaitForCompletion();
        }
        
        public IEnumerator PlayMergeEffect(bool heavy = false)
        {
            _tween.Kill();
            
            float scaleUp = heavy ? 1.4f : 1.2f;
            float duration = PuzzleSettings.MoveDuration;

            Sequence seq = DOTween.Sequence();

            seq.Append(rect.DOScale(scaleUp, duration / 2f).SetEase(Ease.OutQuad));
            seq.Append(rect.DOScale(1f, duration / 2f).SetEase(Ease.InQuad));

            if (heavy)
                seq.Join(rect.DOAnchorPosY(rect.anchoredPosition.y + 20f, duration / 4f)
                    .SetEase(Ease.OutQuad)
                    .SetLoops(2, LoopType.Yoyo));

            _tween = seq;

            yield return _tween.WaitForCompletion();
        }

        private void OnDisable()
        {
            _idleTween.Kill();
            _tween.Kill();
            imgTile.DOKill();
            rect.DOKill();
            transform.localRotation = Quaternion.identity;
            transform.localScale    = Vector3.one;
        }

        private void Release() => Pool.Release(this);
    }
}
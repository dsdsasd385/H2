using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace Puzzle2048
{
    public class PuzzleParticle : MonoBehaviour
    {
        #region Pool
        private static ObjectPool<PuzzleParticle> _pool;
        private static ObjectPool<PuzzleParticle> Pool
        {
            get
            {
                if (_pool == null)
                {
                    _pool = new
                    (
                        createFunc: () =>
                        {
                            var prefab = PuzzleSettings.GetParticlePrefab();
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

        public static void PlayParticle(Transform ui, Vector2 anchoredPos, int mergeValue)
        {
            int count = mergeValue switch
            {
                >= 16 => 12,
                >= 8 => 8,
                _ => 3,
            };

            for (int i = 0; i < count; i++)
            {
                var angle = (360 / count) * i;
                var particle = Pool.Get();
                particle.transform.SetParent(ui, false);
                particle.rect.anchoredPosition = anchoredPos;
                particle.Initialize(angle);
            }
        }
        
        /******************************************************************************************************************/
        /******************************************************************************************************************/

        [SerializeField] private RectTransform rect;
        [SerializeField] private Image         img;

        private void Initialize(float angle)
        {
            float duration = Random.Range(PuzzleSettings.ParticleMinDuration, PuzzleSettings.ParticleMaxDuration);
            float size     = Random.Range(PuzzleSettings.ParticleMinSize, PuzzleSettings.ParticleMaxSize);
            float speed    = Random.Range(PuzzleSettings.ParticleMinSpeed, PuzzleSettings.ParticleMaxSpeed);

            rect.localScale = Vector3.one * size;
            img.color = new Color(1f, 1f, 1f, 1f); // 혹시 누락되어 있을 경우 대비

            float rad = angle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
            Vector2 target = rect.anchoredPosition + direction * speed * duration;

            float spin = Random.Range(-360f, 360f);

            Sequence seq = DOTween.Sequence();
            seq.Join(rect.DOAnchorPos(target, duration).SetEase(Ease.OutQuad));
            seq.Join(rect.DOScale(0f, duration).SetEase(Ease.InQuad));
            seq.Join(img.DOFade(0f, duration * 0.8f).SetEase(Ease.InCubic).From(1));
            seq.Join(rect.DOLocalRotate(new Vector3(0, 0, spin), duration, RotateMode.FastBeyond360)
                .SetEase(Ease.OutQuad));

            seq.OnComplete(() => Pool.Release(this));
        }

    }
}
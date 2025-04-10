using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

        public static void ClearPool()
        {
            Pool.Clear();
        }

        public static void PlayMergeParticle(Transform ui, Vector2 anchoredPos, int mergeValue)
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
                particle.img.sprite = PuzzleSettings.GetParticleSprite(0);
                particle.transform.SetParent(ui, false);
                particle.rect.anchoredPosition = anchoredPos;
                particle.PlaySpread(angle);
            }
        }

        public static void PlayCoinParticle(Transform ui, Vector2 anchoredPos, Vector2 to)
        {
            int count = Random.Range(10, 15);
            
            for (int i = 0; i < count; i++)
            {
                var particle = Pool.Get();
                particle.img.sprite = PuzzleSettings.GetParticleSprite(1);
                particle.transform.SetParent(ui, false);
                particle.rect.anchoredPosition = anchoredPos;
                particle.FlyTo(to);
            }
        }
        
        /******************************************************************************************************************/
        /******************************************************************************************************************/

        [SerializeField] private RectTransform rect;
        [SerializeField] private Image         img;

        private Sequence PlaySpread(float angle)
        {
            float duration = Random.Range(PuzzleSettings.ParticleMinDuration, PuzzleSettings.ParticleMaxDuration);
            float size     = Random.Range(PuzzleSettings.ParticleMinSize, PuzzleSettings.ParticleMaxSize);
            float speed    = Random.Range(PuzzleSettings.ParticleMinSpeed, PuzzleSettings.ParticleMaxSpeed);

            rect.localScale = Vector3.one * size;
            img.color = new Color(1f, 1f, 1f, 1f);

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

            return seq;
        }

        private Sequence FlyTo(Vector2 to)
        {
            float duration = Random.Range(PuzzleSettings.ParticleMinDuration, PuzzleSettings.ParticleMaxDuration);
            float size     = Random.Range(PuzzleSettings.ParticleMinSize, PuzzleSettings.ParticleMaxSize) * 0.75f;
            
            img.color = new Color(1f, 1f, 1f, 1f);

           float spin = Random.Range(-180f, 180f);

            Sequence seq = DOTween.Sequence();

            seq.Join(rect.DOAnchorPos(to, duration).SetEase(Ease.InCubic));
            seq.Join(rect.DOScale(size, duration).SetEase(Ease.OutSine));
            seq.Join(rect.DOLocalRotate(new Vector3(0, 0, spin), duration, RotateMode.FastBeyond360));
            seq.Append(rect.DOScale(0, 0.15f));

            seq.OnComplete(() => Pool.Release(this));

            return seq;
        }

        private void OnDisable()
        {
            rect.DOScale(1, 0);
        }
    }
}
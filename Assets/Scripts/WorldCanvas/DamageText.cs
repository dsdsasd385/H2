using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageText : WorldCanvas
{
    [SerializeField] private TMP_Text txtDamage;
    
    protected override void Initialize(float scale)
    {
        txtDamage.fontSize = scale;
    }

    public void ShowDamage(int damage, float duration = 1.5f)
    {
        txtDamage.text = damage.ToString();

        Cg.DOFade(0f, duration)
            .OnStart(() =>
            {
                transform.DOMoveY(transform.position.y + 0.1f, duration)
                    .SetEase(Ease.Linear)
                    .SetLoops(-1, LoopType.Incremental)
                    .SetSpeedBased(true);
            })
            .OnComplete(() =>
            {
                transform.DOKill();
                Release();
            });
    }
}
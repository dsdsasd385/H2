using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MagicMissile : ActiveSkill
{
    protected override int GetTurnCount() => 2;

    public override IEnumerator OnUseActive(Entity from, List<Entity> targetList)
    {
        var target = targetList[0];

        if (target == null || !target.transform.gameObject.activeInHierarchy)
        {
            Debug.LogWarning("대상 Entity가 유효하지 않습니다. 스킬 실행 취소.");
            yield break;
        }


        var projectile = Projectile.Get<MagicMissileProjectile>(from.transform.position + Vector3.up * 1.5f, target.transform, 2.5f);

        yield return projectile.transform.DOMove(target.transform.position, 10f)
            .SetSpeedBased(true)
            .SetEase(Ease.InQuart)
            .OnComplete(() => projectile.Release(2f))
            .WaitForCompletion();


        float min = from.Status.Power * 0.4f;
        float max = from.Status.Power * 0.6f;

        int damage = Mathf.RoundToInt(Random.Range(min, max));

        target.TakeDamage(damage, target.Status.Defense, from.Status.Critical);

        var text = WorldCanvas.Get<DamageText>(target.transform.position + Vector3.up, 100);

        yield return null;
    }

    public override string GetSkillName() =>
        "매직 미사일";

    public override string GetSkillDescription() =>
        "<b><color=#7B68EE>2턴마다</color></b> 적 1인에게 <b>공격력의 <color=#FF8C00>40~60%</color></b> 피해를 입힙니다.\n";
}

public class BloodMissile : ActiveSkill
{
    protected override int GetTurnCount() => 2;

    public override IEnumerator OnUseActive(Entity from, List<Entity> targetList)
    {
        var target = targetList[0];

        if (target == null || !target.transform.gameObject.activeInHierarchy)
        {
            Debug.LogWarning("대상 Entity가 유효하지 않습니다. 스킬 실행 취소.");
            yield break;
        }

        var projectile = Projectile.Get<BloodMissileProjectile>(from.transform.position + Vector3.up * 1.5f, target.transform, 2.5f);

        yield return projectile.transform.DOMove(target.transform.position, 8f)
             .SetSpeedBased(true)
             .SetEase(Ease.InQuart)
             .OnComplete(() => projectile.Release(2f))
             .WaitForCompletion();



        float min = from.Status.Power * 0.2f;
        float max = from.Status.Power * 0.4f;

        int damage = Mathf.RoundToInt(Random.Range(min, max));

        target.TakeDamage(damage, target.Status.Defense,from.Status.Critical);

        from.Heal(5);

        var text = WorldCanvas.Get<DamageText>(target.transform.position + Vector3.up, 100);
        yield return null;
    }

    public override string GetSkillName() =>
        "블러드 미사일";

    public override string GetSkillDescription() =>
        "<b><color=#8B0000>2턴마다</color></b> 적 1인에게 <b>공격력의 <color=#FF8C00>20~40%</color></b> 피해를 입히고, <b><color=#DC143C>자신의 체력을 5%</color></b> 회복합니다.\n";
}

public class PoisonMissile : ActiveSkill
{
    protected override int GetTurnCount() => 2;

    public override IEnumerator OnUseActive(Entity from, List<Entity> targetList)
    {
        var target = targetList[0];

        if (target == null || !target.transform.gameObject.activeInHierarchy)
        {
            Debug.LogWarning("대상 Entity가 유효하지 않습니다. 스킬 실행 취소.");
            yield break;
        }


        var projectile = Projectile.Get<PoisonMissileProjectile>(from.transform.position + Vector3.up * 1.5f, target.transform, 2.5f);

        yield return projectile.transform.DOMove(target.transform.position, 10f)
             .SetSpeedBased(true)
             .SetEase(Ease.InQuart)
             .OnComplete(() => projectile.Release(2f))
             .WaitForCompletion();


        float min = from.Status.Power * 0.2f;
        float max = from.Status.Power * 0.4f;

        int damage = Mathf.RoundToInt(Random.Range(min, max));

        target.TakeDamage(damage, target.Status.Defense, from.Status.Critical);

        target.TryApplyPoison(0.5f, 2, 3f);

        var text = WorldCanvas.Get<DamageText>(target.transform.position + Vector3.up, 100);
        yield return null;

    }

    public override string GetSkillName() =>
        "포이즌 미사일";

    public override string GetSkillDescription() =>
        "<b><color=#556B2F>2턴마다</color></b> 적 1인에게 <b>공격력의 <color=#FF8C00>20~40%</color></b> 피해를 입히며, <b><color=#32CD32>50%</color></b> 확률로 <b>중독</b>을 겁니다.\n";
}

public class IceMissile : ActiveSkill
{
    protected override int GetTurnCount() => 2;

    public override IEnumerator OnUseActive(Entity from, List<Entity> targetList)
    {
        var target = targetList[0];

        if (target == null || !target.transform.gameObject.activeInHierarchy)
        {
            Debug.LogWarning("대상 Entity가 유효하지 않습니다. 스킬 실행 취소.");
            yield break;
        }

        var projectile = Projectile.Get<IceMissileProjectile>(from.transform.position + Vector3.up * 1.5f, target.transform, 2.5f);
        
        yield return projectile.transform.DOMove(target.transform.position, 10f)
             .SetSpeedBased(true)
             .SetEase(Ease.InQuart)
             .OnComplete(() => projectile.Release(2f))
             .WaitForCompletion();


        float min = from.Status.Power * 0.2f;
        float max = from.Status.Power * 0.4f;

        int damage = Mathf.RoundToInt(Random.Range(min, max));

        target.TakeDamage(damage, target.Status.Defense, from.Status.Critical);

        target.TryApplyFreeze(0.1f); // 10% 확률로 빙결시도

        var text = WorldCanvas.Get<DamageText>(target.transform.position + Vector3.up, 100);
        yield return null;
    }

    public override string GetSkillName() =>
        "아이스 미사일";

    public override string GetSkillDescription() =>
        "<b><color=#4682B4>2턴마다</color></b> 적 1인에게 <b>공격력의 <color=#FF8C00>20~40%</color></b> 피해를 입히며, <b><color=#ADD8E6>10%</color></b> 확률로 <b>빙결</b> 상태로 만듭니다.\n";
}
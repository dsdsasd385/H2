using System.Collections;

public class MagicMissile : ActiveSkill
{
    protected override int GetTurnCount() => 2;
    
    public override IEnumerator OnUseActive()
    {
        yield break;
    }

    public override string GetSkillName() =>
        "매직 미사일";

    public override string GetSkillDescription() =>
        "<b><color=#7B68EE>2턴마다</color></b> 적 1인에게 <b>공격력의 <color=#FF8C00>40~60%</color></b> 피해를 입힙니다.\n";
}

public class BloodMissile : ActiveSkill
{
    protected override int GetTurnCount() => 2;
    
    public override IEnumerator OnUseActive()
    {
        yield break;
    }
    
    public override string GetSkillName() =>
        "블러드 미사일";

    public override string GetSkillDescription() =>
        "<b><color=#8B0000>2턴마다</color></b> 적 1인에게 <b>공격력의 <color=#FF8C00>20~40%</color></b> 피해를 입히고, <b><color=#DC143C>자신의 체력을 5%</color></b> 회복합니다.\n";
}

public class PoisonMissile : ActiveSkill
{
    protected override int GetTurnCount() => 2;
    
    public override IEnumerator OnUseActive()
    {
        yield break;
    }
    
    public override string GetSkillName() =>
        "포이즌 미사일";

    public override string GetSkillDescription() =>
        "<b><color=#556B2F>2턴마다</color></b> 적 1인에게 <b>공격력의 <color=#FF8C00>20~40%</color></b> 피해를 입히며, <b><color=#32CD32>50%</color></b> 확률로 <b>중독</b>을 겁니다.\n";
}

public class IceMissile : ActiveSkill
{
    protected override int GetTurnCount() => 2;
    
    public override IEnumerator OnUseActive()
    {
        yield break;
    }
    
    public override string GetSkillName() =>
        "아이스 미사일";

    public override string GetSkillDescription() =>
        "<b><color=#4682B4>2턴마다</color></b> 적 1인에게 <b>공격력의 <color=#FF8C00>20~40%</color></b> 피해를 입히며, <b><color=#ADD8E6>10%</color></b> 확률로 <b>빙결</b> 상태로 만듭니다.\n";
}
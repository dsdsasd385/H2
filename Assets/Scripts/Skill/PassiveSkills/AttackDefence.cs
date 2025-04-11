public class AttackDefence : PassiveSkill
{
    public override void OnGetPassive()
    {
        
    }

    public override string GetSkillName() =>
        "공격과 방어+";

    public override string GetSkillDescription() =>
        "<b><color=#FFD700>공격력</color></b>과 <b><color=#1E90FF>방어력</color></b>이 <b><color=#00FF00>5%</color></b> 상승합니다.\n";
}

public class AttackDefence2 : PassiveSkill
{
    public override void OnGetPassive()
    {
        
    }

    public override string GetSkillName() =>
        "공격과 방어++";

    public override string GetSkillDescription() =>
        "<b><color=#FFD700>공격력</color></b>과 <b><color=#1E90FF>방어력</color></b>이 <b><color=#00FF00>10%</color></b> 상승합니다.\n";
}
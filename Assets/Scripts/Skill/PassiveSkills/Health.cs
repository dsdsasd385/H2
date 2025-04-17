public class Health : PassiveSkill
{
    public override void OnGetPassive()
    {
        Player.CurrentPlayer.OnHpChanged(10);
    }

    public override string GetSkillName() =>
        "강인한 체력+";

    public override string GetSkillDescription() =>
        "<b><color=#DC143C>최대 체력</color></b>이 <b><color=#00FF00>5%</color></b> 증가하며, <b><color=#00BFFF>현재 체력</color></b>이 <b><color=#00FF00>10%</color></b> 회복됩니다.\n";
}

public class Health2 : PassiveSkill
{
    public override void OnGetPassive()
    {
        Player.CurrentPlayer.OnHpChanged(30);
    }

    public override string GetSkillName() =>
        "강인한 체력++";

    public override string GetSkillDescription() =>
        "<b><color=#DC143C>최대 체력</color></b>이 <b><color=#00FF00>15%</color></b> 증가하며, <b><color=#00BFFF>현재 체력</color></b>이 <b><color=#00FF00>30%</color></b> 회복됩니다.\n";
}
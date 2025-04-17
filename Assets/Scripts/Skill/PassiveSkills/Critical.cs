public class Critical : PassiveSkill
{
    public override void OnGetPassive()
    {
        Player.CurrentPlayer.OnCriticalChanged(5);
    }

    public override string GetSkillName() =>
        "급소 공격+";

    public override string GetSkillDescription() =>
        "<b><color=#FF6347>치명타 확률</color></b>이 <b><color=#00FF00>5%</color></b> 증가합니다.\n";
}

public class Critical2 : PassiveSkill
{
    public override void OnGetPassive()
    {
        Player.CurrentPlayer.OnCriticalChanged(10);
    }

    public override string GetSkillName() =>
        "급소 공격++";

    public override string GetSkillDescription() =>
        "<b><color=#FF6347>치명타 확률</color></b>이 <b><color=#00FF00>10%</color></b> 증가합니다.\n";
}
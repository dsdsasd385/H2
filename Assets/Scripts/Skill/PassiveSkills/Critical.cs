public class Critical : PassiveSkill
{
    public override void OnGetPassive()
    {
        Player.SetCriticalByPercent(5);
    }
}

public class Critical2 : PassiveSkill
{
    public override void OnGetPassive()
    {
        Player.SetCriticalByPercent(10);
    }
}
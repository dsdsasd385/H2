public class Health : PassiveSkill
{
    public override void OnGetPassive()
    {
        Player.SetMaxHpByPercent(5);
        Player.SetHpByPercent(10);
    }
}

public class Health2 : PassiveSkill
{
    public override void OnGetPassive()
    {
        Player.SetMaxHpByPercent(15);
        Player.SetHpByPercent(30);
    }
}
public class AttackDefence : PassiveSkill
{
    public override void OnGetPassive()
    {
        Player.SetAttackPointByPercent(5);
        Player.SetDefenceByPercent(5);
    }
}

public class AttackDefence2 : PassiveSkill
{
    public override void OnGetPassive()
    {
        Player.SetAttackPointByPercent(10);
        Player.SetDefenceByPercent(10);
    }
}
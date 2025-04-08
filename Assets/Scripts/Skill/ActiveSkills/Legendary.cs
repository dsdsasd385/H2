using System.Collections;
using System.Collections.Generic;

public class Rewind : ActiveSkill
{
    protected override int GetTurnCount() => 1;

    public override IEnumerator OnUseActive(Entities.Entity from, List<Entities.Entity> targetList)
    {
        yield break;
    }

    public override string GetSkillName() =>
        "되감기";

    public override string GetSkillDescription() =>
        "<b><color=#FFA500>모든 공격 시</color></b> <b><color=#FFD700>15%</color></b> 확률로 <b>같은 공격을 한 번 더</b> 발동합니다.\n";
}

public class Flair : ActiveSkill
{
    protected override int GetTurnCount() => 2;
    
    public override IEnumerator OnUseActive(Entities.Entity from, List<Entities.Entity> targetList)
    {
        yield break;
    }
    
    public override string GetSkillName() =>
        "플레어";

    public override string GetSkillDescription() =>
        "<b><color=#FF4500>2턴마다</color></b> 적 전체에게 <b>공격력의 <color=#FF1493>200~300%</color></b> 피해를 입히는 강력한 불꽃을 발사합니다.\n";
}

public class Smog : ActiveSkill
{
    protected override int GetTurnCount() => 3;
    
    public override IEnumerator OnUseActive(Entities.Entity from, List<Entities.Entity> targetList)
    {
        yield break;
    }
    
    public override string GetSkillName() =>
        "스모그";

    public override string GetSkillDescription() =>
        "<b><color=#708090>3턴마다</color></b> 적 전체에게 <b><color=#32CD32>100%</color></b> 확률로 <b>중독</b>을 겁니다.\n";
}

public class HeatDeath : ActiveSkill
{
    protected override int GetTurnCount() => -1;
    
    public override IEnumerator OnUseActive(Entities.Entity from, List<Entities.Entity> targetList)
    {
        yield break;
    }

    public override string GetSkillName() =>
        "열죽음";

    public override string GetSkillDescription() =>
        "<b><color=#A52A2A>전투 시작 시</color></b> 적 전체를 <b><color=#00BFFF>빙결</color></b> 상태로 만듭니다.\n";
}
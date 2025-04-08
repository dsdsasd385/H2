using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class Skill
{
    private static Dictionary<SkillGrade, List<Skill>> _skillDB = new()
    {
        { SkillGrade.NORMAL, new(){ new AttackDefence(), new Critical(), new Health(), new MagicMissile() } },
        { SkillGrade.UNIQUE, new(){new AttackDefence2(), new Critical2(), new Health2(), new BloodMissile(), new PoisonMissile(), new IceMissile() } },
        { SkillGrade.LEGENDARY , new(){new Rewind(), new Flair(), new Smog(), new HeatDeath()}}
    };
    
    private static List<Skill> _addedSkillList;
        
    public static void Initialize()
    {
        _addedSkillList = new();
    }

    public static bool HasSkill<T>() where T : Skill
    {
        return _addedSkillList.OfType<T>().Any();
    }

    public static List<Skill> GetAddableSkills(int count = 3)
    {
        var notAddedSkills = _skillDB
            .SelectMany(pair => pair.Value)
            .Where(skill => _addedSkillList.Contains(skill) == false)
            .ToList();

        List<Skill> addables = new();
        
        for (int i = 0; i < count; i++)
        {
            if (notAddedSkills.Count >= 1)
                addables.Add(notAddedSkills.PullRandom());
        }

        return addables;
    }
}

public abstract class PassiveSkill : Skill
{
    protected Entities.Player Player => Entities.Player.Self;
    
    public abstract void OnGetPassive();
}

public abstract class ActiveSkill : Skill
{
    public abstract IEnumerator OnUseActive(Entities.Entity target);
}
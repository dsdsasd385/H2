using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Skill
{
    private static Dictionary<SkillGrade, List<Skill>> _skillDB = new()
    {
        { SkillGrade.NORMAL, new(){ new AttackDefence(), new Critical(), new Health(), new MagicMissile() } },
        { SkillGrade.UNIQUE, new(){new AttackDefence2(), new Critical2(), new Health2(), new BloodMissile(), new PoisonMissile(), new IceMissile() } },
        { SkillGrade.LEGENDARY , new(){new Rewind(), new Flair(), new Smog(), new HeatDeath()}}
    };
    
    private static List<Skill> _addedSkillList;
    private static int _skillDBCount;
        
    public static void Initialize()
    {
        _skillDBCount = _skillDB
            .SelectMany(pair => pair.Value)
            .Count();
        
        Debug.Log($"SKILL COUNT : {_skillDBCount}");
        
        _addedSkillList = new();
    }

    public static bool HasSkill<T>() where T : Skill
    {
        return _addedSkillList.OfType<T>().Any();
    }

    public static List<Skill> GetAddableSkills(int count = 3)
    {
        SkillGrade grade = Random.Range(0, 101) switch
        {
            <= 5  => SkillGrade.LEGENDARY,
            <= 30 => SkillGrade.UNIQUE,
            _     => SkillGrade.NORMAL
        };

        var skills = _skillDB[grade]
            .Where(skill => _addedSkillList.Contains(skill) == false)
            .OrderBy(_ => Random.value)
            .Take(count)
            .ToList();

        if (skills.Count == 0)
        {
            if (_addedSkillList.Count < _skillDBCount)
                return GetAddableSkills(count);

            else
                throw new("Can not add any Skill.");
        }

        return skills;
    }

    public static void AddSkill(Skill skill)
    {
        _addedSkillList.Add(skill);
        
        Debug.Log($"{skill} added!");

        if (skill is PassiveSkill passiveSkill)
            passiveSkill.OnGetPassive();
    }

    public abstract string GetSkillName();
    public abstract string GetSkillDescription();
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
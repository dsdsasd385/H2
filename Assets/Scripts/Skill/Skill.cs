using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Skill
{
    private static Dictionary<SkillGrade, List<Skill>> _skillDB;
    private static List<Skill> _addedSkillList;
    private static int _skillDBCount;
        
    public static void Initialize()
    {
        _skillDB = new()
        {
            { SkillGrade.NORMAL, new(){ new AttackDefence(), new Critical(), new Health(), new MagicMissile() } },
            { SkillGrade.UNIQUE, new(){new AttackDefence2(), new Critical2(), new Health2(), new BloodMissile(), new PoisonMissile(), new IceMissile() } },
            { SkillGrade.LEGENDARY , new(){new Rewind(), new Flair(), new Smog(), new HeatDeath()}}
        };
        
        _skillDBCount = _skillDB
            .SelectMany(pair => pair.Value)
            .Count();
        
        _addedSkillList = new();
        
        Projectile.InitPool();
        WorldCanvas.InitPool();
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
        if (_addedSkillList.Contains(skill))
            return;
        
        _addedSkillList.Add(skill);

        if (skill is PassiveSkill passiveSkill)
            passiveSkill.OnGetPassive();
    }
    
    public static void InitActiveSkills()
    {
        var activeSkills = _addedSkillList
            .OfType<ActiveSkill>()
            .ToList();
        
        activeSkills.ForEach(skill => skill.InitTurnCount());
    }

    public static void ProceedTurn()
    {
        var activeSkills = _addedSkillList
            .OfType<ActiveSkill>()
            .ToList();
        
        activeSkills.ForEach(skill => skill.AddProceedTurn());
    }

    public static IEnumerator UseActiveSkills(Entities.Entity from, List<Entities.Entity> targetList)
    {
        var activeSkills = _addedSkillList
            .OfType<ActiveSkill>()
            .Where(skill => skill.CanUseSkill())
            .ToList();

        foreach (var skill in activeSkills)
            yield return skill.OnUseActive(from, targetList);
        
        foreach (var skill in activeSkills)
            skill.InitTurnCount();
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
    protected abstract int GetTurnCount();

    private int _turnCount;
    private int _proceedTurn;

    public void InitTurnCount()
    {
        _turnCount = GetTurnCount();
        _proceedTurn = 0;
    }

    public void AddProceedTurn()
    {
        if (_turnCount < 0)
            return;
        
        _proceedTurn++;
    }

    public bool CanUseSkill() => _turnCount <= _proceedTurn;
    
    public abstract IEnumerator OnUseActive(Entities.Entity from, List<Entities.Entity> targetList);
}
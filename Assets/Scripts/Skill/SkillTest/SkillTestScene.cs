using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class SkillTestScene : GameScene
{
    [SerializeField] private Transform playerPos;
    [SerializeField] private Transform monsterPos;
    
    private Entities.Entity _player;
    private Entities.Entity _monster;

    private int _turn;
    
    protected override void OnSceneStarted()
    {
        _player = new Entities.Player(1000, 100, 100, 30, 5);
        _player.SetModel(GameObject.CreatePrimitive(PrimitiveType.Cube));
        _player.transform.position = playerPos.position;
        _player.transform.rotation = playerPos.rotation;

        _monster = new Entities.Monster(1000, 100, 100, 30, 5);
        _monster.SetModel(GameObject.CreatePrimitive(PrimitiveType.Cube));
        _monster.transform.position = monsterPos.position;
        _monster.transform.rotation = monsterPos.rotation;
        
        Skill.Initialize();
        Skill.AddSkill(new MagicMissile());
    }

    protected override void OnReleaseScene()
    {
        
    }

    [Button]
    private void Initialize()
    {
        _turn = 0;
        Skill.InitActiveSkills();
    }

    [Button]
    private void Proceed()
    {
        StartCoroutine(BattleCoroutine());
    }

    private IEnumerator BattleCoroutine()
    {
        _turn++;
        
        print($"현재 턴 : {_turn}");
        
        Skill.ProceedTurn();
        
        yield return Skill.UseActiveSkills(_player, new List<Entities.Entity>{_monster});
    }
}
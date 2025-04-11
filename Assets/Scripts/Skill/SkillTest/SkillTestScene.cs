using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class SkillTestScene : GameScene
{
    [SerializeField] private Transform playerPos;
    [SerializeField] private Transform monsterPos;
    
    private Entity _player;
    private Entity _monster;

    private int _turn;
    
    protected override void OnSceneStarted()
    {
        _player = new Player();
        _player.transform = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        _player.transform.position = playerPos.position;
        _player.transform.rotation = playerPos.rotation;

        _monster = new Monster();
        _monster.transform = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
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
        
        yield return Skill.UseActiveSkills(_player, new List<Entity>{_monster});
    }
}
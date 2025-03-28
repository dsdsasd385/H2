using System.Collections;
using UnityEngine;

public class BattleScene : AdditiveScene
{
    public Transform   mapCenter;
    public Transform   playerPos;
    public Transform[] monsterPos;

    public GameObject player;
    public GameObject monster;
    
    protected override IEnumerator OnSceneLoaded()
    {
        player = GameObject.CreatePrimitive(PrimitiveType.Cube);
        player.transform.position = playerPos.position;
        player.transform.rotation = Quaternion.LookRotation(mapCenter.position);
        
        monster = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        monster.transform.position = monsterPos[0].position;
        monster.transform.rotation = Quaternion.LookRotation(mapCenter.position);
        
        yield return null;
    }

    protected override IEnumerator OnUnloadScene()
    {
        yield return null;
    }
}
using System;
using System.Collections;
using UnityEngine;

public class BattleScene : AdditiveScene
{
    public static event Action<BattleScene> SceneLoaded;

    /************************************************************************************************************************/
    /************************************************************************************************************************/

    public Transform   mapCenter;
    public Transform   playerPos;
    public Transform[] monsterPos;

    public GameObject playerPrefabs;
    public GameObject monsterPrefabs;

    public PlayerController player;
    public MonsterController monster;

    public PlayerItem playerItem;

    
    protected override IEnumerator OnSceneLoaded()
    {

        //playerPrefabs = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //player = playerPrefabs.AddComponent<Player>();
        //playerItem = playerPrefabs.AddComponent<PlayerItem>();
        if(player == null)
        player = Instantiate(playerPrefabs).GetComponent<PlayerController>();

        player.transform.position = playerPos.position;
        player.transform.rotation = Quaternion.LookRotation(mapCenter.position);

        //monsterPrefabs = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //monster = monsterPrefabs.AddComponent<Monster>();
        monster = Instantiate(monsterPrefabs).GetComponent<MonsterController>();
        monster.transform.position = monsterPos[0].position;
        monsterPrefabs.transform.rotation = Quaternion.LookRotation(mapCenter.position);

        //Instantiate(player);
        //player.transform.position = playerPos.position;
        //player.transform.rotation = Quaternion.LookRotation(mapCenter.position);

        //Instantiate(monster);
        //monster.transform.position = monsterPos[0].position;
        //monster.transform.rotation = Quaternion.LookRotation(mapCenter.position);

        yield return null;
    }

    protected override IEnumerator OnUnloadScene()
    {
        yield return null;
    }
}
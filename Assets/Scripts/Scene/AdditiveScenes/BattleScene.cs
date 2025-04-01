using System.Collections;
using UnityEngine;

public class BattleScene : AdditiveScene
{
    public Transform   mapCenter;
    public Transform   playerPos;
    public Transform[] monsterPos;

    public GameObject playerPrefabs;
    public GameObject monsterPrefabs;

    public Player player;
    public Monster monster;
    
    protected override IEnumerator OnSceneLoaded()
    {
        playerPrefabs = GameObject.CreatePrimitive(PrimitiveType.Cube);
        player = playerPrefabs.AddComponent<Player>();
        playerPrefabs.transform.position = playerPos.position;
        playerPrefabs.transform.rotation = Quaternion.LookRotation(mapCenter.position);

        monsterPrefabs = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        monster = monsterPrefabs.AddComponent<Monster>();
        monsterPrefabs.transform.position = monsterPos[0].position;
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
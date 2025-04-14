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


    
    protected override IEnumerator OnSceneLoaded()
    {
        if(player == null)
        player = Instantiate(playerPrefabs).GetComponent<PlayerController>();

        player.transform.position = playerPos.position;
        player.transform.rotation = Quaternion.LookRotation(mapCenter.position);

        if (MonsterPoolManager.Instance == null)
        {
            Debug.LogError("MonsterPoolManager.Instance is null! �Ŵ����� ���� �������� �ʾҽ��ϴ�.");
        }

        if (monsterPos == null || monsterPos.Length == 0 || monsterPos[0] == null)
        {
            Debug.LogError("monsterPos[0] �� null�Դϴ�.");
        }

        GameObject monsterObj = MonsterPoolManager.Instance.SpawnMonster(monsterPos[0].position, Quaternion.LookRotation(mapCenter.position));
        monster = monsterObj.GetComponent<MonsterController>();

        monster.Monster.Init();
        //monster = Instantiate(monsterPrefabs).GetComponent<MonsterController>();
        //monster.transform.position = monsterPos[0].position;
        //monsterPrefabs.transform.rotation = Quaternion.LookRotation(mapCenter.position);

        yield return null;
    }

    protected override IEnumerator OnUnloadScene()
    {
        yield return null;
    }
}
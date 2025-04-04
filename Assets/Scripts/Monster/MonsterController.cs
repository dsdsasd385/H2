using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private Monster _monster;
    public Monster Monster => _monster;
    public Status status => _monster.Status; 

    private Player _playerTarget;

    // Start is called before the first frame update
    void Awake()
    {
        _monster = new Monster();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

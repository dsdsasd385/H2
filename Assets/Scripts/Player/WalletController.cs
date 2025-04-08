using System;
using UnityEngine;

public class WalletController : MonoBehaviour
{
    public Wallet wallet { get; private set; }

    [SerializeField] private PlayerUI playerUI; // View와 연결

    private StageRouletteType _stageRouletteTypes;
    private event Action<RouletteResult> roulette;

    private void Awake()
    {
        wallet = new Wallet(); // 일반 C# 객체
        if(playerUI == null)
        {
            playerUI = GetComponent<PlayerUI>();
        }
    }


}

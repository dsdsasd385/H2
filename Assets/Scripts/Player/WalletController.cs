using System;
using UnityEngine;

public class WalletController : MonoBehaviour
{
    public Wallet wallet { get; private set; }

    [SerializeField] private PlayerUI playerUI; // View�� ����

    private StageRouletteType _stageRouletteTypes;
    private event Action<RouletteResult> roulette;

    private void Awake()
    {
        wallet = new Wallet(); // �Ϲ� C# ��ü
        if(playerUI == null)
        {
            playerUI = GetComponent<PlayerUI>();
        }
    }


}

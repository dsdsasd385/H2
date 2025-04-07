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

    public void SetGold(int amount)
    {
        wallet.AddCoin(amount);
    }

    public void RemoveGold(int amount)
    {
        //wallet.LostCoin(amount);
        wallet.AddCoin(amount);
    }

    private void SetWallet(RouletteResult result)
    {
        switch (result.Type)
        {
            case StageRouletteType.PICK_COIN:
                SetGold(result.ChangeValue);
                break;

            case StageRouletteType.LOST_COIN:
                RemoveGold(result.ChangeValue);
                break;
        }
    }

    private void OnEnable()
    {
        Chapter.RouletteResultChangedEvent += SetWallet;
    }
    private void OnDisable()
    {
        Chapter.RouletteResultChangedEvent -= SetWallet;

    }
    private void OnDestroy()
    {
        Chapter.RouletteResultChangedEvent -= SetWallet;

    }

}

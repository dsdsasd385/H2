using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class StageInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text txtStage;
    [SerializeField] private TMP_Text txtCoin;

    private int _beforeCoin;

    private void OnEnable()
    {
        Chapter.StageChangedEvent += OnStageChanged;
        Player.currentPlayer.OnCoinChanged += OnCoinChanged;
        OnCoinChanged(Player.currentPlayer.Coin);
    }

    private void OnDisable()
    {
        Chapter.StageChangedEvent -= OnStageChanged;
        Player.currentPlayer.OnCoinChanged -= OnCoinChanged;
    }

    private void OnCoinChanged(int coin)
    {
        StartCoroutine(txtCoin.ChangeValueAnim(_beforeCoin, coin, 0.5f));
    }

    private void OnStageChanged(int stage, StageType stageType)
    {
        string stageText = $"스테이지 {stage}";
        string type = "";

        switch (stageType)
        {
            case StageType.STATUS_ROULETTE:
                type = "룰렛 이벤트";
                break;
            case StageType.STAGE_EVENT:
                type = "퍼즐";
                break;
            case StageType.BATTLE:
                type = "전투";
                break;
        }

        txtStage.text = $"{stageText} {type}";
        txtStage.DOFade(1f, 0.5f).From(0f);
    }
}
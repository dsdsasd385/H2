using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Slider sldExp;
    [SerializeField] private Slider sldHp;
    [SerializeField] private TMP_Text txtLevel;
    [SerializeField] private TMP_Text txtHp;
    [SerializeField] private TMP_Text txtPower;
    [SerializeField] private TMP_Text txtDefence;
    
    private StagePlayUI _stagePlayUI;
    
    public void Initialize()
    {
        Player.CurrentPlayer.Status.OnHpChange += RefreshHp;
        Player.CurrentPlayer.Status.OnPowerChange += RefreshPower;
        Player.CurrentPlayer.Status.OnDefenseChange += RefreshDefence;
        Player.CurrentPlayer.OnChangeExp += RefreshExp;
        Player.CurrentPlayer.OnLevelUp += RefreshLevel;

        var player = Player.CurrentPlayer;
        
        RefreshLevel(player.Level);
        RefreshExp(player.Exp, player.NeedExp);
        RefreshPower(player.Status.Power);
        RefreshDefence(player.Status.Defense);
        RefreshHp(player.Status.Hp, player.Status.MaxHp);
    }

    private void OnDestroy()
    {
        if (Player.CurrentPlayer != null)
        {
            Player.CurrentPlayer.Status.OnHpChange -= RefreshHp;
            Player.CurrentPlayer.Status.OnPowerChange -= RefreshPower;
            Player.CurrentPlayer.Status.OnDefenseChange -= RefreshDefence;
            Player.CurrentPlayer.OnChangeExp -= RefreshExp;
            Player.CurrentPlayer.OnLevelUp -= RefreshLevel;
        }
    }

    private void RefreshLevel(int level)
    {
        txtLevel.text = $"LV {level}";
    }

    private void RefreshExp(int exp, int needExp)
    {
        print($"NEED : {needExp} EXP : {exp}");
        
        sldExp.maxValue = needExp;

        DOTween.To(() => sldExp.value, x => sldExp.value = x, exp, 0.5f);
    }

    private void RefreshHp(int hp, int maxHp)
    {
        sldHp.maxValue = maxHp;

        DOTween.To(() => txtHp.text, x => txtHp.text = x, $"{hp}/{maxHp}", 0.5f);
        DOTween.To(() => sldHp.value, x => sldHp.value = x, hp, 0.5f);
    }

    private void RefreshPower(float power)
    {
        txtPower.text = $"{power:F1}";
    }

    private void RefreshDefence(float defence)
    {
        txtDefence.text = $"{defence:F1}";
    }
}

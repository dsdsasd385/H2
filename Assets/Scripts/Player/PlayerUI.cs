using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private TMP_Text _hpTxt;
    private TMP_Text _leveltxt;
    private TMP_Text _powerTxt;
    private TMP_Text _defenseTxt;
    private TMP_Text _coinTxt;

    private Slider _exp;
    private Slider _hpVar;

    private StagePlayUI _stagePlayUI;

    //private void Start()
    //{
    //    _stagePlayUI = FindObjectOfType<StagePlayUI>();
    //    if (_stagePlayUI != null)
    //    {
    //        _leveltxt = _stagePlayUI.GetText("Level");
    //        _powerTxt = _stagePlayUI.GetText("Power");
    //        _defenseTxt = _stagePlayUI.GetText("Defense");
    //        _coinTxt = _stagePlayUI.GetText("Coin");
    //    }
    //}

    public void SetText(StagePlayUI stagePlayUI)
    {
        _stagePlayUI = stagePlayUI;
        if (_stagePlayUI != null)
        {
            _hpTxt = _stagePlayUI.GetText("Hp");
            _leveltxt = _stagePlayUI.GetText("Level");
            _powerTxt = _stagePlayUI.GetText("Power");
            _defenseTxt = _stagePlayUI.GetText("Defense");
            _coinTxt = _stagePlayUI.GetText("Coin");
        }
    }
    public void SetHpVar(int hp)
    {        
        _hpTxt.text = hp.ToString("F2");
        //_hpVar.value = hp;
        Debug.Log("HpText가 변경되었습니다.");
        // 슬라이드 애니메이션 (스르륵 움직이게)
    }
    public void SetCriticalText(float critical)
    {

    }
    public void SetPowerText(float power)
    {        
        _powerTxt.text = power.ToString("F2");
        Debug.Log("powerText가 변경되었습니다.");
    }
    public void SetDefenseText(float defense)
    {
        _defenseTxt.text = defense.ToString("F2");
        Debug.Log("DefenseText 변경되었습니다.");

    }
    public void SetSpeedText(float speed)
    {

    }

    public void SetCoinText(int coin)
    {
        // CoinText에 할당
        _coinTxt.text = coin.ToString();
    }

    public void SetExp(int exp)
    {
        // exp 활용
    }

    public void SetLevel(int level)
    {
        _leveltxt.text = level.ToString();
    }


}

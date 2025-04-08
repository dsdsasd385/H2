using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _hpTxt;
    [SerializeField] private TMP_Text _leveltxt;
    [SerializeField] private TMP_Text _powerTxt;
    [SerializeField] private TMP_Text _defenseTxt;
    [SerializeField] private TMP_Text _coinTxt;

    private Slider _exp;
    private Slider _hpVar;
    

    private StagePlayUI _stagePlayUI;


    public void Initialize()
    {
        // 이벤트 구독
        Player.currentPlayer.Status.OnHpChange += SetHpVar;
        Player.currentPlayer.Status.OnPowerChange += SetPowerText;
        Player.currentPlayer.Status.OnDefenseChange += SetDefenseText;
        Player.currentPlayer.OnChangeExp += SetExp;
        Player.currentPlayer.OnLevelUp += SetLevel;
        Player.currentPlayer.OnCoinChanged += SetCoinText;
        //    Player.OnPlayAttackAnimation += _playerAni.PlayAttackAni;
        //    Player.OnPlayDamagedAnimation += _playerAni.PlayDamagedAni;
        //    Player.OnPlayDieAnimation += _playerAni.PlayDieAni;
        //
        }

        private void OnDestroy()
        {
        if(Player.currentPlayer != null)
        {
            // 이벤트 구독해제
            Player.currentPlayer.Status.OnHpChange -= SetHpVar;
            Player.currentPlayer.Status.OnPowerChange -= SetPowerText;
            Player.currentPlayer.Status.OnDefenseChange -= SetDefenseText;
            Player.currentPlayer.OnChangeExp -= SetExp;
            Player.currentPlayer.OnLevelUp -= SetLevel;
        }
    }

        private void SetHpVar(int hp)
        {
            _hpTxt.text = hp.ToString("F2");
            //_hpVar.value = hp;
            Debug.Log("HpText가 변경되었습니다.");
            // 슬라이드 애니메이션 (스르륵 움직이게)
        }
        private void SetCriticalText(float critical)
        {

        }
        private void SetPowerText(float power)
        {
            _powerTxt.text = power.ToString("F2");
            Debug.Log("powerText가 변경되었습니다.");
        }
        private void SetDefenseText(float defense)
        {
            _defenseTxt.text = defense.ToString("F2");
            Debug.Log("DefenseText 변경되었습니다.");

        }
        private void SetSpeedText(float speed)
        {

        }

        private void SetCoinText(int coin)
        {
            // CoinText에 할당
            _coinTxt.text = coin.ToString();
        }

        private void SetExp(int exp)
        {
            // exp 활용
        }

        private void SetLevel(int level)
        {
            _leveltxt.text = level.ToString();
        }


    }

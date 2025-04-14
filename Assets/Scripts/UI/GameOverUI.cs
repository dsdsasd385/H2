using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : UI
{
    [SerializeField] private TMP_Text txtMsg;
    [SerializeField] private Button   btnExit;

    private void Awake()
    {
        btnExit.onClick.AddListener(OnExitPressed);
    }

    private void OnExitPressed()
    {
        Dim.FadeIn(1.5f, 0f, ()=>
        {
            Close();
            GameScene.LoadScene("OutGameScene");
        });
    }

    public void SetMsg(string msg)
    {
        txtMsg.text = msg;
    }
}
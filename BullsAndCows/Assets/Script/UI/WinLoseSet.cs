using UnityEngine.UI;
using UnityEngine;

public class WinLoseSet : Set {

    [SerializeField]
    private Text    HeaderText;
    [SerializeField]
    private Image   HeaderBackground;
    [SerializeField]
    private Sprite  WinColor,
                    LoseColor;
    [SerializeField]
    private GameObject ButtonGroup; 


    GameManager GM;


    private void OnEnable()
    {
        GetGameResult();
    }

    private void GetGameResult()
    {
        bool IsGameWon = GameManager.Inst.BCGame.IsGameWon();
        if (IsGameWon)
        {
            HeaderText.text = "You Win!";
            HeaderBackground.sprite = WinColor;
        }
        else
        {
            HeaderText.text = "You Lose";
            HeaderBackground.sprite = LoseColor;
        }

        SoundManager.Inst.PlayWinLose(IsGameWon);
    }




    public void PlayAgain()
    {
        GameManager.Inst.PlayGame();
        UISetManager.Inst.GetGameSet();
    }

    public void Quit()
    {
        UISetManager.Inst.GetMainMenuSet();
    }
}

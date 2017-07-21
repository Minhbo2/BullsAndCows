using UnityEngine.UI;
using UnityEngine;

public class WinLoseSet : Set {

    [SerializeField]
    private Text    HeaderText;
    [SerializeField]
    private Image   HeaderBackground;
    [SerializeField]
    private Color   WinColor,
                    LoseColor;


    GameManager GM;


    private void OnEnable()
    {
        GetGameState();
    }

    private void GetGameState()
    {
        GM = GameManager.Inst;
        bool IsGameWon = GM.BCGame.IsGameWon();
        Debug.Log(IsGameWon);

        if (IsGameWon)
        {
            HeaderText.text = "You win!";
            HeaderBackground.color = WinColor;
        }
        else
        {
            HeaderText.text = "You lose!";
            HeaderBackground.color = LoseColor;
        }

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

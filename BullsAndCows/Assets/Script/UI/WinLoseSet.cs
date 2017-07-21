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
        GetGameResult();
    }

    private void GetGameResult()
    {

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

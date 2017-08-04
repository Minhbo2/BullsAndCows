using UnityEngine.UI;
using UnityEngine;

public class SummarySet : Set {

    [SerializeField]
    private Text SumText;

    private void OnEnable()
    {
        GetGameResult();
    }

    private void GetGameResult()
    {
        int RoundIndex = GameManager.Inst.BCGame.GetRound();
        SumText.text = "Rounds Completed: " + (RoundIndex + 1);
    }


    public void PlayAgain()
    {
        GameManager.Inst.ResetTimeRound();
        GameManager.Inst.PlayGame();
        UISetManager.Inst.GetGameSet();
    }

    public void Quit()
    {
        GameManager.Inst.ResetTimeRound();
        UISetManager.Inst.GetMainMenuSet();
    }
}

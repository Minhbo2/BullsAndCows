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
        int RoundIndex = Game.Inst.RoundIndex;
        SumText.text = "Rounds Completed: " + RoundIndex;
    }


    public void PlayAgain()
    {
        Game.Inst.GameIsLoading = true;
    }

    public void Quit()
    {
        UISetManager.Inst.GetMainMenuSet();
    }
}

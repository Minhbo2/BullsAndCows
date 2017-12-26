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
        int RoundIndex = Game.Inst.BCGame.GetRound();
        SumText.text = "Rounds Completed: " + RoundIndex;
        Data NewData = Game.Inst.NewData;
        if (RoundIndex > NewData.Round)
        {
            NewData = new Data(RoundIndex, Game.Inst.IsTutorialComplete);
            Game.Inst.HighestRoundCompleted = RoundIndex;
            SaveData.SavingData(NewData, "bcgame.dat");
        }
    }


    public void PlayAgain()
    {
        Game.Inst.GameIsLoading = true;
    }

    public void Quit()
    {
        Game.Inst.uiSetManager.NextActiveSet("Main Menu");
    }
}

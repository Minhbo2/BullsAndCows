using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Inst { get { return m_Inst; } }
    static GameManager m_Inst;

    public BullsCowsGame BCGame = new BullsCowsGame();

    public bool IsTutorialComplete = false;
    
    [Range(1, 3)] // must be between 1-3
    public int DifficultyIndex = 2;



	void Start () {
        if (m_Inst == null)
            m_Inst = this;
    }



    // When game is ready to play
    public void PlayGame()
    {
        BCGame.Reset(DifficultyIndex);
    }



    // get a status for the guess 
    public string ValidateGuess(string Guess)
    {
        string Message = "";
        EGuessState CurrentState = EGuessState.Invalid_Status;
        CurrentState = BCGame.CheckGuessValidity(Guess);

        if (CurrentState != EGuessState.OK)
        {
            switch (CurrentState)
            {
                case EGuessState.Not_Isogram:
                    Message = "Please enter an isogram word.";
                    break;
                case EGuessState.Not_Lowercase:
                    Message = "Please enter a lower case word.";
                    break;
                case EGuessState.Wrong_Length:
                    Message = "Please enter a " + BCGame.GetWordLength() + " letters word.";
                    break;
            }
            SoundManager.Inst.SetAudio(SoundManager.Inst.SLose);
            UISetManager.Inst.IPSet.ErrorMessageText(Message);
            return Message;
        }
        else
        {
            UpdateGameState(Guess);
            return null;
        }
    }




    bool UpdateGameState(string Guess)
    {
        BCGame.AddBullAndCow(Guess);
        UISetManager.Inst.IPSet.OutputResult(BCGame.GetBulls(), BCGame.GetCows());
        BCGame.AddToCurrentTry();

        int CurrentTry = BCGame.GetCurrentTry();
        int MaxTries = BCGame.GetMaxTry();
        if (BCGame.IsGameWon() || CurrentTry >= MaxTries)
            UISetManager.Inst.GetWinLoseSet();
        return true;
    }
}

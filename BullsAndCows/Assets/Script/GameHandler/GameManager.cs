using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Inst { get { return m_Inst; } }
    static GameManager m_Inst;

    public BullsCowsGame BCGame = new BullsCowsGame();


	void Start () {
        if (m_Inst == null)
            m_Inst = this;

        PlayGame();
	}


    // Intro Text
    public string PrintIntro()
    {
        string IntroMessage = "Bulls and Cows is a word guessing game.\n";
        string IntroMessage2 = "A Bull for every letter in the right position.\n";
        string IntroMessage3 = "A Cow for every letter not in the right position.\n";
        string IntroMessage4 = "A word must be an ISOGRAM. \n";

        string CompleteIntroMessage = IntroMessage + IntroMessage2 + IntroMessage3 + IntroMessage4;
        return CompleteIntroMessage;
    }





    // When game is ready to play
    public void PlayGame()
    {
        BCGame.Reset();
    }




    // get guess from player and validating it
    public void SubmitGuess(string Guess)
    {
        int MaxTries = BCGame.GetMaxTry();
        bool IsGameWon = BCGame.IsGameWon();

        if (!IsGameWon && BCGame.GetCurrentTry() < MaxTries)
            Guess = ValidateGuess(Guess);
    }



    // get a status for the guess 
    string ValidateGuess(string Guess)
    {
        string Message = "";
        EGuessState CurrentState = EGuessState.Invalid_Status;
        CurrentState = BCGame.CheckGuessValidity(Guess);

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
            case EGuessState.OK:
                BCGame.AddBullAndCow(Guess);
                CheckGameState();
                break;
        }

        InteractivePanelsSet IPSet = UISetManager.Inst.IPSet;
        IPSet.ErrorMessageText(Message);
        return Message;
    }




    bool CheckGameState()
    {
        InteractivePanelsSet IPSet = UISetManager.Inst.IPSet;
        IPSet.OutputResult(BCGame.GetBulls(), BCGame.GetCows());
        return true;
    }



    // print game summary/player condition
    string PrintSummary()
    {
        string SummaryText;
        if(BCGame.IsGameWon())
            SummaryText = "You win!";
        else if(BCGame.GetCurrentTry() >= BCGame.GetMaxTry())
            SummaryText = "You lose!";
        else
            SummaryText = "";

        string AskToPlayAgain = SummaryText + "\n Do you want to play again?";
        return AskToPlayAgain;
    }



    // check to see if player want to start again
    bool AskPlayAgain()
    {
        // TODO: chekc if player want to play again or quit
        // if yes
            // reset the game
        // else 
            // quit
        return true;
    }
}

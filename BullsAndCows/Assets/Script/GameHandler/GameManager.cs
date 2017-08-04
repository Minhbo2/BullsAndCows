using UnityEngine;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {
    public static GameManager Inst { get { return m_Inst; } }
    static GameManager m_Inst;

    public BullsCowsGame BCGame = new BullsCowsGame();

    private bool IsRoundStarted = false;

    public bool IsTutorialComplete = false;

    public float LevelTime;
    private float BonusTime = 30f;


    [Range(1, 3)] // must be between 1-3
    public int DifficultyIndex = 2;



	void Start () {
        if (m_Inst == null)
            m_Inst = this;
    }




    private void Update()
    {
        if(IsRoundStarted)
        {
            LevelTime -= Time.deltaTime;
            if (BCGame.GetCurrentTry() >= BCGame.GetMaxTry() || LevelTime < 0)
            {
                IsRoundStarted = false;
                UISetManager.Inst.GetSummarySet();     
            }
        }
    }



    public void ResetTimeRound()
    {
        LevelTime = 300;
    }



    // When game is ready to play
    public void PlayGame()
    {
        BCGame.Reset(DifficultyIndex);
        IsRoundStarted = true;
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
            UISetManager.Inst.GPSet.ErrorMessageText(Message);
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
        int RoundIndex = BCGame.GetRound(); // get round index before adding
        BCGame.AddBullAndCow(Guess);
        UISetManager.Inst.GPSet.OutputResult(BCGame.GetBulls(), BCGame.GetCows());
        BCGame.AddToCurrentTry();

        if (RoundIndex < BCGame.GetRound()) // compare after, if changes then reset
        {
            UISetManager.Inst.GPSet.Pausing(UISetManager.Inst.GPSet.ContinuePanel);
            LevelTime += BonusTime;
            PlayGame();
        }

        return true;
    }
}

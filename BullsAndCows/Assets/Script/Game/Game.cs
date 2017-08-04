﻿using UnityEngine;
using System.Collections.Generic;


public enum GameState
{
    INIT,
    WAITING,
    LOADING,
    RUNNING
}



public class Game : MonoBehaviour {

    public GameState CurrentState = GameState.INIT;

    public bool GameIsWaiting,
                GameIsLoading,
                GameIsRunning;


    public static Game Inst {get {return m_Inst;}}
    private static Game m_Inst;

    private UISetManager UISetManagerScreen;
    private SoundManager SoundManagerObj;

    public BullsCowsGame BCGame = new BullsCowsGame();

    private bool IsRoundStarted    = false;
    public bool IsTutorialComplete = false;

    public float TimeToQuit;
    public float LevelTime;
    private float BonusTime = 30f;

    [Range(1, 3)] 
    public int DifficultyIndex;

    public int RoundIndex = 1;



    void Start()
    {
        if (m_Inst == null)
            m_Inst = this;
    }



	void Update () {
        switch (CurrentState)
        {
            case GameState.INIT:
                UISetManagerScreen    = SetManager.OpenSet<UISetManager>();
                SoundManagerObj       = ResourcesManager.Create("Prefab/SoundManager").GetComponent<SoundManager>();
                UISetManagerScreen.Init();             

                BCGame.IsogramWords = LoadWordsList.GetWordsFile();
                GameIsWaiting = true;

                if (GameIsWaiting)
                    ChangeState(GameState.WAITING);
                break;
            case GameState.WAITING:
                LevelTime = 300;
                RoundIndex = 1;

                bool UserInput = Input.anyKey;
                TimeToQuit -= Time.deltaTime;
                if (TimeToQuit <= 0)
                    App.Inst.Quit();
                else if (UserInput && TimeToQuit > 0)
                    TimeToQuit = 120f;
                

                if (GameIsLoading)
                    ChangeState(GameState.LOADING);
                break;
            case GameState.LOADING:
                PlayGame();
                UISetManagerScreen.GetGameSet();
                if (GameIsRunning)
                    ChangeState(GameState.RUNNING);
                break;
            case GameState.RUNNING:
                if (IsRoundStarted)
                {
                    LevelTime -= Time.deltaTime;
                    if (BCGame.GetCurrentTry() >= BCGame.GetMaxTry() || LevelTime < 0)
                    {
                        IsRoundStarted = false;
                        UISetManagerScreen.GetSummarySet();
                        ChangeState(GameState.WAITING);
                    }
                }

                if (GameIsLoading)
                    ChangeState(GameState.LOADING);
                else if(GameIsWaiting)
                    ChangeState(GameState.WAITING);
                break;
        }



        if (Input.GetKeyDown(KeyCode.Space))
            print(BCGame.MyHiddenWord);
	}



    void ChangeState(GameState NewState)
    {
        CurrentState = NewState;

        GameIsWaiting   = false;
        GameIsLoading   = false;
        GameIsRunning   = false;
    }


    // When game is ready to play
    public void PlayGame()
    {
        BCGame.Reset(DifficultyIndex);
        IsRoundStarted = true;
        GameIsRunning = true;
    }



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
            SoundManagerObj.SetAudio(SoundManagerObj.Slose);
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
        BCGame.AddBullAndCow(Guess);
        UISetManager.Inst.GPSet.OutputResult(BCGame.GetBulls(), BCGame.GetCows());
        BCGame.AddToCurrentTry();

        if (BCGame.IsRoundComplete()) // compare after, if changes then reset
        {
            UISetManagerScreen.GPSet.Pausing(UISetManagerScreen.GPSet.ContinuePanel);
            LevelTime += BonusTime;
            RoundIndex++;
        }

        return true;
    }
}

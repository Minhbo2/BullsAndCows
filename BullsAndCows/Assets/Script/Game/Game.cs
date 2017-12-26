using UnityEngine;
using System.Collections.Generic;
using System.IO;


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

    public UISetManager uiSetManager;
    public SoundManager soundManager;

    public BullsCowsGame BCGame = new BullsCowsGame();

    private bool IsRoundStarted    = false;
    public bool IsTutorialComplete = false;

    public float TimeToQuit;
    public float LevelTime;
    private float BonusTime = 30f;

    [Range(1, 3)] 
    public int DifficultyIndex;

    public int HighestRoundCompleted;

    public Data NewData;



    void Start()
    {
        if (m_Inst == null)
            m_Inst = this;
    }



	void Update () {
        switch (CurrentState)
        {
            case GameState.INIT: // setting up all managers and game datas
                uiSetManager    = SetManager.OpenSet<UISetManager>();
                soundManager       = ResourcesManager.Create("Prefab/SoundManager").GetComponent<SoundManager>();
                uiSetManager.NextActiveSet("Intro");

                bool FileExist = File.Exists(Application.persistentDataPath + "bcgame.dat");
                if (FileExist)
                {
                    NewData               = new Data();
                    NewData               = SaveData.LoadData<Data>("bcgame.dat");
                    HighestRoundCompleted = NewData.Round;
                    IsTutorialComplete    = NewData.IsTutorialComplete;
                }


                BCGame.IsogramWords = LoadWordsList.GetWordsFile();
                GameIsWaiting = true;

                if (GameIsWaiting)
                    ChangeState(GameState.WAITING);
                break;
            case GameState.WAITING: // waiting for input from player, reset gameplay
                LevelTime = 300;
                BCGame.CurrentRound = 1;

                bool UserInput = Input.anyKey;
                TimeToQuit    -= Time.deltaTime;
                if (TimeToQuit <= 0)
                    App.Inst.Quit();
                else if (UserInput && TimeToQuit > 0)
                    TimeToQuit = 120f;
                

                if (GameIsLoading)
                    ChangeState(GameState.LOADING);
                break;
            case GameState.LOADING: // get neccessary datas for the game to start
                PlayGame();
                uiSetManager.NextActiveSet("Game Set");
                if (GameIsRunning)
                    ChangeState(GameState.RUNNING);
                break;
            case GameState.RUNNING: // check if win or lose, save data
                if (IsRoundStarted)
                {
                    LevelTime -= Time.deltaTime;
                    bool bRoundIncomplete = BCGame.GetCurrentTry() > BCGame.GetMaxTry();
                    if (bRoundIncomplete || LevelTime < 0)
                    {
                        IsRoundStarted = false;
                        uiSetManager.NextActiveSet("Summary");
                        soundManager.PlayWinLose(!bRoundIncomplete);
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
        GameIsRunning  = true;
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
            soundManager.SetAudio(soundManager.SError);
            uiSetManager.GetComponentInChildren<GamePanelSet>().ErrorMessageText(Message);
            return Message;
        }
        else
        {
            UpdateGameState(Guess);
            return null;
        }
    }



    void UpdateGameState(string Guess)
    {
        BCGame.AddBullAndCow(Guess);
        uiSetManager.GetComponentInChildren<GamePanelSet>().OutputResult(BCGame.GetBulls(), BCGame.GetCows());
        BCGame.AddToCurrentTry();

        bool bRoundCompleted = BCGame.IsRoundComplete();
        if (bRoundCompleted) // compare after, if changes then reset
        {
            uiSetManager.GetComponentInChildren<GamePanelSet>().Pausing(uiSetManager.GetComponentInChildren<GamePanelSet>().ContinuePanel);
            LevelTime += BonusTime;
            BCGame.RoundCompleted();
            soundManager.PlayWinLose(bRoundCompleted);
        }
    }
}

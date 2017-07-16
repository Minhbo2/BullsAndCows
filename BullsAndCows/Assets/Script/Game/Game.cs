using UnityEngine;


public enum GameState
{
    INIT,
    WAITING,
    LOADING,
    RUNNING
}



public class Game : MonoBehaviour {

    public GameState CurrentState = GameState.INIT;

    bool    GameIsWaiting,
            GameIsLoading,
            GameIsRunning;


    private static Game m_Inst;
    public static Game Inst {
        get {return m_Inst;}
    }



    private UISetManager UISetManager;
    private GameObject GameManager;


    void Start()
    {
        if (m_Inst == null)
            m_Inst = this;
    }



	void Update () {
        switch (CurrentState)
        {
            case GameState.INIT:
                // TODO: instantiate all mains and managers
                GameManager     = ResourcesManager.Create("Prefab/GameManager");
                UISetManager    = SetManager.OpenSet<UISetManager>();
                GameIsWaiting = true;

                if (GameIsWaiting)
                    ChangeState(GameState.WAITING);
                break;
            case GameState.WAITING:
                // TODO: checking if states are init
                if (GameManager && UISetManager)
                {
                    // waiting for player to press play
                }

                if (GameIsLoading)
                    ChangeState(GameState.LOADING);
                break;
            case GameState.LOADING:
                // TODO: load all game and players data
                // setting up game logic and UI elements

                if (GameIsRunning)
                    ChangeState(GameState.RUNNING);
                break;
            case GameState.RUNNING:
                // TODO: check for win or lose condition 
                // loop states for gameplay
                break;
        }
	}



    void ChangeState(GameState NewState)
    {
        CurrentState = NewState;

        GameIsWaiting   = false;
        GameIsLoading   = false;
        GameIsRunning   = false;
    }
}

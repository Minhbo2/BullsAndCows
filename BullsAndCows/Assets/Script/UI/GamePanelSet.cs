using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class GamePanelSet : Set {

    [SerializeField]
    private InputField PlayerInputField;
    [SerializeField]
    private ScrollviewManagerSet SMSet;
    [SerializeField]
    Text    LettersWordText,
            TriesRemaining,
            ErrorMessage,
            RoundText,
            Timer,
            HintText;

    Game GM;

    private string PlayerGuess;

    public GameObject PauseScreen;
    public GameObject ContinuePanel;

    List<GameObject> RPList = new List<GameObject>();


    private void Start()
    {
        Init();
    }



    private void Update()
    {
        int CurrentTime = Mathf.RoundToInt(Game.Inst.LevelTime);
        Timer.text = "Time Remaining: " + CurrentTime;
    }



    void Init()
    {
        GM = Game.Inst;
        int WordLength = GM.BCGame.GetWordLength();
        LettersWordText.text = WordLength + " letters word";
        TriesRemain(GM.BCGame.GetCurrentTry());
        HintText.text = GM.BCGame.GetHint();
        RoundText.text = RoundIndex();
    }



    string RoundIndex()
    {
        string RoundText = "Round: " + GM.RoundIndex;
        return RoundText;
    }



    public string ErrorMessageText(string Message)
    {
        ErrorMessage.text = Message;
        return Message;
    }





    public void SubmitByBtn()
    {
        PlayerGuessInput();
    }


    public void SubmitByEnter()
    {
        if (!PlayerInputField.isFocused)
            PlayerGuessInput();
    }


    public void PlayerGuessInput()
    {
        if (PlayerInputField == null)
        {
            Debug.Log("Error: Missing InputField");
            return;
        }
        else
        {
            PlayerGuess = PlayerInputField.text;
            GM.ValidateGuess(PlayerGuess);
        }
    }



    public void OutputResult(int Bulls, int Cows)
    {
        RectTransform ContentArea = SMSet.ContentArea;
        if (ContentArea)
        {
            ResultPrefabSet Result = SetManager.OpenSet<ResultPrefabSet>();
            RPList.Add(Result.gameObject);
            Result.transform.SetParent(ContentArea.transform, false);
            Result.GetComponent<ResultPrefabSet>().GetPlayerGuess(PlayerGuess);
            SMSet.ManageContentAreaSize(Result.gameObject);
            TriesRemain(GM.BCGame.GetCurrentTry());
        }
    }



    string TriesRemain(int Try)
    {
        string CurrentTry = "Try " + Try + " out of " + GM.BCGame.GetMaxTry();
        TriesRemaining.text = CurrentTry;
        return null;
    }



    public void Pausing(GameObject Screen)
    {
        if (Time.timeScale == 1)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

        GetScreen(Screen);
    }


    GameObject GetScreen(GameObject Screen)
    {
        if (!Screen)
            return null;
        else
        {
            bool IsActive = Screen.activeInHierarchy;
            Screen.SetActive(!IsActive);
        }
        return Screen;
    }



    public void QuitToSummary()
    {
        Pausing(PauseScreen);
        UISetManager.Inst.GetSummarySet();
        Game.Inst.GameIsWaiting = true;
    }


    public void ContinueRound()
    {
        Game.Inst.GameIsLoading = true;
        Pausing(ContinuePanel);
        foreach (GameObject rp in RPList)
            Destroy(rp);
        Init();
    }
}

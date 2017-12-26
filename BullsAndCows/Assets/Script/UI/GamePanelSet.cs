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

    Game GM = Game.Inst;

    private string PlayerGuess;

    public GameObject PauseScreen;
    public GameObject ContinuePanel;

    [SerializeField]
    List<ResultPrefabSet> RPList = null;





    private void Update()
    {
        int CurrentTime = Mathf.RoundToInt(Game.Inst.LevelTime);
        Timer.text = "Time Remaining: " + CurrentTime;

#if UNITY_EDITOR
        // Dev Hack
        if (Input.GetKeyDown(KeyCode.Space))
            LettersWordText.text = GM.BCGame.MyHiddenWord;
#endif
    }



    void Init()
    {
        PlayerInputField.Select();
        PlayerInputField.text = "";
        ClearingResults();
        int WordLength = GM.BCGame.GetWordLength();
        LettersWordText.text = WordLength + " letters word";
        TriesRemain(GM.BCGame.GetCurrentTry());
        HintText.text = GM.BCGame.GetHint();
        RoundText.text = RoundIndex();
    }



    private void ClearingResults()
    {
        if (RPList == null) { return; }
        foreach (ResultPrefabSet rp in RPList)
            Destroy(rp.gameObject);
        RPList = null;
    }



    string RoundIndex()
    {
        string RoundText = "Round: " + GM.BCGame.GetRound();
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
        // make sure to always have a list 
        if (RPList == null){ RPList = new List<ResultPrefabSet>(); }

        RectTransform ContentArea = SMSet.ContentArea;
        if (ContentArea)
        {
            ResultPrefabSet Result = ResourcesManager.Create("Sets/ResultPrefabSet").GetComponent<ResultPrefabSet>();
            Result.transform.SetParent(ContentArea.transform, false);
            RPList.Add(Result);
            Result.GetPlayerGuess(PlayerGuess);
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
        Game.Inst.uiSetManager.NextActiveSet("Summary");
        Game.Inst.GameIsWaiting = true;
    }


    public void ContinueRound()
    {
        Game.Inst.GameIsLoading = true;
        Pausing(ContinuePanel);
        Init();
    }



    private void OnEnable()
    {
        Init();
    }
}

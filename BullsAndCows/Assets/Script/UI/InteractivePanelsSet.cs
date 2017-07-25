using UnityEngine.UI;
using UnityEngine;

public class InteractivePanelsSet : Set {

    [SerializeField]
    private InputField PlayerInputField;
    [SerializeField]
    private ScrollviewManagerSet SMSet;
    [SerializeField]
    Text    ChallengeQuestion,
            TriesRemaining,
            ErrorMessage,
            HintText;

    GameManager GM;

    private string PlayerGuess;

    public GameObject PauseScreen;

    private void Start()
    {
        Init();
    }




    private void Init()
    {
        GM = GameManager.Inst;
        int WordLength = GM.BCGame.GetWordLength();
        ChallengeQuestion.text = WordLength + " letters word";
        TriesRemainingText(GM.BCGame.GetCurrentTry());
        HintText.text = GM.BCGame.GetHint();
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
        int CurrentTry = GM.BCGame.GetCurrentTry();
        if (ContentArea)
        {
            ResultPrefabSet Result = SetManager.OpenSet<ResultPrefabSet>();
            Result.transform.SetParent(ContentArea.transform, false);
            Result.GetComponent<ResultPrefabSet>().GetPlayerGuess(PlayerGuess);
            SMSet.ManageContentAreaSize(Result.gameObject);
            TriesRemainingText(CurrentTry);
        }
    }


    string TriesRemainingText(int Try)
    {
        string CurrentTry = "Try " + Try + " out of " + GM.BCGame.GetMaxTry();
        TriesRemaining.text = CurrentTry;
        return null;
    }




    public void Pausing()
    {
        if (Time.timeScale == 1)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

        GetPauseScreen();
    }

    GameObject GetPauseScreen()
    {
        if (!PauseScreen)
            return null;
        else
        {
            bool IsActive = PauseScreen.activeInHierarchy;
            PauseScreen.SetActive(!IsActive);
        }

        return PauseScreen;
    }



    public void BackToMainMenu()
    {
        Pausing();
        UISetManager.Inst.GetMainMenuSet();
    }
}

using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class InteractivePanelsSet : Set {

    [SerializeField]
    private InputField PlayerInputField;
    [SerializeField]
    private ScrollviewManagerSet SMSet;
    [SerializeField]
    Text    ChallengeQuestion,
            TriesRemaining,
            ErrorMessage;

    GameManager GM;





    private void Start()
    {
        Init();
    }




    private void Init()
    {
        GM = GameManager.Inst;
        int WordLength = GM.BCGame.GetWordLength();
        ChallengeQuestion.text = "Can you guess the " + WordLength + " letters word\n I am thinking of?";
        TriesRemainingText(GM.BCGame.GetCurrentTry());
        ErrorMessageText("");
    }






    public string ErrorMessageText(string Message)
    {
        ErrorMessage.text = Message;
        return Message;
    }




    public void PlayerGuessInput()
    {
        string PlayerGuess;
        if (PlayerInputField == null)
        {
            Debug.Log("Error: Missing InputField");
            return;
        }
        else
        {
            PlayerGuess = PlayerInputField.text;
            GM.SubmitGuess(PlayerGuess);
        }
    }






    public void OutputResult(int Bulls, int Cows)
    {
        GameObject TryResult = GetTryResultPrefab();
        RectTransform ContentArea = SMSet.ContentArea;

        bool GameIsWon = GM.BCGame.IsGameWon();
        int CurrentTry = GM.BCGame.GetCurrentTry();

        if (!GameIsWon)
        {
            if (ContentArea)
            {
                SMSet.ManageContentAreaSize(TryResult);
                TryResult.transform.SetParent(ContentArea.transform, false);
                Text ResultText = TryResult.GetComponent<Text>();
                ResultText.text = CurrentTry + ". " + "Bulls: " + Bulls + "         Cows: " + Cows;
                TriesRemainingText(CurrentTry);
            }
        }
    }




    string TriesRemainingText(int Try)
    {
        string CurrentTry = "Try " + Try + " out of " + GM.BCGame.GetMaxTry();
        TriesRemaining.text = CurrentTry;
        return null;
    }




    private GameObject GetTryResultPrefab()
    {
        return ResourcesManager.Create("Prefab/Try");
    }
}

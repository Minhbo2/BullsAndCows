using UnityEngine.UI;
using UnityEngine;

public class InteractivePanelsSet : Set {

    [SerializeField]
    private InputField PlayerInputField;
    [SerializeField]
    private GameObject TriesResultArea;
    [SerializeField]
    Text ChallengeQuestion,
        TriesRemaining,
        ErrorMessage;





    private void Start()
    {
        Init();
    }




    private void Init()
    {
        GameManager GM = GameManager.Inst;
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
            GameManager.Inst.SubmitGuess(PlayerGuess);
        }
    }






    public void OutputResult(int Bulls, int Cows)
    {
        GameObject TryResult = GetTryResultPrefab();
        bool GameIsWon = GameManager.Inst.BCGame.IsGameWon();
        int GetCurrentTry = GameManager.Inst.BCGame.GetCurrentTry();

        if (!GameIsWon)
        {
            if (TriesResultArea)
            {
                TryResult.transform.SetParent(TriesResultArea.transform, false);
                Text ResultText = TryResult.GetComponent<Text>();
                ResultText.text = "Bulls: " + Bulls + "         Cows: " + Cows;
                TriesRemainingText(GetCurrentTry);
            }
        }
    }





    string TriesRemainingText(int Try)
    {
        string CurrentTry = "Try " + Try + " out of " + GameManager.Inst.BCGame.GetMaxTry();
        TriesRemaining.text = CurrentTry;
        return null;
    }




    private GameObject GetTryResultPrefab()
    {
        return ResourcesManager.Create("Prefab/Try");
    }
}

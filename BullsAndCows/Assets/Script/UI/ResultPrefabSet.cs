using UnityEngine;
using UnityEngine.UI;

public class ResultPrefabSet : Set {

    [SerializeField]
    private Text    Try,
                    GuessWord,
                    BullIndex,
                    CowIndex;

    private string PlayerGuess;

    public void SetResult()
    {
        GameManager GM = GameManager.Inst;
        Try.text = GM.BCGame.GetCurrentTry().ToString();
        GuessWord.text = PlayerGuess;
        BullIndex.text = GM.BCGame.GetBulls().ToString();
        CowIndex.text = GM.BCGame.GetCows().ToString();
    }


    public string GetPlayerGuess(string Guess)
    {
        PlayerGuess = Guess;
        SetResult();
        // if win
            // winloseset
        return PlayerGuess;
    }
}

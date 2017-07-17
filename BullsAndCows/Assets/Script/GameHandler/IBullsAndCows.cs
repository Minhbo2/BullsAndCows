using UnityEngine;

public struct BullsCowsCount
{
    int Bulls;
    int Cows;

    void BCCount(int AddBulls, int AddCows)
    {
        Bulls += AddBulls;
        Cows += AddCows;
    }
}



public enum EGuessState
{
    Invalid_Status,
    OK,
    Not_Isogram,
    Wrong_Length,
    Not_Lowercase
}



public interface IBullsAndCows {

    int GetMaxTry();
    int GetCurrentTry();
    string SelectAHiddenWord();
    EGuessState CheckGuessValidity(string Guess);
    string ExtractingGuess(string Guess);
    string SubmitGuess(string Guess);
    bool IsGameWon();
    void Reset();
}

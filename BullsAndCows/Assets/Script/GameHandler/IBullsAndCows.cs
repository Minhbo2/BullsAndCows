using UnityEngine;

public struct BullsCowsCount
{
    public int Bulls;
    public int Cows;

    void BCCount(int AddBulls, int AddCows)
    {
        Bulls += AddBulls;
        Cows += AddCows;
    }
}



public enum EGuessState
{
    Invalid_Status,
    Not_Isogram,
    OK,
    Wrong_Length,
    Not_Lowercase
}



public interface IBullsAndCows {

    int GetMaxTry();
    int GetCurrentTry();
    int GetBulls();
    int GetCows();
    string SelectAHiddenWord();
    int GetWordLength();
    bool IsLowerCase(string Guess);
    bool IsIsogram(string Guess);
    EGuessState CheckGuessValidity(string Guess);
    BullsCowsCount AddBullAndCow(string Guess);
    bool IsGameWon();
    void Reset();
}

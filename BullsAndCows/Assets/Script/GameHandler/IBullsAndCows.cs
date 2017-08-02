﻿using UnityEngine;

public struct BullsCowsCount
{
    public int Bulls;
    public int Cows;

    public void BCCount()
    {

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

    int AddToCurrentTry();
    string GetHint();
    int GetMaxTry();
    int GetCurrentTry();
    int GetBulls();
    int GetCows();
    string SelectAHiddenWord(int WordLength);
    int GetWordLength();
    bool IsLowerCase(string Guess);
    bool IsIsogram(string Guess);
    EGuessState CheckGuessValidity(string Guess);
    BullsCowsCount AddBullAndCow(string Guess);
    bool IsGameWon();
    string SetWordHint();
    void Reset(int DifIndex);
    int RandomWordLength(int DifIndex);
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class BullsCowsGame : IBullsAndCows
{
    int MyMaxTries;
    int MyCurrentTry;
    bool isGameWon = false;
    string MyHiddenWord;
    string Hint;
   
    BullsCowsCount BCCount;

    Dictionary<int, int> WordMaxTry = new Dictionary<int, int>() {
            { 3, 4},
            { 4, 7},
            { 5, 10},
            { 6, 16},
            { 7, 20},
        };

    private string[] HiddenWordArr = new string[5]
    {
        "eat",
        "lake",
        "plane",
        "jungle",
        "jukebox"
    };

    public string GetHint() { return Hint; }

    public bool IsGameWon(){ return isGameWon;}

    public int GetCurrentTry(){ return MyCurrentTry;}

    public int GetWordLength() { return MyHiddenWord.Length;}

    public int GetBulls() { return BCCount.Bulls; }

    public int GetCows() { return BCCount.Cows; }



    public int GetMaxTry()
    {
        MyMaxTries = WordMaxTry[GetWordLength()];
        return MyMaxTries;
    }




    public void Reset()
    {
        MyHiddenWord = SelectAHiddenWord();
        Hint = SetWordHint(MyHiddenWord.Length);
        Debug.Log("You have " + GetMaxTry() + " tries.");
        MyCurrentTry = 1;
        isGameWon = false;
        
        return;
    }





    public string SelectAHiddenWord()
    {
        int WordIndex = UnityEngine.Random.Range(0, HiddenWordArr.Length);
        string SelectedWord = HiddenWordArr[WordIndex];
        return SelectedWord;
    }





    public bool IsIsogram(string Guess)
    {
        bool isIsogram = true;
        Dictionary<char, bool> WordMap = new Dictionary<char, bool>(); 
        for (int i = 0; i < Guess.Length; i++) 
        {
            if (!WordMap.ContainsKey(Guess[i]))
                WordMap.Add(Guess[i], true);
            else
                isIsogram = false;
        }
        return isIsogram;
    }



    public bool IsLowerCase(string Guess)
    {
        foreach(char Char in Guess)
        {
            if (!Char.IsLower(Char))
                return false;
        }
        return true;
    }



    public EGuessState CheckGuessValidity(string Guess)
    {
        if (!IsIsogram(Guess))
            return EGuessState.Not_Isogram;
        else if (!IsLowerCase(Guess))
            return EGuessState.Not_Lowercase;
        else if (Guess.Length != GetWordLength())
            return EGuessState.Wrong_Length;
        else
            return EGuessState.OK;
    }




    public BullsCowsCount AddBullAndCow(string Guess)
    {
        int HiddenWordLenght = MyHiddenWord.Length;
        BCCount = new BullsCowsCount();

        for (var i = 0; i < HiddenWordLenght; i++) // loop though the hidden word length
        {
            for (var j = 0; j < HiddenWordLenght; j++) // assumming the guess word is same length
            {
                if (Guess[j] == MyHiddenWord[i]) // if char is the same
                {
                    if (i == j) // if they are in the same place
                        BCCount.Bulls++;
                    else
                        BCCount.Cows++;
                }
            }
        }

        if (BCCount.Bulls == HiddenWordLenght)
            isGameWon = true;
        else
            isGameWon = false;

        return BCCount;        
    }



    public int AddToCurrentTry()
    {
        MyCurrentTry++;
        return MyCurrentTry;
    }



    //TODO: redo this more roburst
    public string SetWordHint(int WordLength)
    {
        string Hint = "";
        switch (WordLength)
        {
            case 3:
                Hint = "Hint: An action you do to live.";
                break;
            case 4:
                Hint = "Hint: A lot of water in a large pit.";
                break;
            case 5:
                Hint = "Hint: Everyone likes to fly.";
                break;
            case 6:
                Hint = "Hint: Hot, humid, wet and trees.";
                break;
            case 7:
                Hint = "Hint: Old machine at an old cafe.";
                break;
        }
        return Hint;
    }
}

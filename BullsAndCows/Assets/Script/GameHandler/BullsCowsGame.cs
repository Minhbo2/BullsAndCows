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

    List<string> IsogramWords = new List<string>();

    Dictionary<int, int> WordToMaxTry = new Dictionary<int, int>() {
            { 3, 4},
            { 4, 7},
            { 5, 10},
            { 6, 16},
            { 7, 20},
            { 8, 29}
        };

    private char[] Alphabet = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };


    public string GetHint() { return Hint; }
    public bool IsGameWon(){ return isGameWon;}
    public int GetCurrentTry(){ return MyCurrentTry;}
    public int GetWordLength() { return MyHiddenWord.Length;}
    public int GetBulls() { return BCCount.Bulls; }
    public int GetCows() { return BCCount.Cows; }



    public int GetMaxTry()
    {
        MyMaxTries = WordToMaxTry[GetWordLength()];
        return MyMaxTries;
    }




    public void Reset(int DifIndex)
    {
        int WordLength = RandomWordLength(DifIndex);
        MyHiddenWord = SelectAHiddenWord(WordLength);
        Hint = SetWordHint();
        MyCurrentTry = 1;
        isGameWon = false;
        
        return;
    }




    public string SelectAHiddenWord(int WordLength)
    {
        char RandomChar      = Alphabet[UnityEngine.Random.Range(0, Alphabet.Length)];
        IsogramWords         = LoadWordsList.CharWordsWithLength(RandomChar, WordLength);

        string SelectedWord  = IsogramWords[UnityEngine.Random.Range(0, IsogramWords.Count)];
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



    public string SetWordHint()
    {
        char FirstChar = MyHiddenWord[0];
        char SecondChar = MyHiddenWord[MyHiddenWord.Length - 1];
        string Hint = "Hint: Starts with " + FirstChar + ", Ends with " + SecondChar;

        return Hint;
    }


    public int RandomWordLength(int DifIndex)
    {
        int WordLength = 3;
        switch (DifIndex)
        {
            case 1:
                WordLength = UnityEngine.Random.Range(3, 6);
                break;
            case 2:
                WordLength = UnityEngine.Random.Range(5, 8);
                break;
            case 3:
                WordLength = UnityEngine.Random.Range(6, 9);
                break;
        }
        return WordLength;
    }
}

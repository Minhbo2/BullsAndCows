using System;
using System.Collections.Generic;

public class BullsCowsGame : IBullsAndCows
{
    int MyMaxTries;
    int MyCurrentTry;
    bool RoundComplete;
    string Hint;

    public string MyHiddenWord;

    BullsCowsCount BCCount;

    public List<string> IsogramWords = new List<string>();

    Dictionary<int, int> WordToMaxTry = new Dictionary<int, int>() {
            { 4, 7},
            { 5, 10},
            { 6, 16},
            { 7, 20},
            { 8, 26},
        };

    private char[] Alphabet = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};


    public bool IsRoundComplete() { return RoundComplete; }
    public string GetHint() { return Hint; }
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
        RoundComplete = false;
    }




    public string SelectAHiddenWord(int WordLength)
    {
        char RandomChar         = Alphabet[UnityEngine.Random.Range(0, Alphabet.Length)];
        List<string> CharWords  = new List<string>();
        bool IsComplete         = false;
        int WordIndex           = 0;

        while (!IsComplete)
        {
            if (WordIndex >= IsogramWords.Count)
                break;
            else
            {
                char CurrentChar = IsogramWords[WordIndex][0];
                if (CurrentChar != RandomChar && CharWords.Count > 0) // after adding, if the next word doesnt start with char then finish
                    IsComplete = true;
                else if (CurrentChar == RandomChar && IsogramWords[WordIndex].Length == WordLength) // go through list, once find, add to list
                {
                    CharWords.Add(IsogramWords[WordIndex]);
                    IsComplete = false;
                }
            }
            WordIndex++;
        }

        int RandomIndex      = UnityEngine.Random.Range(0, CharWords.Count);
        UnityEngine.Debug.Log(CharWords.Count + " " + RandomIndex);
        string SelectedWord  = CharWords[RandomIndex];
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
            RoundComplete = true;

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
        int FirstIndex = DifIndex + 3;
        int SecondIndex = FirstIndex + 3; 
        return UnityEngine.Random.Range(FirstIndex, SecondIndex);// (4,7)(5,8)(6,9)
    }
}

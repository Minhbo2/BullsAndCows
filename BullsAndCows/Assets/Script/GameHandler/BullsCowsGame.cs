using System;
using UnityEngine;

public class BullsCowsGame : IBullsAndCows
{
    int MyMaxTries = 0;
    int MyCurrentTry = 0;
    bool isGameWon = false;
    string MyHiddenWord = "";

    BullsCowsCount BCGame;

    private string[] HiddenWordArr = new string[4]
    {
        "cue",
        "lake",
        "planet",
        "jungle"
    };




    public int GetMaxTry() { return MyMaxTries;}

    public bool IsGameWon(){ return isGameWon;}

    public int GetCurrentTry(){ return MyCurrentTry;}


    public void Reset()
    {
        MyHiddenWord = SelectAHiddenWord();
        MyCurrentTry = 1;
        isGameWon = false;
        
        return;
    }





    public string SelectAHiddenWord()
    {
        int WordIndex = UnityEngine.Random.Range(0, HiddenWordArr.Length);
        string SelectedWord = HiddenWordArr[WordIndex];
        Debug.Log(SelectedWord);
        return SelectedWord;
    }


    public string ExtractingGuess(string Guess)
    {
        // TODO: grab and extracting the guess user input in the inputfield
        return Guess;
    }



    public EGuessState CheckGuessValidity(string Guess)
    {
        return EGuessState.OK;
    }




    public string SubmitGuess(string Guess)
    {
        throw new NotImplementedException();
    }

}

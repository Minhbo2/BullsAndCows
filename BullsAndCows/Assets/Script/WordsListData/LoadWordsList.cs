using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class LoadWordsList : MonoBehaviour
{
    private string JsonWordList = "/StreamingAssets/wordlist.json";

    public static void LoadWordsJson(string FilePath)
    {
        string NewFilePath = Application.dataPath + FilePath;
        if (File.Exists(NewFilePath))
        {
            string DataAsJson           = File.ReadAllText(NewFilePath);
            WordsList LoadedWordList    = JsonUtility.FromJson<WordsList>(DataAsJson);
            string[] WordArr            = LoadedWordList.wordlist;
            bool IsIsogram              = false;
            List<string> NewIsogramList = new List<string>();

            for (int LetterIndex = 0; LetterIndex < WordArr.Length; LetterIndex++)
            {
                Dictionary<char, bool> WordMap = new Dictionary<char, bool>();
                string CurrentWord = WordArr[LetterIndex];

                for (int CharIndex = 0; CharIndex < CurrentWord.Length; CharIndex++)
                {
                    if (!WordMap.ContainsKey(CurrentWord[CharIndex]))
                    {
                        WordMap.Add(CurrentWord[CharIndex], true);
                        if (CharIndex == CurrentWord.Length - 1)
                            IsIsogram = true;
                    }
                    else
                    {
                        IsIsogram = false;
                        break;
                    }
                }

                if (IsIsogram)
                    NewIsogramList.Add(CurrentWord);
            }

            // after loading and sorting json file, save it into a binary file for future load
            IsogramList SaveFile = new IsogramList();
            SaveFile.IsogramWords = NewIsogramList;
            SaveData.SavingData(SaveFile, "/isogramwordlist.dat");
        }
        else
            Debug.Log("FilePath is Null");
    }



    // load isogram word list from the save file and look for words starts with char
    public static List<string> CharWordsWithLength(char Char, int WordLength)
    {
        IsogramList WordsWithChar = new IsogramList();
        WordsWithChar.IsogramWords = new List<string>();
        List<string> LoadWordFile = SaveData.LoadData<IsogramList>("/isogramwordlist.dat").IsogramWords;

        if (LoadWordFile != null)
        {
            bool IsComplete = false;
            int WordIndex = 0;

            while (!IsComplete)
            {
                char CurrentChar = LoadWordFile[WordIndex][0];
                if (CurrentChar == Char && LoadWordFile[WordIndex].Length == WordLength) // go through list, once find, add to list
                {
                    WordsWithChar.IsogramWords.Add(LoadWordFile[WordIndex]);
                    IsComplete = false;
                }
                else if (CurrentChar != Char && WordsWithChar.IsogramWords.Count > 0) // after adding, if the next word doesnt start with char then finish
                    IsComplete = true;

                WordIndex++;
            }
        }
        else
            Debug.Log("Can't load save file");
        
        return WordsWithChar.IsogramWords;
    }
}

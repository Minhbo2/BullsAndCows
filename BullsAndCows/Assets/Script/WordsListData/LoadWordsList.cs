using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class LoadWordsList : MonoBehaviour
{
    private void Start()
    {
        //LoadWordsJson(JsonWordList);
    }

    public static void SavedToFile(string FilePath)
    {
        string NewFilePath = Application.dataPath + FilePath;
        if (File.Exists(NewFilePath))
        {
            string DataAsJson           = File.ReadAllText(NewFilePath);
            WordsList LoadedWordList    = JsonUtility.FromJson<WordsList>(DataAsJson);
            string[] WordArr            = LoadedWordList.wordlist;
            List<string> NewIsogramList = new List<string>();
            BullsCowsGame BCGame        = new BullsCowsGame();

            for (int LetterIndex = 0; LetterIndex < WordArr.Length; LetterIndex++)
            {
                string CurrentWord = WordArr[LetterIndex];
                if(BCGame.IsIsogram(CurrentWord))
                    NewIsogramList.Add(CurrentWord);
            }

            // after loading and sorting json file, save it into a binary file for future load
            IsogramList SaveFile  = new IsogramList();
            SaveFile.IsogramWords = NewIsogramList;
            SaveData.SavingData(SaveFile, "/isogramwordlist.dat");
        }
        else
            Debug.Log("FilePath is Null");
    }



    // load isogram word list from the save file and look for words starts with char
    public static List<string> GetWordsFile()
    {
        List<string> LoadWordFile = SaveData.LoadData<IsogramList>("/isogramwordlist.dat").IsogramWords;
        return LoadWordFile;
    }
}

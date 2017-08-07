using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class Data
{
    public int Round;
    public bool IsTutorialComplete;

    public Data()
    {

    }

    public Data(int RoundCompleted, bool IsComplete)
    {
        Round = RoundCompleted;
        IsTutorialComplete = IsComplete;
    }
}


public class SaveData  {

    // give a file type to load 
    public static T LoadData<T>(string FileType)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + FileType, FileMode.Open);
        T LoadedData = (T)bf.Deserialize(file);
        file.Close();

        return LoadedData;
    }


    // give a file type to save
    public static void SavingData<T>(T SaveFile, string FileType)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + FileType);
        bf.Serialize(file, SaveFile);
        file.Close();
    }
}

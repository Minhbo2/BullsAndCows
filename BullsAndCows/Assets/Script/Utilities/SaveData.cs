using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public struct Data
{
    public bool IsTutorialComplete;
}


public class SaveData  {

    public static void LoadData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/BCGame.dat", FileMode.Open);
        Data loadedData = (Data)bf.Deserialize(file);
        file.Close();

        GameManager.Inst.IsTutorialComplete = loadedData.IsTutorialComplete;
    }

    public static void SavingData()
    {
        Data NewData = new Data();
        NewData.IsTutorialComplete = GameManager.Inst.IsTutorialComplete;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/BCGame.dat");
        bf.Serialize(file, NewData);
        file.Close();
    }
}

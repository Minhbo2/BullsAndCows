﻿using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System;

public class App : MonoBehaviour {

    private static App m_Inst;
    public static App Inst { get{ return m_Inst; } }

    [NonSerialized]
    public bool IsRunning = true;

    private string JsonWordList = "/StreamingAssets/wordlist.json";


    // Constructor
    public App()
    {
        if (m_Inst == null)
            m_Inst = this;
    }



    // Game entry point (this is the first thing done when the game boots)
    public void Awake()
    {
        // TODO: Load all of your data
        Init();
        // TODO: Initialize the application    
    }

    private void Init()
    {
        // TODO: Add your app after loading data
        bool FileSaved = File.Exists(Application.persistentDataPath + "/isogramwordlist.dat");
        if (!FileSaved)
            LoadWordsList.SavedToFile(JsonWordList);
    }

    // Update is called once per frame
//    void Update()
//    {

//#if UNITY_EDITOR
//        ------------------------
//        Dev Hacks
//        ------------------------

//         NOTE: Put dev hacks here

//         ------------------------
//#endif
//    }

    public void Reset()
    {
        // TODO: Reset the entire application
    }

    public void Pause()
    {
        App.Inst.IsRunning = false;
    }

    public void Unpause()
    {
        App.Inst.IsRunning = false;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public bool GetIsRunning()
    {
        return IsRunning;
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SoundManager : MonoBehaviour {
    public static SoundManager Inst { get{ return m_Inst;}}
    static SoundManager m_Inst;

    
    public AudioSource BackgroundAudio;
    public AudioSource SFX;

    public AudioClip    SIntro,
                        SBtn,
                        SIngame,
                        SWin,
                        SLose;


    private void Start()
    {
        if (m_Inst == null)
            m_Inst = this;

    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject ObjSelected = EventSystem.current.currentSelectedGameObject;
            if (ObjSelected == null)
                return;
            else
            {
                bool IsButton = ObjSelected.GetComponent<Button>();
                if (!IsButton)
                    return;
                else
                    PlayButtonSFX();
            }
        }
    }

    public void PlayButtonSFX()
    {
        SetAudio(SBtn);
    } 

    public void PlayIntro()
    {
        SetAudio(SIntro);
    }

    public void PlayIngame()
    {
        PlayInGameMusic();
    }

    public void PlayWinLose(bool IsGameWon)
    {
        if (IsGameWon)
            SetAudio(SWin);
        else
            SetAudio(SLose);
    }



    public void SetAudio(AudioClip NewAudio)
    {
        SFX.clip = NewAudio;
        SFX.Play();
    }


    public void PlayInGameMusic()
    {
        BackgroundAudio.clip = SIngame;
        BackgroundAudio.Play();
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SoundManager : MonoBehaviour { 
    
    public AudioSource BackgroundAudio;
    public AudioSource SFX;

    public AudioClip    SIntro,
                        SBtn,
                        SIngame,
                        SWin,
                        Slose,
                        SError;



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
            SetAudio(Slose);
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

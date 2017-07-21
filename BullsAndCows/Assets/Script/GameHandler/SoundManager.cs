using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {
    public static SoundManager Inst { get{ return m_Inst;}}
    static SoundManager m_Inst;

    
    public AudioSource MyAudioSource;

    public AudioClip    SIntro,
                        SIngame,
                        SWin,
                        SLose;


    private void Start()
    {
        if (m_Inst == null)
            m_Inst = this;

        MyAudioSource = GetComponent<AudioSource>();
    }


    public void PlayIntro()
    {
        SetAudio(SIntro);
    }

    public void PlayIngame()
    {
        SetAudio(SIngame);
    }

    public void PlayWinLose()
    {
        bool IsGameWon = GameManager.Inst.BCGame.IsGameWon();
        if (IsGameWon)
            SetAudio(SWin);
        else
            SetAudio(SLose);
    }


    public void SetAudio(AudioClip NewAudio)
    {
        MyAudioSource.clip = NewAudio;
        MyAudioSource.Play();
    }
}

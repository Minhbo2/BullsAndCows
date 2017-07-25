using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SplashIntroSet : Set {


    public void GetMainMenu()
    {
        UISetManager.Inst.GetMainMenuSet();
    }

    public void PlayIntroSound()
    {
        SoundManager.Inst.PlayIntro();
    }
}

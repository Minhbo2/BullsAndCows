using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SplashIntroSet : Set {


    public void GetMainMenu()
    {
        Game.Inst.uiSetManager.NextActiveSet("Main Menu");
    }

    public void PlayIntroSound()
    {
        Game.Inst.soundManager.PlayIntro();
    }
}

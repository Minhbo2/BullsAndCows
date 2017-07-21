using UnityEngine;
using UnityEngine.UI;

public class MainMenuSet : Set {
    [SerializeField]
    private GameObject OptionScreen;
    [SerializeField]
    private GameObject ButtonsGroup;
    [SerializeField]
    private Slider SoundSlider;

    public void PlayButton()
    {
        // TODO: loading datas, checking ui screens, setting game profiles
        // changing gamestate to loading
        UISetManager.Inst.GetGameSet();
        GameManager.Inst.PlayGame();
    }


    public void OptionButton()
    {
        // TODO: open option screen
        // change game setting 
        if (OptionScreen)
            ToggleOnOff();
    }

    public void Quit()
    {

    }



    public void ToggleOnOff()
    {
        bool IsOptionActive = OptionScreen.activeInHierarchy;
        bool IsBtnsActive = ButtonsGroup.activeInHierarchy;

        OptionScreen.SetActive(!IsOptionActive);
        ButtonsGroup.SetActive(!IsBtnsActive);
    }


    public void ManageSoundVolume()
    {
        float SoundIndex = 0;

        if (SoundSlider)
        {
            SoundIndex = SoundSlider.value;
            SoundManager.Inst.MyAudioSource.volume = SoundIndex;
        }
    }
}

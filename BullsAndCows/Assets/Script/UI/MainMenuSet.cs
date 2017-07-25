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
        if (!GameManager.Inst.IsTutorialComplete)
            UISetManager.Inst.GetTutorialSet();
        else
        {
            UISetManager.Inst.GetGameSet();
            GameManager.Inst.PlayGame();
        }
    }


    public void OptionButton()
    {
        if (OptionScreen)
            ToggleOnOff();
    }

    public void Quit()
    {
        Application.Quit();
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
            SoundManager.Inst.BackgroundAudio.volume = SoundIndex;
        }
    }


    private void OnEnable()
    {
        SoundManager.Inst.PlayIngame();
    }
}

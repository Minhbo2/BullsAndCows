using UnityEngine;
using UnityEngine.UI;
using System;

public class MainMenuSet : Set {
    [SerializeField]
    private GameObject OptionScreen;
    [SerializeField]
    private GameObject ButtonsGroup;
    [SerializeField]
    private Slider SoundSlider;
    [SerializeField]
    private Slider DifficultySlider;



    private void Start()
    {
        DifficultySlider.value = Game.Inst.DifficultyIndex;
        SoundSlider.value = SoundManager.Inst.BackgroundAudio.volume;
    }



    public void PlayButton()
    {
        if (!Game.Inst.IsTutorialComplete)
            UISetManager.Inst.GetTutorialSet();
        else
            Game.Inst.GameIsLoading = true;
    }


    public void OptionButton()
    {
        if (OptionScreen)
            ToggleOnOff();
    }

    public void Quit()
    {
        App.Inst.Quit();
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



    public void DifficultySetting()
    {
        float DifficultIndex = DifficultySlider.value;
        int WholeIndex = Mathf.RoundToInt(DifficultIndex);
        Game.Inst.DifficultyIndex = WholeIndex;
    }



    private void OnEnable()
    {
        SoundManager.Inst.PlayIngame();
    }
}

using UnityEngine;

public class UISetManager :Set {


    [SerializeField]
    GameObject Holder;

    private Set newSet = null;



    public void NextActiveSet(string setName)
    {
        if (newSet != null)
            newSet.CloseSet();

        switch (setName)
        {
           case "Intro":
                newSet = SetManager.OpenSet<SplashIntroSet>();
                break;
            case "Main Menu":
                newSet = SetManager.OpenSet<MainMenuSet>();
                break;
            case "Tutorial":
                newSet = SetManager.OpenSet<TutorialSet>();
                break;
            case "Game Set":
                newSet = SetManager.OpenSet<GamePanelSet>();
                break;
            case "Summary":
                newSet = SetManager.OpenSet<SummarySet>();
                break;
        }

        newSet.transform.SetParent(Holder.transform, false);
    }
}

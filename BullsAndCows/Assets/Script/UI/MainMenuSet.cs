using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSet : Set {


    public void PlayButton()
    {
        // TODO: loading datas, checking ui screens, setting game profiles
        // changing gamestate to loading
        CloseSet();
    }


    public void OptionButton()
    {
        // TODO: open option screen
        // change game setting 
        CloseSet();
    }

    public void Quit()
    {

    }


}

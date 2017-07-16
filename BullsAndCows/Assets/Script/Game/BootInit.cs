using UnityEngine;

public class BootInit : MonoBehaviour {

    void Start()
    {
        var App = ResourcesManager.Create("Game/App");
        if (App)
        {
            App.name = "App";

            var Game = ResourcesManager.Create("Game/Game");
            Game.name = "Game";

            Destroy(gameObject);
        }
    }
}

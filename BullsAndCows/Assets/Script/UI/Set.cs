using UnityEngine;

public class Set : MonoBehaviour {

    public void CloseSet()
    {
        SetManager.ToggleSet(this);
    }
}

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class InputFieldSet : MonoBehaviour {

    private InputField InputField;

    void Start()
    {
        InputField = GetComponent<InputField>();
    }


    public void SubmitWord()
    {
        string WordSubmitted = InputField.text;
        print(WordSubmitted);
    }
}

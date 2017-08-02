using UnityEngine.UI;
using UnityEngine;

public class TutorialSet : Set {

    [SerializeField]
    private Text InstructionText;

    public float Timer;
    public float StartTimer;


    void Start()
    {
        if (InstructionText)
            InstructionText.text = PrintIntro();
    }



    void Update()
    {
        StartTimer += Time.deltaTime;
        if (StartTimer > Timer)
            SkipIntro();
    }




    private string PrintIntro()
    {
        string IntroMessage = "Bulls and Cows is a word guessing game. \n";
        string IntroMessage2 = "A Bull for every correct letter \n in the right position. \n";
        string IntroMessage3 = "A Cow for every correct letter \n in wrong position. \n";
        string IntroMessage4 = "A word must be an ISOGRAM. \n";

        string CompleteIntroMessage = IntroMessage + IntroMessage2 + IntroMessage3 + IntroMessage4;
        return CompleteIntroMessage;
    }



    public void SkipIntro()
    {
        GameManager.Inst.IsTutorialComplete = true;
        UISetManager.Inst.GetGameSet();
    }
}

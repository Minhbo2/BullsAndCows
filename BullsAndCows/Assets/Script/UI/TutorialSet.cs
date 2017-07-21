using UnityEngine.UI;
using UnityEngine;

public class TutorialSet : Set {

    [SerializeField]
    private Text InstructionText;

    public float Timer;
    public float StartTimer;


    void Start()
    {
        GettingIntroText();
    }



    void Update()
    {
        StartTimer += Time.deltaTime;
        if (StartTimer > Timer)
            SkipIntro();
    }




    private void GettingIntroText()
    {
        if (InstructionText)
        {
            string IntroText = GameManager.Inst.PrintIntro();
            InstructionText.text = IntroText;
        }
    }



    public void SkipIntro()
    {
        UISetManager.Inst.GetGameSet();
        GameManager.Inst.PlayGame();
    }
}

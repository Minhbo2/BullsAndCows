using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Inst { get { return m_Inst; } }
    static GameManager m_Inst;

    BullsCowsGame BCGame = new BullsCowsGame();


	void Start () {
        if (m_Inst == null)
            m_Inst = this;
	}


	void Update () {
		
	}



    public string PrintIntro()
    {
        // TODO: get the intro setup before going into gameplay
        string IntroMessage = "Bulls and Cows is a word guessing game.\n";
        string IntroMessage2 = "A Bull for every letter in the right position.\n";
        string IntroMessage3 = "A Cow for every letter not in the right position.\n";

        string CompleteIntroMessage = IntroMessage + IntroMessage2 + IntroMessage3;
        return CompleteIntroMessage;
    }



    void PlayGame()
    {
        BCGame.Reset();

        bool PlayAgain = true;

        do
        {

        } while (PlayAgain);
    }


    bool AskPlayAgain()
    {
        return true;
    }
}

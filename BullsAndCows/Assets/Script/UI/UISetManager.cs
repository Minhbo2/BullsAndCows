using UnityEngine;

public class UISetManager :Set {

    private static UISetManager m_Inst;
    public static UISetManager Inst {get { return m_Inst; }}

    [SerializeField]
    GameObject Holder;

    public GamePanelSet GPSet;
    public TutorialSet TSet;
    public MainMenuSet MMSet;
    public SummarySet SumSet;
    public SplashIntroSet SISet;



	void Start () {
        if (m_Inst == null)
            m_Inst = this;

        if (Holder)
            Init();
    }



    public void Init()
    {
        SISet = GetSet(SISet);
    }

    public void GetTutorialSet()
    {
        TSet = GetSet(TSet);
    }


    public void GetMainMenuSet()
    {
        MMSet = GetSet(MMSet);
    }



    public void GetGameSet()
    {
        GPSet = GetSet(GPSet);
    }


    public void GetSummarySet()
    {
        SumSet = GetSet(SumSet);
    }



    T GetSet<T>(T MySet) where T : Set
    {
        Set ActiveSet = Holder.GetComponentInChildren<Set>();
        if (ActiveSet)
            ActiveSet.CloseSet();

        MySet = SetManager.OpenSet <T> ();
        MySet.transform.SetParent(Holder.transform, false);

        return MySet;
    }
}

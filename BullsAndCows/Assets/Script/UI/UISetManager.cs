using UnityEngine;

public class UISetManager :Set {

    private static UISetManager m_Inst;
    public static UISetManager Inst {get { return m_Inst; }}

    [SerializeField]
    GameObject Holder;

    public InteractivePanelsSet IPSet;
    public TitleBGSet TBGSet;
    public MainMenuSet MMSet;
    public WinLoseSet WLSet;



	void Start () {
        if (m_Inst == null)
            m_Inst = this;

        if (Holder)
            GetTitleSet();
    }



    void GetTitleSet()
    {
        TBGSet = GetSet(TBGSet);
    }


    public void GetMainMenuSet()
    {
        MMSet = GetSet(MMSet);
    }



    public void GetGameSet()
    {
        IPSet = GetSet(IPSet);
    }


    public void GetWinLoseSet()
    {
        WLSet = GetSet(WLSet);
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

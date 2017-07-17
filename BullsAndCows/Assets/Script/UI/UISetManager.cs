using UnityEngine;

public class UISetManager :Set {

    private static UISetManager m_Inst;
    public static UISetManager Inst {get { return m_Inst; }}

    [SerializeField]
    GameObject Holder;
    InteractivePanelsSet IPSet;
    TitleBGSet TBGSet;
    MainMenuSet MMSet;


	void Start () {
        if (m_Inst == null)
            m_Inst = this;

        if (Holder)
            Init();
            
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void Init()
    {
        TBGSet = SetManager.OpenSet<TitleBGSet>();
        TBGSet.transform.SetParent(Holder.transform, false);
    }


    public void GetMainMenuSet()
    {
        MMSet = SetManager.OpenSet<MainMenuSet>();
        MMSet.transform.SetParent(Holder.transform, false);
    }



    public void GetGameSet()
    {
        IPSet = SetManager.OpenSet<InteractivePanelsSet>();
        IPSet.transform.SetParent(Holder.transform, false);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class SetManager {

    protected static SetManager m_Inst;

    List<Set> Sets = new List<Set>();
    Set CurrentSet;

    public static SetManager Inst
    {
        get
        {
            if (m_Inst == null)
                m_Inst = new SetManager();
            return m_Inst;
        }
    }


    public static T OpenSet<T>() where T: Set 
    {
        string SetName = typeof(T).Name;

        if (Inst.CurrentSet != null)
            Inst.CurrentSet.gameObject.SetActive(true);

        GameObject SetGO = ResourcesManager.Create("Sets/" + SetName);
        if (SetGO != null)
        {
            T CastedObject = SetGO.GetComponent<T>();
            if (CastedObject)
            {
                AddToSets<T>(CastedObject);
            }
            else
            {
                Debug.LogWarning(SetName + " didn't have a Set component attached, adding it for you..");
                CastedObject = SetGO.AddComponent<T>();
                AddToSets<T>(CastedObject);
            }

            return CastedObject;
        }
        return null;
    }


    private static void AddToSets<T>(T CastedObject)where T : Set
    {
        Inst.Sets.Add(CastedObject);
        Inst.CurrentSet = CastedObject;
        Inst.CurrentSet.gameObject.SetActive(true);
    }



    public static void CloseSet(Set set)
    {
        var inst = Inst;
        if(!inst.Sets.Contains(set))
        {
            Debug.Log("Tried to close a set that wasn't on the stack");
            return;
        }
        RemoveAndDestroySet(set);
    }



    static void RemoveAndDestroySet(Set set)
    {
        Inst.Sets.RemoveAll(s => s == set);

        if (set)
            UnityEngine.Object.Destroy(set.gameObject);
    }
}

using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ResourcesManager{

    protected static ResourcesManager m_Inst;
    Dictionary<string, UnityEngine.Object> cachedObjects = new Dictionary<string, UnityEngine.Object>();

    public static ResourcesManager Inst
    {
        get
        {
            if (m_Inst == null)
                m_Inst = new ResourcesManager();
            return m_Inst;
        }
    }



    public static GameObject Create(string prefabPath)
    {
        if (!Inst.cachedObjects.ContainsKey(prefabPath))
            Inst.cachedObjects[prefabPath] = Resources.Load(prefabPath);
        else if (Inst.cachedObjects[prefabPath] == null)
        {
            Debug.Log("Could not load " + prefabPath);
            return null;
        }

        return UnityEngine.Object.Instantiate(Inst.cachedObjects[prefabPath]) as GameObject;
    }
}

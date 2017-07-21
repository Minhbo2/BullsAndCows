using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Scrollbar))]
public class ScrollviewManagerSet : Set {

    [SerializeField]
    private Scrollbar VerticalScrollbar;
    public RectTransform ContentArea;
    public List<GameObject> TriesList = new List<GameObject>();



    public void ManageContentAreaSize(GameObject Try)
    {
        TriesList.Add(Try);

        if (TriesList.Count > 6)
        {
            float Height = ContentArea.rect.height;
            RectTransform TryRect = Try.GetComponent<RectTransform>();
            Height += TryRect.rect.height;
            ContentArea.sizeDelta = new Vector2(0, Height);
            VerticalScrollbar.value = 0;
        }
    }
}

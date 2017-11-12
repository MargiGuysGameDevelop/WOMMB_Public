using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUIFigureData : MonoBehaviour {

    Dictionary<string, string> elementsData;

    public GameObject Base;

    private MyUIFigureDataElemenrt[] UIs;

    public Dictionary<string, string> Elements {
        set {
            elementsData = value;
            if (Base != null) {
                UIs = new MyUIFigureDataElemenrt[elementsData.Count];
                for (int i = 0; i < elementsData.Keys.Count; i++,elementsData.Keys.GetEnumerator().MoveNext(),elementsData.Values.GetEnumerator().MoveNext()) {
                    GameObject go = Instantiate(Base) as GameObject;
                    UIs[i] = go.GetComponent<MyUIFigureDataElemenrt>();
                    UIs[i].Title.text = elementsData.Keys.GetEnumerator().Current;
                    UIs[i].Content.text = elementsData.Values.GetEnumerator().Current;
                }
            }
        }
    }
}

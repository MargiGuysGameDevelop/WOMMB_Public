using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyUIElementPresent : MonoBehaviour {

    public Text Title;
    public Image Icon;
    public Text Content;
    public MyUnityUI Btn;

    MyUIElement element;

    public MyUIElement Element {
        set {
            element = value;
            if(Title!=null)
                Title.text = value.Name;
            if(Icon != null)
               Icon.sprite = value.Icon;
            if(Content != null)
                Content.text = value.Content;
            if (Btn != null)
            {
                Btn.OnClickEvent = value.ButtonClickEvent;
            }
        }
        get { return element; }
    }

    private RectTransform rect;
    public RectTransform Transform {
        get {
            if (rect == null)
                rect = GetComponent<RectTransform>();
            return rect;
        }
    }

	void Start () {
		
	}
}

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

    MyUIElementData element;

    public MyUIElementData Element {
        set {
            element = value;
            Body.SetActive(element != null);
            if (element == null)
                return;
            if (Title!=null)
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

    private GameObject myGameObject;
    public GameObject Body {
        get {
            if (myGameObject == null)
                myGameObject = gameObject;
            return myGameObject;
        }
    }

	void Start () {
		
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderPicker : MyUnityUI,IDragHandler,IEndDragHandler,IBeginDragHandler {

    /// <summary>
    /// 使用三個物件來回選取
    /// </summary>
    int sliderNumber = 3;
    string[] sliderNames = {"0","1","2" };

    public GameObject Base;

    public SliderPickElement[] myEUIs;

    SliderPickElement[] ElementsUI {
        get {
            if (myEUIs == null || myEUIs.Length != sliderNumber) {
                //UI陣列
                myEUIs = new SliderPickElement[sliderNumber];
                for (int i = 0; i < sliderNumber; i++) {
                    myEUIs[i] = Instantiate(Base).GetComponent<SliderPickElement>() ;
                    myEUIs[i].Trans.SetParent(Transform);
                }
                Base.SetActive(false);
            }
            RePositionUI(0, false);
            return myEUIs;
        }
    }

    /// <summary>
    /// Data Elements
    /// </summary>
    protected MyUIElementData[] elements;

    /// <summary>
    /// Set Elements of one rol.
    /// </summary>
    /// <param name="_rol"></param>
    /// <param name="_eles"></param>
    /// <returns></returns>
    public bool Elements(MyUIElementData[] _eles) {
        elements = _eles;
        return true;
    }

    protected int currentDataPosition = 0;
    protected int CurrentDataPosition {
        set {
            currentDataPosition = value % elements.Length;
        }
        get { return currentDataPosition; }
    }

    protected int ShiftLeftDataPosition {
        get {
            currenmtDataIndex --;
            if (currenmtDataIndex < 0)
                currenmtDataIndex = elements.Length - 1;
            return currenmtDataIndex;
        }
    }

    protected int ShiftRightDataPosition {
        get {
            currenmtDataIndex++;
            if (currenmtDataIndex >= elements.Length)
                currenmtDataIndex = 0;
            return currenmtDataIndex;
        }
    }

    protected int currenmtDataIndex = 0;
    /// <summary>
    /// Get Current Index Slider Element By Rol
    /// </summary>
    public int Index() {
        if (elements == null)
        {
            return -1;
        }
        return currenmtDataIndex;
    }

    /// <summary>
    /// Decide how element work.
    /// </summary>
    /// <param name="_xDistance"></param>
    public virtual void Slide(float _xDistance) {
        if (_xDistance == 0)
            return;
        if (_xDistance < 0)
        {
            SlidLeft();
        }
        else
        {
            SlidRight();
        }

        if (elements == null)
            return;

        if (_xDistance < 0)
        {
            currenmtDataIndex = ShiftLeftDataPosition;
        }
        else
        {
            currenmtDataIndex = ShiftRightDataPosition;
        }
        myEUIs[1].Element = elements[currenmtDataIndex];

    }

    public void SlidLeft() {
        RePositionUI(-1,true);

    }

    public void SlidRight() {
        RePositionUI(1,true);
    }

    public void SlidToIndex(int _index) {
        int offset = 0;

        if (elements == null)
        {
            Debug.LogWarning("No Element.");
            return;
        }

        if (_index == CurrentDataPosition)
            return;

        if (_index > CurrentDataPosition)
        {
            offset = (1);
        }
        else
        {
            offset = -1;
        }

        RePositionUI(offset,true);


    }

    private void RePositionUI(int _offset,bool _anim) {
        int position = _offset - 1;
        SliderPickElement[] newEUI = new SliderPickElement[3];
        for (int i = 0; i < sliderNumber ;i++ , position++)
        {
            if (position == -2)
            {
                position = 1;
            }
            else if (position == 2) {
                position = -1;
            }
            myEUIs[i].Trans.anchorMin = new Vector2((position) % sliderNumber, 0);
            myEUIs[i].Trans.anchorMax = new Vector2((position + 1) % sliderNumber, 1);
            myEUIs[i].Trans.offsetMax = myEUIs[i].Trans.offsetMin = Vector2.zero;
            myEUIs[i].name = sliderNames[i];
            newEUI[position + 1] = myEUIs[i];
        }
        myEUIs = newEUI;
    }

    protected float enterMouseXPosition = 0;

    public void OnBeginDrag(PointerEventData eventData)
    {
        enterMouseXPosition = eventData.position.x;
        newPosition = Transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Transform.localPosition = NewPostion(eventData.position.x);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Transform.localPosition = NewPostion(enterMouseXPosition);
        Slide((eventData.delta.x));
    }

    Vector2 newPosition = Vector2.zero;

    private Vector2 NewPostion(float _newX) {
        newPosition.x = _newX - enterMouseXPosition;
        return newPosition;
    }

    void Start()
    {
        var e = ElementsUI;

    }
}

using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyUnityUI : Toggle
{
    /// <summary>
    /// 用來記錄額外資訊
    /// </summary>
    public int ExtraData = 0;

    GameObject myGameObject = null;
    /// <summary>
    /// 取得遊戲物件
    /// </summary>
    public GameObject GameObject {
        get {
            if (myGameObject == null) myGameObject = gameObject;
            return myGameObject;
        }
    }
    RectTransform myTrans;
    /// <summary>
    /// 物體的位置
    /// </summary>
    public RectTransform Transform
    {
        get
        {
            if (myTrans == null)
                myTrans = GameObject.GetComponent<RectTransform>();
            return myTrans;
        }
    }

    Image myImage;
    public Image Image {
        get {
            if (myImage == null)
                myImage = GetComponentInChildren<Image>();
            return image; }
    }

    /// 按鈕事件
    public MyDelegate.ButtonDelegate onClick;
    public MyDelegate.ButtonDelegate onEnter;
    public MyDelegate.ButtonDelegate onExit;
    public MyDelegate.ButtonDelegate onDown;
    public MyDelegate.ButtonDelegate onUp;

    /// <summary>
    /// 增加事件
    /// </summary>
    public MyDelegate.ButtonDelegate OnClickEvent {
        set {
            onClick -= value;
            onClick += value;
        }
        get { return onClick; }
    }
    /// <summary>
    /// 增加事件
    /// </summary>
    public MyDelegate.ButtonDelegate OnEnterEvent
    {
        set
        {
            onClick -= value;
            onClick += value;
        }
        get { return onEnter; }
    }
    /// <summary>
    /// 增加事件
    /// </summary>
    public MyDelegate.ButtonDelegate OnExitEvent
    {
        set
        {
            onExit -= value;
            onExit += value;
        }
        get { return onExit; }
    }
    /// <summary>
    /// 增加事件
    /// </summary>
    public MyDelegate.ButtonDelegate OnDownEvent
    {
        set
        {
            onDown -= value;
            onDown += value;
        }
        get { return onDown; }
    }
    /// <summary>
    /// 增加事件
    /// </summary>
    public MyDelegate.ButtonDelegate OnUpEvent
    {
        set
        {
            onUp -= value;
            onUp += value;
        }
        get { return onUp; }
    }

    /// <summary>
    /// 按鈕點擊事件
    /// </summary>
    /// <param name="_eventData"></param>
    public override void OnPointerClick(PointerEventData _eventData)
    {
        base.OnPointerClick(_eventData);
        CallEvent(onClick,_eventData);
    }

    /// <summary>
    /// 滑鼠移進UI事件
    /// </summary>
    /// <param name="_eventData"></param>
    public override void OnPointerEnter(PointerEventData _eventData)
    {
        base.OnPointerEnter(_eventData);
        CallEvent(onEnter, _eventData);
    }

    /// <summary>
    /// 滑鼠離開UI事件
    /// </summary>
    /// <param name="_eventData"></param>
    public override void OnPointerExit(PointerEventData _eventData)
    {
        base.OnPointerExit(_eventData);
        CallEvent(onExit, _eventData);
    }

    /// <summary>
    /// 滑鼠按下按鈕事件
    /// </summary>
    /// <param name="_eventData"></param>
    public override void OnPointerUp(PointerEventData _eventData)
    {
        base.OnPointerUp(_eventData);
        CallEvent(onUp, _eventData);
    }

    /// <summary>
    /// 滑鼠放開按鈕事件
    /// </summary>
    /// <param name="_eventData"></param>
    public override void OnPointerDown(PointerEventData _eventData)
    {
        base.OnPointerDown(_eventData);
        CallEvent(onDown, _eventData);
    }


    /// <summary>
    /// 呼叫_event，如果沒有事件則不呼叫
    /// </summary>
    /// <param name="_event"></param>
    /// <param name="_eventData"></param>
    protected void CallEvent(MyDelegate.ButtonDelegate _event,PointerEventData _eventData) {
        if (_event != null)
            _event.Invoke(ExtraData,_eventData);
    }

    public void Hidden() {
        GameObject.SetActive(false);
    }

    public void Show() {
        GameObject.SetActive(true);
    }
}
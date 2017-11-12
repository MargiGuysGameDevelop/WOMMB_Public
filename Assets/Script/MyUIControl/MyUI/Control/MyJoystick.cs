using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyJoystick : ScrollRect
{
    public int ExtraData = 0;
    /// <summary>
    /// Max Radius
    /// </summary>
	protected float radius = 100;
    /// <summary>
    /// 1/radius
    /// </summary>
    protected float radius1;

    public MyDelegate.JoystickDelegate onDrag;
    public MyDelegate.JoystickDelegate OnDragEvent {
        set {
            onDrag -= value;
            onDrag += value;
        }
        get { return onDrag; }
    }
    public MyDelegate.JoystickDelegate onDragExit;
    public MyDelegate.JoystickDelegate OnDragExit {
        set { onDragExit -= value; onDragExit += value; }
        get { return onDragExit; }
    }

    protected override void Awake()
    {
        base.Awake();


    }

    protected override void Start()
    {
        base.Start();
        OnStart();
    }

    public void OnStart()
    {
        var myRect = GetComponent<RectTransform>();
        //myRect.sizeDelta = new Vector2(myRect.rect.height, 0);
        radius = myRect.rect.height * 0.4f;
        radius1 = 1 / radius;
    }

    public override void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        base.OnDrag(eventData);

        content.position = Input.mousePosition;

        content.anchoredPosition = Vector3.ClampMagnitude(base.content.anchoredPosition, this.radius);

        if (OnDragEvent != null)
        {
            OnDragEvent(base.content.anchoredPosition.x * radius1, base.content.anchoredPosition.y * radius1);
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        if (OnDragExit != null) OnDragExit(0, 0);
    }

    public Vector3 GetRelativePosition()
    {
        return this.content.localPosition.normalized;
    }
}

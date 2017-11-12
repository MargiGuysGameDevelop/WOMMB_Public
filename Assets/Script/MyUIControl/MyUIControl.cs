using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class MyUIControl
{
    /// <summary>
    /// 最底層的UI，包含所需控制項
    /// </summary>
    protected MyUnityUIGroup myRootUI;

    /// <summary>
    /// 相依系統
    /// </summary>
    protected MySystem mySystem;

    public abstract void Start();
    public abstract void Update();

    /// <summary>
    /// 讀取根UI，可以放在Start函式中
    /// </summary>
    protected virtual void LoadRootUI(string _objectName,UIFactory.CanvasType _type) {
        var go = UIFactory.Instance.CreateUI(_objectName, _type);
        myRootUI = go.GetComponent<MyUnityUIGroup>();
    }

    public virtual void Inject(MySystem _system) {
        mySystem = _system;
    }
}


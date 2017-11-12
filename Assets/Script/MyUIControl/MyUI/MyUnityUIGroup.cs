using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 一個UI功能的基礎，通常會掛在該功能最上層的物件上，
/// 包含底下所有的MyUnityUI以利事件存取及管理。
/// 繼承此類別並掛在Prefab上後撰寫新的UIControl邏輯。
/// 
/// </summary>
public class MyUnityUIGroup : MonoBehaviour
{

    /// <summary>
    /// 所有底下的MyUnityUI
    /// (可以搭配自定義ENUM實現直接存取特定UI的功能)
    /// </summary>
    [SerializeField]
    protected List<MyUnityUI> members =new List<MyUnityUI>();



    GameObject myGameObject;
    /// <summary>
    /// 物體本身
    /// </summary>
    public GameObject GameObject {
        get {
            if (myGameObject == null)
                myGameObject = gameObject;
            return myGameObject;
        }
    }
    RectTransform myTrans;
    /// <summary>
    /// 物體的位置
    /// </summary>
    public RectTransform Transform {
        get {
            if (myTrans == null)
                myTrans = GameObject.GetComponent<RectTransform>();
            return myTrans;
        }
    }

    /// <summary>
    /// 手動新增子物件，盡量使用Prefab，不要使用此函式
    /// </summary>
    /// <param name="_unityUI"></param>
    public void AddUIMember(MyUnityUI _unityUI) {
        members.Add(_unityUI);
        _unityUI.GameObject.transform.SetParent(Transform);
    }

    /// <summary>
    /// 手動新增子物件(根物件)
    /// </summary>
    public void AddUIMember(MyUnityUIGroup _root) {
        _root.GameObject.transform.SetParent(Transform);
    }

    #region UI隱藏

    private Canvas canvas;
    //取得Canvas
    public Canvas Canvas
    {
        get
        {
            if (canvas == null)
                canvas = GetComponentInParent<Canvas>();
            return canvas;
        }
    }

    private Vector3 orginPosition = Vector3.zero;
    private Vector3 hidePosition;

    public virtual void Hide()
    {
        if (hidePosition == Vector3.zero)
        {
            //儲存位置
            RectTransform canvasTransform = Canvas.GetComponent<RectTransform>();
            hidePosition = new Vector3(canvasTransform.sizeDelta.x, 0f);
        }
        Transform.localPosition = hidePosition;
    }

    public virtual void Show()
    {
        Transform.localPosition = orginPosition;
    }

    #endregion


    /// <summary>
    /// 存取特定的MyUnityUI
    /// (可以新增Enum去取得對應功能的數字)
    /// </summary>
    /// <param name="_index"></param>
    protected void GetUIByIndex(int _index) {
        if (_index < 0 || _index > members.Count)
            return;
    }

    /// <summary>
    /// 齒輪選單的新功能，用以在Editor中事先找出所有可能會動到的項目
    /// 可以依照排序及其功能新增Enum，使用時搭配GetUIByIndex取得物件
    /// </summary>
    [ContextMenu("找尋所有MyUnityUI")]
    protected void FindAllUIMember()
    {
        members = myGameObject.GetComponentsInChildren<MyUnityUI>().ToList();
    }

    //生命週期
    //
    //
    //
}

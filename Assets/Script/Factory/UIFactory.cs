using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UIFactory
{
    static private UIFactory instance;

    static public UIFactory Instance {
        get {
            if (instance == null)
                instance = new UIFactory();
            return instance;       
        }
    }
    /// <summary>
    /// 場上的物件池
    /// </summary>
    Dictionary<string, MyUnityUIGroup> uiPool =
        new Dictionary<string, MyUnityUIGroup>();

    //將畫面上的Canvas依據刷新頻率分為3類
    //不刷新
    //有時候會刷新
    //常刷新
    MyUnityUIGroup noneRefreshCanvas;
    MyUnityUIGroup usuallyRefreshCanvas;
    MyUnityUIGroup alwaysRefreshCanvas;

    MyUnityUIGroup NoneRefreshCanvas {
        get {
            if (noneRefreshCanvas == null)
            {
                noneRefreshCanvas = GameObject.Instantiate
                    (LoadFactory.Instance.LoadGameObject("Canvas", "UI")
                    .GetComponent<MyUnityUIGroup>());
                //noneRefreshCanvas.isStatic = true;
                noneRefreshCanvas.name = "NoneRefreshCanvas";
                GameObject.DontDestroyOnLoad(noneRefreshCanvas.GameObject);
            }
            return noneRefreshCanvas;
        }
    }

    MyUnityUIGroup UsuallyRefreshCanvas
    {
        get
        {
            if (usuallyRefreshCanvas == null)
            {
                usuallyRefreshCanvas = GameObject.Instantiate
                    (LoadFactory.Instance.LoadGameObject("Canvas", "UI")
                    .GetComponent<MyUnityUIGroup>());
               // usuallyRefreshCanvas.isStatic = true;
                usuallyRefreshCanvas.name = "UsuallyRefreshCanvas";
                GameObject.DontDestroyOnLoad(usuallyRefreshCanvas.GameObject);
            }
            return usuallyRefreshCanvas;
        }
    }

    MyUnityUIGroup AlwaysRefreshCanvas
    {
        get
        {
            if (alwaysRefreshCanvas == null)
            {
                alwaysRefreshCanvas = GameObject
                    .Instantiate(LoadFactory.Instance.
                    LoadGameObject("Canvas","UI").GetComponent<MyUnityUIGroup>());
                //alwaysRefreshCanvas.isStatic = true;
                alwaysRefreshCanvas.name = "AlwaysRefreshCanvas";
                GameObject.DontDestroyOnLoad(alwaysRefreshCanvas.GameObject);
            }
            return alwaysRefreshCanvas;
        }
    }

    /// <summary>
    /// 此UI刷新頻率
    /// </summary>
    public enum CanvasType :int {
        NoneRefresh,
        UsuallyRefresh,
        AlwaysRefresh
    }


    /// <summary>
    /// 將已經召喚的物件納入物件池中
    /// </summary>
    /// <param name="_ui"></param>
    /// <returns></returns>
    public bool LoginUI(MyUnityUIGroup _ui,CanvasType _type) {
        _ui.Transform.SetParent(GetCanvasByFrequence(_type).Transform);
        if (uiPool.ContainsValue(_ui))
            return false;
        uiPool.Add(_ui.name,_ui);
        return true;
    }

    /// <summary>
    /// 從檔案新增UI至場上
    /// </summary>
    /// <param name="_name"></param>
    /// <returns></returns>
    public GameObject CreateUI(string _name,CanvasType _type) {
        return CreateUI(_name,GetCanvasByFrequence(_type).Transform);
    }

    public GameObject CreateUI(string _name,RectTransform _parent) {
        MyUnityUIGroup group = FindUI(_name);
        if (group!=null) {
            Debug.LogError("沒有該UI的資料! : "+_name);
            return group.GameObject;
        }
        //從UI資料夾中取得資料
        GameObject go = LoadFactory.Instance.LoadAndInstantiateGameObject(_name,"UI");
        if (go != null)
        {
            go.SetActive(true);
            MyUnityUIGroup ui = go.GetComponent<MyUnityUIGroup>();
            if (ui != null)
            {
                ui.Transform.SetParent(_parent);
                ui.Transform.sizeDelta = Vector2.zero;
                ui.Transform.localScale = new Vector3(1.0f,1.0f,1.0f);
                ui.Transform.localPosition = Vector3.zero;

                ui.Transform.anchoredPosition = Vector3.zero;
                uiPool.Add(_name, ui);
            }
        }
        return go;
    }

    public MyUnityUIGroup FindUI(string _name) {
        if (uiPool.ContainsKey(_name))
            return uiPool[_name];
        return null;
    }

    /// <summary>
    /// 設定UI所屬的Canvas
    /// </summary>
    /// <param name="_ui"></param>
    /// <param name="_type"></param>
    private MyUnityUIGroup GetCanvasByFrequence(CanvasType _type) {
        switch (_type) {
            case CanvasType.AlwaysRefresh:
                return AlwaysRefreshCanvas;
            case CanvasType.UsuallyRefresh:
                return UsuallyRefreshCanvas;
            case CanvasType.NoneRefresh:
                return NoneRefreshCanvas;
        }
        return UsuallyRefreshCanvas;
    }
}
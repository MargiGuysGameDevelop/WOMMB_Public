using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[SerializeField]
public class MeatBallSetFactory 
{
    /// <summary>
    /// 套裝列表
    /// </summary>
    public enum SetType {
        /// <summary>
        /// 原始套裝，一開始就會有
        /// </summary>
        Orgin,
        /// <summary>
        /// 戰士套裝，可以在貢貢村或是登入選單獲得
        /// </summary>
        Warrior,
        /// <summary>
        /// 弓手套裝，可以在貢貢村或是登入選單獲得
        /// </summary>
        Archer,
        /// <summary>
        /// 鐵匠套裝，可以在貢貢村或是登入選單獲得
        /// </summary>
        BlackSmith
    }

    static private MeatBallSetFactory instance = null;
    static public MeatBallSetFactory Instance {
        get {
            if (instance == null)
                instance = new MeatBallSetFactory();
            return instance;
        }
    }



    /// <summary>
    /// 存放未使用套裝的物件名稱
    /// </summary>
    private string RootObjectName = "SuitManager";
    /// <summary>
    /// 存放未使用套裝的物件
    /// </summary>
    private Transform rootTransform;

    public MeatBallSetFactory() {
        //管理器物件
        GameObject go = GameObject.Instantiate(new GameObject()) as GameObject;
        if (go != null)
        {
            rootTransform = go.transform;
            go.name = RootObjectName;
            GameObject.DontDestroyOnLoad(go);
        }
        //從資料夾找物件
    }

    //資料池,尚未實例化
    Dictionary<SetType, MeatBallSet> dataPool = 
        new Dictionary<SetType, MeatBallSet>();
    //物件池，存在場上的物件
    Dictionary<SetType, List<MeatBallSet>> objectPool =
        new Dictionary<SetType, List<MeatBallSet>>();


    /// <summary>
    /// 從資源檔案取得套裝
    /// </summary>
    /// <param name="_setName"></param>
    /// <returns></returns>
    public MeatBallSet GetSet(SetType _set) {

        MeatBallSet set  = null;
        if (!dataPool.ContainsKey(_set))
        {
            ScriptableObject so = LoadFactory.Instance.LoadScriptableObject(_set.ToString(), "MeatBall","Set");
            if (so == null)
                throw new Exception(_set + " : This Name is no match any Set.");
            set = so as MeatBallSet;
            //給予資料池此物件
            dataPool.Add(_set, set);
            //遊戲物件池也初始化但沒給值
            objectPool.Add(_set, new List<MeatBallSet>());
        }
        else {
            set = dataPool[_set];
        }
        return set;
    }

    /// <summary>
    /// 取得空的遊戲內套裝
    /// </summary>
    /// <returns></returns>
    public MeatBallSet GetSetObject(SetType _setName) {
        MeatBallSet set = null;
        //確認資料池中是否已經有該套裝
        if (dataPool.ContainsKey(_setName))
        {
            //確認散件是否在使用中
            for (int i = 0; i < objectPool[_setName].Count; i++)
            {
                if (objectPool[_setName][i].IsPartUsing)
                {
                    continue;
                }
                //如果沒有任何一個在使用中的話在初始化函式後直接回傳
                objectPool[_setName][i].ReuseAllParts();
                return objectPool[_setName][i];
            }
        }
        set = GameObject.Instantiate(GetSet(_setName));
        set.FindPartsSection();
        set.InstantiateParts();
        set.ReuseAllParts();
        objectPool[_setName].Add(set);
        return set;
    }

    /// <summary>
    /// 取得套裝散件
    /// </summary>
    /// <param name="_setName"></param>
    /// <param name="_type"></param>
    /// <returns></returns>
    protected MBSetPart GetPartGameObject(SetType _setName,MeatBall.SetPartType _type) {
        //如果原資料就沒有該部位了就回傳null
        if (dataPool[_setName].GetPartByEnum(_type) == null)
            return null;
        Debug.Log("Search Suit");
        MBSetPart part=null;
        //尋找該Dictionary
        for (int i = 0; i < objectPool[_setName].Count; i++) {
            //找出該套裝
            part = objectPool[_setName][i].GetPartByEnum(_type);
            //如果此物件的該部位正在被使用就繼續找別的
            if (part.IsThisUsing)
            {
                part = null;
                continue;
            }
            //沒在使用就指定該物件
            break;
        }
        //整場物件都沒有空的部位就造新的套裝再取其物件
        if (part == null)
        {
            MeatBallSet newSet = GameObject.Instantiate(GetSet(_setName)) as MeatBallSet;
            objectPool[_setName].Add(newSet);
            newSet.InstantiateParts();
            part = newSet.GetPartByEnum(_type);
        }
        Debug.Log(part.name);
        //事前準備
        part.ReuseGameObject();
        //回傳該物件
        return part;
    }

    /// <summary>
    /// 取得場上沒有被使用的套服
    /// </summary>
    /// <param name="_setName">套裝名稱</param>
    /// <returns></returns>
    public MBSetSuit GetSuit(SetType _setName) {
        return GetPartGameObject(_setName, MeatBall.SetPartType.Suit) as MBSetSuit;
    }
    /// <summary>
    /// 取得場上沒有被使用的手套
    /// </summary>
    /// <param name="_setName">套裝名稱</param>
    /// <returns></returns>
    public MBSetGlove GetGlove(SetType _setName) {
        return GetPartGameObject(_setName, MeatBall.SetPartType.Glove) as MBSetGlove;
    }
    /// <summary>
    /// 取得場上沒有被使用的帽子
    /// </summary>
    /// <param name="_setName">套裝名稱</param>
    /// <returns></returns>
    public MBSetHat GetHat(SetType _setName) {
        return GetPartGameObject(_setName,MeatBall.SetPartType.Hat) as MBSetHat;
    }
    /// <summary>
    /// 取得場上沒有被使用的武器
    /// </summary>
    /// <param name="_setName">套裝名稱</param>
    /// <returns></returns>
    public MBSetWeapon GetWeapon(SetType _setName) {
        return GetPartGameObject(_setName,MeatBall.SetPartType.Weapon) as MBSetWeapon;
    }

    /// <summary>
    /// 將套件脫離人物
    /// </summary>
    /// <param name="_part"></param>
    public void RecyclePart(MBSetPart _part) {
        if (_part.Sections == null)
            return;
        _part.RecycleGameObject(rootTransform);
    }
}
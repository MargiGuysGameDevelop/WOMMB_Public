using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLoginGroup : MyUnityUIGroup {

    /// <summary>
    /// 初始可供選擇的套裝
    /// </summary>
    MeatBallSetFactory.SetType[] initialSetList = {
        MeatBallSetFactory.SetType.Warrior,
        MeatBallSetFactory.SetType.Archer,
        MeatBallSetFactory.SetType.BlackSmith
    };
     
    /// <summary>
    /// 用以顯示套裝們
    /// </summary>
    [SerializeField]MyUIElementsPresent SetSelectUIs;

    /// <summary>
    /// 直接變更套裝列表
    /// </summary>
    public List<MyUIElement> SetSelectElements {
        set { SetSelectUIs.Elements = value; }
    }

    /// <summary>
    /// 開始讀取套裝資料與其事件
    /// </summary>
    public void LoadData() {
        List<MyUIElement> list = new List<MyUIElement>();
        for (int i=0;i<initialSetList.Length ;i++)
        {
            MeatBallSet set = MeatBallSetFactory.Instance.GetSet(initialSetList[i]);
            MyUIElement setElement = set.NewElement;
            setElement.ButtonClickEvent = (_index, _extraData) =>
            {
                GameLogicManager.Instance.ChangeMainMeatBallSet(
                    MeatBallSetFactory.Instance.GetSetObject(set.Type));
            };
            list.Add(setElement);
        }
        SetSelectElements = list;
    }
}

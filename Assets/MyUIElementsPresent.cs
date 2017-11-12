using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUIElementsPresent : MonoBehaviour {

    public enum UITrans {
        ///UI元件往Y軸延伸
        ExpandY,
        ///UI元件往X軸
        ExpandX,
        /// <summary>
        /// UI元件往兩軸延伸
        /// </summary>
        ExpandBoth,
        /// <summary>
        /// 外框不動，UI會自動調整尺寸 
        /// </summary>
        Fixed
    }

    protected List<MyUIElement> myElements;

    public UITrans Type = UITrans.ExpandBoth;

    public GameObject Base;

    int rowNumbers = 5;

    public RectTransform ParentRectTransform;

    public List<MyUIElementPresent> MyUIElements = new List<MyUIElementPresent>();

    public List<MyUIElement> Elements {
        set {
            if (value == null || value.Count == 0)
                return;
            if (Base == null)
            {
                Debug.LogWarning("MyUIElementsPresent:Base is null");
                return;
            }
            if (ParentRectTransform == null)
            {
                Debug.LogWarning("MyUIElementsPresent:ParentRectTransform is null");
                return;
            }

            RectTransform baseRect = Base.GetComponent<RectTransform>();
            Vector2 newUISize = Vector2.zero;

            //決定單一UI的尺寸
            switch (Type) {
                case UITrans.ExpandX:
                case UITrans.ExpandY:
                case UITrans.ExpandBoth:
                    newUISize.x = baseRect.sizeDelta.x;
                    newUISize.y = baseRect.sizeDelta.y;
                    break;
                case UITrans.Fixed:
                    throw new System.NotImplementedException();
            }

            //開啟UI原型供複製用
            Base.SetActive(true);

            //資料
            myElements = value;

            //計算位置尺寸
            Vector3 newParentRight = ParentRectTransform.right;
            float xPosition = 0, yPosition = 0;
            //將UI製造出來後填充資料
            for (int i=0;i< myElements.Count ;i++) {
                //如果需要超出原本UI數量則新增UI
                if(i>= MyUIElements.Count)
                    MyUIElements.Add(Instantiate(Base).GetComponentInChildren<MyUIElementPresent>());
                //指定父層並將位置縮放歸0
                MyUIElements[i].Transform.SetParent(ParentRectTransform);
                MyUIElements[i].Transform.anchoredPosition = Vector2.zero;
                MyUIElements[i].Transform.localScale = new Vector3(1f,1f,1f);
                //取該UI位置算該位置
                MyUIElements[i].Transform.sizeDelta = new Vector2(newUISize.x,newUISize.y);
                MyUIElements[i].Transform.localPosition = new Vector3(xPosition,yPosition,0f);
                MyUIElements[i].Transform.name = myElements[i].Name;
                //只要不是僅有Y軸延伸就將X位置偏移
                if (Type != UITrans.ExpandY)
                {
                    xPosition += newUISize.x;
                }
                //只要不是僅有X軸延伸或雙向伸展遇到換行條件時，就將Y軸偏移
                if (Type != UITrans.ExpandX ||  (Type == UITrans.ExpandBoth&&i%rowNumbers==0))
                {
                    yPosition += newUISize.y;
                }
                //給予資料
                MyUIElements[i].Element = myElements[i];
            }
            //如果是固定模式則不變動視窗尺寸
            if(Type != UITrans.Fixed)
                ParentRectTransform.right = newParentRight;

            //隱藏UI原型
            Base.SetActive(false);
        }
    }
        
}

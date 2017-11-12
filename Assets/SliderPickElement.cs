using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderPickElement : MonoBehaviour {

    #region 欄位
    public Image imgIcon;
    public Text txtName;
    public Text txtContent;

    private RectTransform myTrans;
    public RectTransform Trans {
        get {
            if (myTrans == null) 
                myTrans = GetComponent<RectTransform>();
            return myTrans;
        }
    }

    private MyUIElement myElement;
    public MyUIElement Element {
        set {
            myElement = value;
            imgIcon.sprite = myElement.Icon;
            txtName.text = myElement.Name;
            txtContent.text = myElement.Content;
        }
    }

    #endregion

    #region 生命週期
    void Start() {
        /*抓取值*/
        if (imgIcon == null)
            imgIcon = GetComponentInChildren<Image>();
        if (txtName == null || txtContent == null)
        {
            var texts = GetComponentsInChildren<Text>();
            txtName = texts[0];
            txtContent = texts[1];
        }

        //計算UI尺寸
        float thisHeight = Trans.sizeDelta.y;
        //icon
        var iconTrans = imgIcon.GetComponent<RectTransform>();
        iconTrans.sizeDelta = new Vector2(thisHeight,thisHeight);
        //文字說明的共同最小偏移量
        Vector2 offset = new Vector2(thisHeight, 0);
        //name
        var nameTrans = txtName.GetComponent<RectTransform>();
        nameTrans.offsetMin = offset;
        //content
        var contentTrans = txtContent.GetComponent<RectTransform>();
        contentTrans.offsetMin = offset;
    }

    #endregion

}

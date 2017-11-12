using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyUIFigureState : MyUnityUIGroup,FigureValueChangeListener {

    [SerializeField]
    private GameObject Base;

    [SerializeField]
    private RectTransform Buff;

    [SerializeField]
    private Slider HP;

    [SerializeField]
    private GameObject[] Buffers = new GameObject[10];
    [SerializeField]
    private int columns = 5;
    private MyUIElementPresent[] ElementsUI = new MyUIElementPresent[10];
    /// <summary>
    /// 資料多的時候由此儲存供替補
    /// </summary>
    private List<MyUIElement> moreBuff = new List<MyUIElement>();

    /// <summary>
    /// 新增BUFF，回傳該BUFF的位置。
    /// </summary>
    /// <param name="_element"></param>
    /// <returns></returns>
    public void AddBuff(MyUIElement _element) {
        int position = 0;
        //尋找陣列
        for (; position < Buffers.Length; position++) {
            //無資料的位置
            if (ElementsUI[position] == null) {
                //新增物件
                if (Buffers[position] == null) {
                    Buffers[position] = GameObject.Instantiate(Base);
                    Buffers[position].SetActive(true);
                    var trans = Buffers[position].GetComponent<RectTransform>();
                    trans.localPosition = new Vector3(
                        trans.sizeDelta.x * position%columns,
                        trans.sizeDelta.y * position/columns,
                        0);
                }
                //新增資料
                ElementsUI[position] = Buffers[position].GetComponent<MyUIElementPresent>();
                ElementsUI[position].Element = _element;
            }
        }
        //溢位
        if (position == Buffers.Length) {
            moreBuff.Add(_element);
        }    
    }

    public void DeleteBuff(MyUIElement _element) {
        int position = 0;
        for (;position<Buffers.Length ;position++) {
            if (ElementsUI[position].Element == _element) {
                ElementsUI[position].Element = null;
                Buffers[position].SetActive(false);
                break;
            }
        }
        if (position == Buffers.Length) {
            for (int i = 0; i < moreBuff.Count; i++) {
                if (moreBuff[position] == _element) {
                    moreBuff[position] = null;
                    break;
                }
            }
        }
        for (int index = 0; index < moreBuff.Count; index++) {
            if (moreBuff[index] != null) {
                moreBuff.RemoveAt(index);
                for (int j=0;j<Buffers.Length ;j++) {
                    if (ElementsUI[j] == null) {
                        ElementsUI[j].Element = _element;
                    }
                }
            }
        }
    }

    public void HPChange(int _hp)
    {
        HP.value = _hp;
    }

    public void SpeedChange(float _times)
    {}

    public void CriticleTimesChange(float _times)
    {}

    public void SkillChange(Skill _skill)
    {}

    public void BindingFigure(Figure _figure) {
        _figure.AddValueListener = this;
        HP.maxValue = _figure.Value.HPMax;
        HP.value = _figure.Value.HP;
    }


    void Awake()
    {
        //註冊至常刷新Canvas
        UIFactory.Instance.LoginUI(this, UIFactory.CanvasType.AlwaysRefresh);

    }
}

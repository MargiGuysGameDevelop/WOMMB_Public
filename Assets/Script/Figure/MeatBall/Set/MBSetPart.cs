using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public class MBSetPart : ScriptableObject
{
    public enum BodyPart {
        RWeapon,
        LWeapon,
        RGlove,
        LGlove,
        LShoes,
        RShoes,
        Cloak,
        Hat,
        Suit
    }

    /// <summary>
    /// 套裝名稱，應與Set相同
    /// </summary>
    public string SetName;
    /// <summary>
    /// 套裝對應的遊戲物件
    /// </summary>
    [SerializeField]private GameObject[] sections = 
        new GameObject[Enum.GetValues(typeof(MeatBall.BodyPart)).Length];
    public GameObject[] Sections {
        get {
            if (sections.Length !=
                Enum.GetValues(typeof(MeatBall.BodyPart)).Length)
                sections = new GameObject[Enum.GetValues(typeof(MeatBall.BodyPart)).Length];
            return sections;
        }
    }
    /// <summary>
    /// 技能，當貢丸穿上後會有該技能
    /// </summary>
    public Skill Skill;

    private Transform myTrans = null;

    /// <summary>
    /// 將遊戲物件回收
    /// </summary>
    public void RecycleGameObject(Transform _parent) {
        for (int i = 0; i < Sections.Length; i++)
        {
            if (Sections[i] == null)
                continue;
            Sections[i].transform.SetParent(_parent);
            Sections[i].SetActive(false);
            this.IsThisUsing = false;
        }
    }

    /// <summary>
    /// 將遊戲物件再利用前的準備
    /// </summary>
    public void ReuseGameObject() {
        for (int i = 0; i < Sections.Length; i++)
        {
            if (Sections[i] == null)
                continue;
            Sections[i].SetActive(true);
            this.IsThisUsing = true;
        }
    }

    /// <summary>
    /// 是不是正在被使用
    /// </summary>
    public bool IsThisUsing = false;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_object"></param>
    public void Initial(string _name) {
        SetName = _name;
    }

    /// <summary>
    /// 初始化，會將遊戲物件都實例化
    /// </summary>
    public void Instantiate() {
        for (int i = 0; i < Sections.Length; i++) {
            if (Sections[i] == null)
                continue;
            Sections[i].name = name;
            Sections[i] = GameObject.Instantiate(Sections[i]);
        }
        if (Skill != null)
        {
            Skill = Instantiate(Skill);
            Skill.Index = GetSkillIndex();
        }
    }

    /// <summary>
    /// 回傳該套會有的部位
    /// </summary>
    /// <returns></returns>
    public virtual BodyPart[] GetSelectionsEnum() { throw new Exception("此部位回傳衣物部位的函式尚未實做"); }

    /// <summary>
    /// 從套裝檔案找到各部位
    /// </summary>
    /// <param name="_setObject"></param>
    public virtual void FindPartsFormFile(GameObject _setObject) {
        BodyPart[] parts = GetSelectionsEnum();
        Transform[] trans = _setObject.GetComponentsInChildren<Transform>();
        for (int i = 0; i < trans.Length; i++)
        {
            for (int j = 0; j < parts.Length; j++)
            {
                string name = parts[j].ToString();
                if (name == trans[i].name)
                    Sections[(int)parts[j]] = trans[i].gameObject;
            }
        }
    }

    /// <summary>
    /// 取得技能欄位
    /// </summary>
    /// <returns></returns>
    public virtual int GetSkillIndex() {
        return 0;
    }
}
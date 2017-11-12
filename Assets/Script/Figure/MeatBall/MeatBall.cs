using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class MeatBall : Figure
{

    /// <summary>
    /// 顏色種類
    /// </summary>
    public enum ColorType {
        Dirty
    }

    /// <summary>
    /// 技能/套裝對應排序
    /// </summary>
    public enum SetPartType {
        Hat,
        Glove,
        Suit,
        Weapon
    }

    /// <summary>
    /// 貢丸各套件的父物件名稱
    /// </summary>
    public enum BodyPart
    {
        weapon_IK_R,
        weapon_IK_L,
        hand_R_end,
        hand_L_end,
        feet_L,
        feet_R,
        cloak,
        hat,
        body
    }

    [SerializeField]
    protected MBSetSuit suit = null;
    [SerializeField]
    protected MBSetWeapon weapon = null;
    [SerializeField]
    protected MBSetGlove glove = null;
    [SerializeField]
    protected MBSetHat hat = null;

    /// <summary>
    /// 套裝
    /// </summary>
    public MBSetSuit Suit {
        set {
            SetPartsData(value, SetPartType.Suit);
            suit = value;
        }
        get { return suit; }
    }
    /// <summary>
    /// 帽子
    /// </summary>
    public MBSetHat Hat
    {
        set
        {
            SetPartsData(value, SetPartType.Hat);
            hat = value;
        }
        get { return hat; }
    }
    /// <summary>
    /// 手套
    /// </summary>
    public MBSetGlove Glove
    {
        set
        {
            SetPartsData(value, SetPartType.Glove);
            glove = value;
        }
        get { return glove; }
    }
    /// <summary>
    /// 武器
    /// </summary>
    public MBSetWeapon Weapon
    {
        set
        {
            SetPartsData(value, SetPartType.Weapon);
            weapon = value;
            AttackNumber = weapon.AttackNumber;
        }
        get { return weapon; }
    }

    [NonSerialized]
    new private MeatBallBehavior myBehavior;

    public MeatBall(FigureBuilder _builder) : base(_builder)
    {
        MeatBallBuilder builder = _builder as MeatBallBuilder;

        myBehavior = _builder.GameObjectOnScene.GetComponent<MeatBallBehavior>();
        myBehavior.myMeatBallFigure = this;

        if (builder == null || !(builder is MeatBallBuilder))
            SetSet(MeatBallSetFactory.Instance.GetSetObject(MeatBallSetFactory.SetType.Orgin));
        else
            SetSet(MeatBallSetFactory.Instance.GetSetObject(builder.Set));
    }

    /// <summary>
    /// 設定顏色
    /// </summary>
    /// <param name="_color"></param>
    public void SetColor(ColorType _color) {

    }

    /// <summary>
    /// 更換整個套裝
    /// </summary>
    /// <param name="_set"></param>
    public void SetSet(MeatBallSet _set) {
        //設定AnimatorOoverrideController
        if(_set.AOC!=null)
            ChangeAOC(_set.AOC);
        //設定套裝物件
        Suit = _set.Suit;
        Weapon = _set.Weapon;
        Hat = _set.Hat;
        Glove = _set.Glove;
    }

    /// <summary>
    /// 替換部件
    /// </summary>
    /// <param name="_part">已經實例化的套件</param>
    /// <param name="_type">套件種類</param>
    private void SetPartsData(MBSetPart _part,SetPartType _type) {
        int partIndex = (int)_type;
        //回收舊部件
        if (GetSetPartByEnum(_type) != null)
        {
            MeatBallSetFactory.Instance.RecyclePart(GetSetPartByEnum(_type));
        }
        //資料

        //技能
        Skill newSkill = _part.Skill;
        skills[(int)_type] = newSkill;
        SkillChange(newSkill);
        //遊戲物件
        BodyPart[] parts = Enum.GetValues(typeof(BodyPart)) as BodyPart[];
        Transform buffer = null;
        for (int i=0;i<parts.Length ;i++) {
            if (_part.Sections[i] == null )
                continue;
            if (myBehavior.BodyParts[i] == null)
                throw new Exception("沒有指定貢丸穿戴套裝物件的物件,檢查一下MeatBallBehavior的BodyParts");
            buffer = _part.Sections[i].transform;
            buffer.SetParent(myBehavior.BodyParts[i].transform);
            buffer.localPosition = Vector3.zero;
            buffer.localRotation = Quaternion.Euler(Vector3.zero);
        }
        
    }

    private MBSetPart GetSetPartByEnum(SetPartType _type) {
        switch (_type)
        {
            case SetPartType.Suit:
                return Suit;
            case SetPartType.Hat:
                return Hat;
            case SetPartType.Glove:
                return Glove;
            case SetPartType.Weapon:
                return Weapon;
        }
        return null;
    }
}
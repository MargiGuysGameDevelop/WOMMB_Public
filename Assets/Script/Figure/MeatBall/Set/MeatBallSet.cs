using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName ="Set", menuName = "ScriptableObject/MeatBall/Set")]
public class MeatBallSet : ScriptableObject
{
    public string Name;

    public string Description = "";

    public Sprite Icon;

    [SerializeField]
    public MeatBallSetFactory.SetType Type;

    /// <summary>
    /// 用手拉，裏面要有所有該套裝該有的部位
    /// </summary>
    [Header("用手拉，裏面要有所有該套裝該有的部位")]
    public GameObject SetGameObject;

    [Header("用手拉，裏面會有該覆蓋的動作")]
    public AnimatorOverrideController AOC;

    [Space(order = 30)]

    public MBSetSuit Suit;
    public MBSetWeapon Weapon;
    public MBSetGlove Glove;
    public MBSetHat Hat;

    public bool IsPartUsing {
        get { return (!(Suit.IsThisUsing) &&
                        (Weapon.IsThisUsing) &&
                        (Glove.IsThisUsing) &&
                        (Hat.IsThisUsing)); }
    }

    /// <summary>
    /// 取得套件
    /// </summary>
    /// <param name="_partType"></param>
    /// <returns></returns>
    public MBSetPart GetPartByEnum(MeatBall.SetPartType _partType) {
        MBSetPart part = null;
        switch (_partType)
        {
            case MeatBall.SetPartType.Glove:
                part = this.Glove;
                break;
            case MeatBall.SetPartType.Suit:
                part = this.Suit;
                break;
            case MeatBall.SetPartType.Hat:
                part = this.Hat;
                break;
            case MeatBall.SetPartType.Weapon:
                part = this.Weapon;
                break;
            default:
                break;
        }
        part.Initial(Name);
        return part;
    }


    public void InstantiateParts() {
        Suit = GameObject.Instantiate(Suit);
        Suit.Instantiate();
        Hat = GameObject.Instantiate(Hat);
        Hat.Instantiate();
        Glove = GameObject.Instantiate(Glove);
        Glove.Instantiate();
        Weapon = GameObject.Instantiate(Weapon);
        Weapon.Instantiate();
    }

    [ContextMenu("尋找部位遊戲物件")]
    public void FindPartsSection() {
        Suit.FindPartsFormFile(SetGameObject);
        Weapon.FindPartsFormFile(SetGameObject);
        Hat.FindPartsFormFile(SetGameObject);
        Glove.FindPartsFormFile(SetGameObject);
    }

    public MyUIElement NewElement {
        get {
            return new MyUIElement()
            {
                Name = this.Name,
                Icon = this.Icon,
                Content = this.Description
            };
        }
    }
    /// <summary>
    /// 當全部的物件都要被使用前呼叫的函式
    /// </summary>
    public void ReuseAllParts() {
        Suit.ReuseGameObject();
        Hat.ReuseGameObject();
        Glove.ReuseGameObject();
        Weapon.ReuseGameObject();
    }
    /// <summary>
    /// 回收全部的物件前呼叫,要輸入回收者物件
    /// </summary>
    public void RecycleAllParts(Transform _parent) {
        Suit.RecycleGameObject(_parent);
        Hat.RecycleGameObject(_parent);
        Glove.RecycleGameObject(_parent);
        Weapon.RecycleGameObject(_parent);

    }
}
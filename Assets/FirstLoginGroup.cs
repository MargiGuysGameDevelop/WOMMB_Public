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
    public List<MyUIElementData> SetSelectElements {
        set { SetSelectUIs.Elements = value; }
    }
    /// <summary>
    /// 技能圖式管理者
    /// </summary>
    [SerializeField]private MySkillUIPresent SkillsUI;

    /// <summary>
    /// 開始遊戲的按鈕
    /// </summary>
    public MyUnityUI BtnStartGame;

    public override void Show()
    {
        base.Show();
    }

    /// <summary>
    /// 開始讀取套裝資料與其事件
    /// </summary>
    public void LoadData() {
        List<MyUIElementData> list = new List<MyUIElementData>();
        for (int i=0;i<initialSetList.Length ;i++)
        {
            MeatBallSet set = MeatBallSetFactory.Instance.GetSet(initialSetList[i]);
            MyUIElementData setElement = set.NewElement;
            setElement.ButtonClickEvent = (_index, _extraData) =>
            {
                //給貢丸換套裝
                GameLogicManager.Instance.ChangeMainMeatBallSet(
                    MeatBallSetFactory.Instance.GetSetObject(set.Type));
                //登入下方按鈕填入技能
                MyUIElementData hatElement = null;
                MyUIElementData suitElement = null;
                MyUIElementData gloveElement = null;
                MyUIElementData weaponElement = null;
                if (set.Hat.Skill != null)
                {
                    hatElement = set.Hat.Skill.NewElementData;
                }
                if (set.Suit.Skill!=null) {
                    suitElement = set.Suit.Skill.NewElementData;
                }
                if (set.Glove.Skill != null)
                {
                    gloveElement = set.Glove.Skill.NewElementData;
                }
                if (set.Weapon.Skill != null)
                {
                    weaponElement = set.Weapon.Skill.NewElementData;
                }

                SkillsUI.ChangeSkill((int)MeatBall.SetPartType.Hat, hatElement);
                SkillsUI.ChangeSkill((int)MeatBall.SetPartType.Suit, suitElement);
                SkillsUI.ChangeSkill((int)MeatBall.SetPartType.Glove, gloveElement);
                SkillsUI.ChangeSkill((int)MeatBall.SetPartType.Weapon, weaponElement);

            };
            list.Add(setElement);
        }
        SetSelectElements = list;
    }


}

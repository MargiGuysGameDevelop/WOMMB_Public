using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// 技能按鈕列表
/// 使用Skills指派技能後會於SkillsUI處顯示技能
/// 如果Persents的樣本有圖片就會呈現技能圖片、有標題就會呈現標題....等等
/// </summary>
[Serializable]public class MySkillUIPresent
{
    /// <summary>
    /// 技能數量,當指派技能超過此數量就無效(通常為4)
    /// </summary>
    [SerializeField]
    private int skillNumber = 4;
    /// <summary>
    /// 技能UI呈現(圖示)
    /// </summary>
    public MyUIElementPresent[] Persents;


    private Skill[] skills;
    /// <summary>
    /// (初始化)
    /// 技能列表(使用此欄位初始化僅會有技能資料，無法給與事件)
    /// </summary>
    public Skill[] Skills {
        get { return skills; }
        set {
            for (int i=0;i<value.Length&&i<skillNumber ;i++) {
                if (Persents[i] == null)
                    Debug.LogWarning("Persents UI少於技能數量");
                else
                    Persents[i].Element = value[i].NewElementData;
            }
            skills = value;
        }
    }
    /// <summary>
    /// (初始化)
    /// 直接給與技能資料
    /// 可以在給與此物件資料前先行註冊事件
    /// </summary>
    public List<MyUIElementData> SkillsDatas {
        set {
            for (int i = 0; i < value.Count && i < skillNumber; i++)
            {
                if (Persents[i] == null)
                    Debug.LogWarning("Persents UI少於技能數量");
                else
                    Persents[i].Element = value[i];
            }
        }
    }

    /// <summary>
    /// 更改該位置的技能，無事件僅資料
    /// </summary>
    /// <param name="_index"></param>
    /// <param name="_skill"></param>
    public void ChangeSkill(int _index,Skill _skill) {
        if (_index >= Persents.Length)
            return;
        ChangeSkill(_index,_skill.NewElementData);
    }

    /// <summary>
    /// 直接更改該位置的技能資料
    /// </summary>
    /// <param name="_index"></param>
    /// <param name="_data"></param>
    public void ChangeSkill(int _index, MyUIElementData _data)
    {
        if (_index >= Persents.Length)
            return;
        Persents[_index].Element = _data;
    }

}


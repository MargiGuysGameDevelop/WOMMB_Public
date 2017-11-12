using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName ="Skill",menuName ="ScriptableObject/Figure/Skill")]
public class Skill :ScriptableObject {
    
    public string SkillName {
        get { return name; }
    }

    [HideInInspector]
    public int Index = 0;

    public Sprite Icon;

    [Header("暫時性地覆蓋動作。")]
    public AnimatorOverrideController AOC;

    [Header("技能動作數")]
    public int SkillNumber = 1;

    [Header("當技能動作中有LaunchProjection函式時會實體化出來的物件")]
    public GameObject[] Projections = new GameObject[0];


    public enum AnimationPlay {
        /// <summary>
        /// 自動繼續動作
        /// </summary>
        Auto,
        /// <summary>
        /// 玩家放開按鈕時
        /// </summary>
        ExitButton,
        /// <summary>
        /// 永不停止直至死亡
        /// </summary>
        None
    }

    [Header("動作是否自動撥放")]
    public AnimationPlay AnimationPlayType = AnimationPlay.Auto ;
}

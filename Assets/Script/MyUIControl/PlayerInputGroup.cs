using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerInputGroup : MyUnityUIGroup, FigureValueChangeListener
{
    //UIs
    public MyJoystick Movement;
    public MyUnityUI Attack;
    [SerializeField]
    private int skillNumber = 4;
    public RectTransform SkillUIRoot;
    public MyUnityUI[] Skills;

    /// <summary>
    /// 更改技能按鈕
    /// </summary>
    [ContextMenu("技能變更")]
    public void RefreshSkillIcons() {
        int _numbers = skillNumber;
        if (_numbers > 4 && _numbers < 0)
            return;
        //隱藏多餘按鈕
        for (int index = _numbers; index < Skills.Length; index++) {
            Skills[index].Hidden();
        }

    }

    public void ChangeSkillIcons(Sprite _icon,int _index) {
        if (Skills == null)
        {
            MyLog.Log("Null Skill Button");
        }
        Skills[_index].image.sprite = _icon;
    }

    void Awake() {
        //註冊至常刷新Canvas
        UIFactory.Instance.LoginUI(this, UIFactory.CanvasType.AlwaysRefresh);
    }

    public void HPChange(int _hp)
    {
        
    }

    public void SpeedChange(float _times)
    {
        
    }

    public void CriticleTimesChange(float _times)
    {
        
    }

    /// <summary>
    /// 當Skill的AOC尚未定義時則
    /// </summary>
    /// <param name="_skill"></param>
    public void SkillChange(Skill _skill)
    {
        if (_skill == null)
            return;
        if ( _skill.AOC == null)
        {
            Skills[_skill.Index].Hidden();
            return;
        }
        Skills[_skill.Index].Image.sprite = _skill.Icon;
    }
}
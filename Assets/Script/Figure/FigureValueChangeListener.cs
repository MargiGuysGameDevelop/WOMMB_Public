using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;

public interface FigureValueChangeListener
{
    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="_hp"></param>
    void HPChange(int _hp);
    /// <summary>
    /// 改變速度
    /// </summary>
    /// <param name="_times"></param>
    void SpeedChange(float _times);
    /// <summary>
    /// 改變爆擊機率
    /// </summary>
    /// <param name="_times"></param>
    void CriticleTimesChange(float _times);
    /// <summary>
    /// 改變技能
    /// </summary>
    /// <param name="_skill"></param>
    void SkillChange(Skill _skill);
}

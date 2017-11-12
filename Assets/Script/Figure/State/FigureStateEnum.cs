using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//!!!!!!!!!!!!
//如要新增狀態
//在此增加種類後
//再新增與其同名之class並繼承自FigureState即可

/// <summary>
/// 表示角色的狀態種類
/// </summary>
public enum FigureStateEnum
{
    /// <summary>
    /// 普通狀態
    /// </summary>
    NormalState,
    /// <summary>
    /// 攻擊狀態
    /// </summary>
    AttackState,
    /// <summary>
    /// 移動
    /// </summary>
    MovementState,
    /// <summary>
    /// 技能
    /// </summary>
    SkillState,
    /// <summary>
    /// 死亡
    /// </summary>
    DeadState,
    /// <summary>
    /// 過場
    /// </summary>
    DramaState,
    /// <summary>
    /// 空中
    /// </summary>
    FlyingState,
    /// <summary>
    /// 躺著
    /// </summary>
    LieState
}

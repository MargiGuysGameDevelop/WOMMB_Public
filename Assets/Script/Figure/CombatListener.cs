using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public interface CombatListener
{
    /// <summary>
    /// 普攻
    /// </summary>
    /// <param name="_extraData"></param>
    /// <param name="_data"></param>
    void Attack(int _extraData, PointerEventData _data);
    /// <summary>
    /// 結束普攻
    /// </summary>
    /// <param name="_extraData"></param>
    /// <param name="_data"></param>
    void StopAttack(int _extraData, PointerEventData _data);

    /// <summary>
    /// 受擊
    /// </summary>
    /// <param name="_damage"></param>
    /// <param name="_force"></param>
    void Hitted(int _damage, Vector3 _force);

    /// <summary>
    /// 擊飛
    /// </summary>
    /// <param name="_force"></param>
    void Fly(Vector3 _force);

    /// <summary>
    /// 使用技能
    /// </summary>
    /// <param name="_skillIndex"></param>
    /// <param name="_data"></param>
    void UseSkill(int _skillIndex, PointerEventData _data);
    /// <summary>
    /// 停止使用技能
    /// </summary>
    /// <param name="_skillIndex"></param>
    /// <param name="_data"></param>
    void StopSkill(int _skillIndex, PointerEventData _data);
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public struct FigureValue
{
    [SerializeField]
    int realHP;
    /// <summary>
    /// 目前HP
    /// </summary>
    [SerializeField]
    public int HP {
        set {
            realHP = value;
        }
        get { return Mathf.Clamp(realHP, 0, HPMax); }
    }

    /// <summary>
    /// 最大HP上限，通常為100
    /// </summary>
    [SerializeField]
    public int HPMax;

    /// <summary>
    /// 攻擊時所附加的傷害
    /// </summary>
    public int Damage;

    [SerializeField]
    int realCritclePrecentage;
    /// <summary>
    /// 表示爆擊的機率，0-100
    /// 通常為15
    /// </summary>
    [SerializeField]
    public int CritclePrecentage {
        set { realCritclePrecentage = value; }
        get {
            return Mathf.Clamp(realCritclePrecentage, 0,100);
        }
    }

    [SerializeField]
    float realSpeed;
    /// <summary>
    /// 表示動作的速度倍率，通常為1.00
    /// </summary>
    [SerializeField]
    public float Speed {
        set { realSpeed = value; }
        get { return Mathf.Clamp(realSpeed,0f,2f); }
    }

    public FigureValue(int _hp,int _damage) {
        realHP = HPMax = _hp;
        Damage = _damage;
        realCritclePrecentage = 15;
        realSpeed = 1f;
    }

    public FigureValue(int _hp, int _damage,int _cp,float _speed)
    {
        realHP = HPMax = _hp;
        Damage = 10;
        realCritclePrecentage = 15;
        realSpeed = 1f;
    }

    static public FigureValue DefaultValue {
        get { return new FigureValue(100, 10, 15, 1f); }
    }
}
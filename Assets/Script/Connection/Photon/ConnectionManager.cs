using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

abstract class ConnectionManager
{
    /// <summary>
    /// 在線上創造場上遊戲物件
    /// </summary>
    /// <param name="_object"></param>
    /// <returns></returns>
    abstract public GameObject InstantiateNetwork(GameObject _object);

    /// <summary>
    /// 在本地端創立場上遊戲物件
    /// </summary>
    /// <param name="_object"></param>
    /// <returns></returns>
    virtual public GameObject InstantiateLocal(GameObject _object) {
        return GameObject.Instantiate(_object);
    }

    abstract public void Attack(int _damage, bool _isBroken,Figure _figure);


}
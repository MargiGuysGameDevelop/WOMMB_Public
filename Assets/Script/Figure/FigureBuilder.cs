using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class FigureBuilder
{
    //
    //基本數值
    //
    [SerializeField]public string Name;
    private GameObject myGameObjectOnScene;
    public GameObject GameObjectOnScene {
        get {
            if (myGameObjectOnScene == null)
                myGameObjectOnScene = GameObject.Instantiate(LoadFactory.Instance.LoadGameObject(Name));
            return myGameObjectOnScene;
        }
    }
    [SerializeField]
    public FigureValue Value;
    [SerializeField]
    public FigureBehavior.InputType Type;
    [SerializeField]
    public int AttackNumber = 1;

    //
    //人物位置
    //
    public Vector3 Position = Vector3.zero;
    public Quaternion Rotation = Quaternion.Euler(Vector3.zero);
}
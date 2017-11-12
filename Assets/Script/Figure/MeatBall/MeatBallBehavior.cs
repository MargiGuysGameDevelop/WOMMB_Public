using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MeatBallBehavior : FigureBehavior
{
    /// <summary>
    /// MeatBall物體本身
    /// </summary>
    [SerializeField]public MeatBall myMeatBallFigure;

    /// <summary>
    /// 所有部件的父物件，請在UnityEditor中拉好拉滿
    /// </summary>
    public GameObject[] BodyParts = new 
        GameObject[Enum.GetValues(typeof(MeatBall.BodyPart)).Length];

    /// <summary>
    /// 設置UV位置用
    /// </summary>
    public void SetUV() {

    }


    [ContextMenu("尋找身體物件")]
    private void FindBodyPartsObject() {
        MeatBall.BodyPart[] array = Enum.GetValues(typeof(MeatBall.BodyPart)) as MeatBall.BodyPart[] ;
        for (int i=0;i< array.Length;i++) {
            int index = (int)array[i];
            BodyParts[index] = GameObject.Find(array[i].ToString());
        }
    }


}
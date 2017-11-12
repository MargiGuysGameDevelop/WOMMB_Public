using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 測試創造函式用
/// </summary>
public class FigureCreator : MonoBehaviour {

    /// <summary>
    /// 建造者，可在Unity中直接設定節省時間
    /// </summary>
    [SerializeField]
    public FigureBuilder Builder;

    /// <summary>
    /// 在遊戲開始時直接生成測試
    /// </summary>
    void Start() {
        var figure = FigureFactory.Instance.CreateFigure(Builder);
    }
}

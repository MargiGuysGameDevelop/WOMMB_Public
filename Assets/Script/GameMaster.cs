using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    //場景管理者
    [SerializeField]
    MySceneControl sceneControl = new MySceneControl();

    //遊戲邏輯(介面、系統)管理者
    [SerializeField]
    GameLogicManager gameLogicManager;

    void Start() {
        //初始化
        Initial();
        //遊戲邏輯
        gameLogicManager = GameLogicManager.Instance;
        gameLogicManager.GM = this;
        //場景
        sceneControl.Start();
    }

    void Update() {
        //場景
        sceneControl.Update();
        //遊戲邏輯
        gameLogicManager.Update();
    }

    /// <summary>
    /// 初始化函式
    /// </summary>
    private void Initial() {
        //此(GM)物件永遠存於場上
        DontDestroyOnLoad(gameObject);
    }
}

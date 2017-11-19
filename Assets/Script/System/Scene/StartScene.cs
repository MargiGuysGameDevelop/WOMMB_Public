using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartScene : MyScene
{
    public StartScene(MySceneControl _con) : base(_con)
    {   }

    public override void Start()
    {
        LoadScene();
    }

    /// <summary>
    /// 載入單一Login場景
    /// </summary>
    public override void LoadScene()
    {
        unitySceneName = "Login";
        LoadSceneSingle();

        //隱藏狀態以及控制選項
        GameLogicManager.Instance.HidePlayerFigureUI();

        //顯示登入UI
        GameLogicManager.Instance.ShowLoginUI();

        //貢丸出現在畫面左方
        GameLogicManager.Instance.FigureFactory.CreateFigure(new MeatBallBuilder());
        //播放登入動畫
        AnimatorOverrideController aoc = LoadFactory.Instance.LoadObject<AnimatorOverrideController>
            ("LoginMBAnimator", "Animator", "Figure", "MeatBall");
        GameLogicManager.Instance.ChangeMainAnimation(aoc);
        //站在指定位置並面相攝影機
        GameLogicManager.Instance.ChangeMainPosition(new Vector3(-3.47f, 0f,3.25f));
        GameLogicManager.Instance.ChangeMainRotation(Quaternion.Euler(new Vector3(0f,180f,0f)));

        //掛上開始遊戲的委派
        GameLogicManager.Instance.LoginUI.Btn_Login.onClick += LoginGame ;

        //將攝影機的目標轉移後
        //讓攝影機就定位
        GameLogicManager.Instance.CameraTarget(null);
        GameLogicManager.Instance.CameraPosition(new Vector3(-1.9f,0.43f, 0.08f));
        GameLogicManager.Instance.CameraRotation(Quaternion.Euler(new Vector3(-15f,0f,0f)));

    }

    /// <summary>
    /// 登入事件
    /// </summary>
    /// <param name="_noMean"></param>
    /// <param name="_extraData"></param>
    private void LoginGame(int _noMean, PointerEventData _extraData) {
        sceneController.ChangeScene(new FirstLoginScene(this.sceneController));
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        GameLogicManager.Instance.HideLoginUI();
    }
}

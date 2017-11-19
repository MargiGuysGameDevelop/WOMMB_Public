using System;
using System.Collections.Generic;


public class FirstLoginScene : MyScene
{
    public FirstLoginScene(MySceneControl _control) : base(_control)
    {
        GameLogicManager.Instance.ShowFirstLoginUI();
        //按下開始按鈕則開始遊戲
        GameLogicManager.Instance.FirstLoginUI.BtnStartGame.OnClickEvent
            = (_no, _no2) =>
            {
                sceneController.ChangeScene(new GonGonVillageScene(sceneController));
            };

    }

    public override void Exit()
    {
        GameLogicManager.Instance.HideFirstLoginUI();
    }

    public override void LoadScene()
    {

    }

    public override void Start()
    {

    }

    public override void Update()
    {}
}


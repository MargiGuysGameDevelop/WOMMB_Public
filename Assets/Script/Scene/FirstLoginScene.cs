using System;
using System.Collections.Generic;


public class FirstLoginScene : MyScene
{
    public FirstLoginScene(MySceneControl _control) : base(_control)
    {
        GameLogicManager.Instance.ShowFirstLoginUI();
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


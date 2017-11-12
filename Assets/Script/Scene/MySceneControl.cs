using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class MySceneControl
{
    MyScene currentScene;

    public void Start()
    {
        currentScene = new StartScene(this);
        currentScene.Start();
    }

    public void Update()
    {
        currentScene.Update();
    }

    public void ChangeScene(MyScene _newScene)
    {
        currentScene.Exit();
        currentScene = _newScene;
        currentScene.Start();
    }
}

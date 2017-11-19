using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;
using UnityEngine;

public abstract class MyScene: MySceneLifeCycle
{
    protected GameMaster gm;

    protected MySceneControl sceneController;

    [SerializeField]
    [Header("SceneName")]
    protected string unitySceneName = "";

    public MyScene(MySceneControl _con) {
        sceneController = _con;
    }

    /// <summary>
    /// 如有需要轉換/增加場景，請在Start中呼叫
    /// </summary>
    public abstract void LoadScene();

    public abstract void Start();
    public abstract void Update();
    public abstract void Exit();

    /// <summary>
    /// 以增加場景的方式呼叫Unity的場景
    /// </summary>
    protected void LoadSceneAddtive()
    {
        SceneManager.LoadSceneAsync(unitySceneName, LoadSceneMode.Additive);
    }

    /// <summary>
    /// 以取代場景的方式呼叫Unity的場景
    /// </summary>
    protected void LoadSceneSingle()
    {
        SceneManager.LoadSceneAsync(unitySceneName, LoadSceneMode.Single);
    }
}

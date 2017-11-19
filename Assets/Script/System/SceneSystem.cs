using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class SceneSystem : MySystem
{
    //場景管理者
    [SerializeField]
    MySceneControl sceneControl = new MySceneControl();

    //攝影機管理者
    CameraManager cameraManager = new CameraManager();

    public override void Start()
    {
        //場景開始切換
        sceneControl.Start();
        //攝影機初始化
        cameraManager.Start();
    }

    public override void Update()
    {
        //場景
        sceneControl.Update();
    }

    /// <summary>
    /// 攝影機目標
    /// </summary>
    /// <param name="_figure"></param>
    public void CameraTarget(Figure _figure)
    {
        cameraManager.Camera.Figure = _figure;
    }

    /// <summary>
    /// 改變攝影機位置
    /// </summary>
    /// <param name="_poition"></param>
    public void CamaraPosition(Vector3 _poition)
    {
        cameraManager.Camera.Transform.position = _poition;
    }

    /// <summary>
    /// 改變攝影機的旋轉
    /// </summary>
    public void CameraRotation(Quaternion _rotation)
    {
        cameraManager.Camera.Transform.rotation = _rotation;
    }
}

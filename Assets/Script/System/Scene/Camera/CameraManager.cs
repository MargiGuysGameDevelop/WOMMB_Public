using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class CameraManager : MySceneLifeCycle
{
    MyCamara myCamera;

    public MyCamara Camera
    {
        get {
            if (myCamera == null)
            {
                Start();
            }
            return myCamera; }
    }

    public void Start()
    {
        //如果沒有攝影機，則找/創造一個並使其存活於場上不消亡
        if (myCamera == null)
        {
            myCamera = MyCamara.FindCamera();
            MyCamara.LetCameraExist();
        }
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }
}
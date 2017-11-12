using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class ControllerManager : MyUIControl
{
    /// <summary>
    /// 使用者輸入(移動 攻擊 技能)
    /// </summary>
    PlayerInputGroup playerFigureInput;
    
    /// <summary>
    /// 基本行動控制面板
    /// </summary>
    public PlayerInputGroup UnityUI {
        get {
            if (playerFigureInput == null)
                playerFigureInput = myRootUI.GameObject.GetComponent<PlayerInputGroup>();
            return playerFigureInput;
        }
    }

    /// <summary>
    /// 相機
    /// </summary>
    private MyCamara mainCameraControl;
    public MyCamara MainMyCamera {
        get
        {
            if (mainCameraControl == null)
            {
                mainCameraControl = MyCamara.FindCamera();
            }
            return mainCameraControl;
        }
    }

    FigureSystem myFigureSystem {
        get {
            return mySystem as FigureSystem;
        }
    }

    public override void Start()
    {
        LoadRootUI("FigureController", UIFactory.CanvasType.AlwaysRefresh);
        
    }

    public override void Update()
    {

    }
}

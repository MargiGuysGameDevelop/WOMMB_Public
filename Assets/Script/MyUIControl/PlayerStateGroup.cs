using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PlayerStateGroup : MyUIControl
{
    private MyUIFigureState playerState = null;
    /// <summary>
    /// 資料顯示
    /// </summary>
    public MyUIFigureState UnityUI
    {
        get
        {
            if (playerState == null)
                playerState = myRootUI.GameObject.GetComponent<MyUIFigureState>();
            return playerState;
        }
    }

    public override void Start()
    {
        LoadRootUI("FigureState", UIFactory.CanvasType.AlwaysRefresh);
    }

    public override void Update()
    {
    }
}

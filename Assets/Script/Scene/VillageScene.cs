using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 村莊場景
/// 有預先寫好的函式
/// </summary>
class VillageScene : MyScene
{
    public VillageScene(MySceneControl _con) : base(_con)
    {
    }

    public override void Exit()
    {
        
    }

    public override void LoadScene()
    {
        
    }

    public override void Start()
    {
        //GameLogicManager.Instance.CheckCamera();
    }

    public override void Update()
    {
        
    }

    /// <summary>
    /// 顯示玩家角色UI
    /// </summary>
    protected void ShowPlayerFigureUI() {
        GameLogicManager.Instance.ShowPlayingFigureUI();
    }
    /// <summary>
    /// 隱藏玩家角色UI
    /// </summary>
    protected void HidePlayerFigureUI() {
        GameLogicManager.Instance.HidePlayerFigureUI();
    }
}
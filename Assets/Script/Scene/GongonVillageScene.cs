using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class GonGonVillageScene : VillageScene
{
    public GonGonVillageScene(MySceneControl _con) : base(_con)
    {
    }

    public override void Start()
    {
        base.Start();
        ShowPlayerFigureUI();
        LoadScene();
    }

    public override void LoadScene()
    {
        unitySceneName = "GonGon";
        LoadSceneSingle();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 玩家資料
/// </summary>
class PlayerData
{

    //真正的資訊

    protected string name;
    protected int money;
    protected string stage;
    protected string meatBallSet;

    public string Name {
        get { return name; }
        set { name = value;}
    }

    public int Money {
        get { return money; }
        set {
             money = value;
        }
    }

    public string StageProgress {
        get { return stage; }
        set { stage = value; }
    }

    public string MeatBallSets {
        get { return meatBallSet; }
        set { meatBallSet = value; }
    }

    public PlayerData() {}

}
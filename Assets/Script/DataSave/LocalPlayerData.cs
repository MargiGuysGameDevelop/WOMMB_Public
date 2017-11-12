using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class LocalPlayerData : PlayerData
{
    ///資料工廠(如果更改讀取資料方式從這裏改類別)
    static private PlayerDataFactory factory = new PlayerSLPlayerPrefs();

    //主要玩家的資訊
    private static LocalPlayerData instance;
    public static LocalPlayerData Instance
    {
        get
        {
            if (instance == null)
                instance = factory.Load() as LocalPlayerData;
            return instance;
        }
    }

    new public string Name
    {
        get { return name; }
        set { name = value; factory.Save(this); }
    }

    new public int Money
    {
        get { return money; }
        set
        {
            money = value; factory.Save(this);
        }
    }

    new public string StageProgress
    {
        get { return stage; }
        set { stage = value; factory.Save(this); }
    }

    new public string MeatBallSets
    {
        get { return meatBallSet; }
        set { meatBallSet = value; factory.Save(this); }
    }
}

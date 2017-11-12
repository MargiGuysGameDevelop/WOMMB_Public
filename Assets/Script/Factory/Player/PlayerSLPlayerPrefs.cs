using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class PlayerSLPlayerPrefs : PlayerDataFactory
{
    public override LocalPlayerData Load()
    {
        LocalPlayerData result = new LocalPlayerData();
        //名稱
        if (PlayerPrefs.HasKey(keyPlayerName))
            result.Name = PlayerPrefs.GetString(keyPlayerName);
        else
        {
            result.Name = "No Name";
            PlayerPrefs.SetString(keyPlayerName,result.Name);
        }
        //錢
        if (PlayerPrefs.HasKey(keyPlayerMoney))
            result.Money = PlayerPrefs.GetInt(keyPlayerMoney);
        else
        {
            result.Money = 0;
            PlayerPrefs.SetInt(keyPlayerMoney, result.Money);
        }
        //貢丸套裝
        if (PlayerPrefs.HasKey(keyPlayerMeatBallSets))
            result.MeatBallSets = PlayerPrefs.GetString(keyPlayerMeatBallSets);
        else
        {
            //僅有初始套裝
            result.MeatBallSets = "1";
            PlayerPrefs.SetString(keyPlayerMeatBallSets, result.MeatBallSets);
        }
        //劇情進度
        if (PlayerPrefs.HasKey(keyStageProgress))
            result.StageProgress = PlayerPrefs.GetString(keyStageProgress);
        else
        {
            //空進度
            result.StageProgress = "";
            PlayerPrefs.SetString(keyStageProgress, result.StageProgress);
        }
        return result;
    }

    public override void Save(PlayerData _data)
    {
        PlayerPrefs.Save();
    }
}

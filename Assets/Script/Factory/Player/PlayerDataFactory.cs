using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 存取使用者資料的類別
/// 用以繼承
/// </summary>
abstract class PlayerDataFactory
{
    //靜態變數，用以提取玩家資料

    /// <summary>
    /// 玩家名稱
    /// </summary>
    static protected string keyPlayerName = "PLAYERNAME";
    /// <summary>
    /// 玩家的金錢
    /// </summary>
    static protected string keyPlayerMoney = "PLAYERMONEY";
    /// <summary>
    /// 玩家貢丸的套裝
    /// </summary>
    static protected string keyPlayerMeatBallSets = "MEATBALLSETS";
    /// <summary>
    /// 關卡進度
    /// </summary>
    static protected string keyStageProgress = "STAGES";

    /// <summary>
    /// 儲存使用者資料
    /// </summary>
    /// <param name="_data"></param>
    public abstract void Save(PlayerData _data);

    /// <summary>
    /// 讀取使用者資料
    /// </summary>
    /// <returns></returns>
    public abstract LocalPlayerData Load();
}

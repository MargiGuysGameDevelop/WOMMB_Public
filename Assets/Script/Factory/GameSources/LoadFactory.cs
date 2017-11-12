using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 產生物件的物件，使用單例模式(Singleton-Pattern)
/// 使用時直接LoadFactory.Instance.即可
/// </summary>
public abstract class LoadFactory
{
    
    static private LoadFactory instance= null;
    static public LoadFactory Instance {
        get {
            if (instance == null)
                instance = new ResourceLoadFactory();
            return instance;
        }
    }

    /// <summary>
    /// 取得Unity.Object，僅從檔案讀取至記憶體，並無實例化
    /// </summary>
    /// <param name="_name"></param>
    /// <returns></returns>
    public abstract GameObject LoadGameObject(string _name);
    /// <summary>
    /// 取得Unity.Object，僅從檔案讀取至記憶體，並無實例化
    /// </summary>
    /// <param name="_tag">資料夾名稱或其標籤</param>
    /// <param name="_name"></param>
    /// <returns></returns>
    public abstract GameObject LoadGameObject(string _name,params string[] _path);
    /// <summary>
    /// 取得物件(輸入必須含附檔名)，僅從檔案讀取至記憶體，並無實例化
    /// </summary>
    /// <param name="_name"></param>
    /// <returns></returns>
    public abstract object LoadObject(string _name);
    /// <summary>
    /// 取得物件(輸入必須含附檔名)，僅從檔案讀取至記憶體，並無實例化
    /// </summary>
    /// <param name="_tag">資料夾名稱或其標籤</param>
    /// <param name="_name"></param>
    /// <returns></returns>
    public abstract object LoadObject(string _name, params string[] _pat);
    /// <summary> 
    /// 使用泛形直接取得物件(輸入必須含附檔名)，僅從檔案讀取至記憶體，並無實例化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_name"></param>
    /// <param name="_path"></param>
    /// <returns></returns>
    public abstract T LoadObject<T>(string _name,params string[] _path) where T:UnityEngine.Object;
    /// <summary>
    /// 取得ScriptableObject，僅從檔案讀取至記憶體，並無實例化
    /// </summary>
    /// <param name="_name"></param>
    /// <returns></returns>
    public abstract ScriptableObject LoadScriptableObject(string _name);
    /// <summary>
    /// 取得ScriptableObject，僅從檔案讀取至記憶體，並無實例化
    /// </summary>
    /// <param name="_name">資料夾名稱或其標籤</param>
    /// <returns></returns>
    public abstract ScriptableObject LoadScriptableObject(string _name, params string[] _pat);

    /// <summary>
    /// 取得已經實例化的遊戲物件。
    /// </summary>
    /// <param name="_name"></param>
    /// <returns></returns>
    public virtual GameObject LoadAndInstantiateGameObject(string _name,params string[] _path) {
        return GameObject.Instantiate(LoadGameObject(_name,_path));
    }

    /// <summary>
    /// 將輸入字串陣列轉成檔案路徑
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_path"></param>
    /// <returns></returns>
    protected string TrunStringsIntoPath(string _name, params string[] _path)
    {
        string entirePath = string.Empty;
        for (int i = 0; i < _path.Length; i++)
        {
            entirePath = Path.Combine(entirePath, _path[i]);
        }
        entirePath = Path.Combine(entirePath, _name);
        return entirePath;
    }
}

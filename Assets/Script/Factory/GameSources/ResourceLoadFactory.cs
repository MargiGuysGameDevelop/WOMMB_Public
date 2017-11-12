using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;

/// <summary>
/// LoadFactory的其中一種方式，使用UnityEngine.Resourse
/// </summary>
class ResourceLoadFactory : LoadFactory
{
    Dictionary<string,object> gameObjectPool
        = new Dictionary<string, object>();
    

    public override GameObject LoadGameObject(string _name)
    {
        return LoadGameObject("",_name);
    }

    public override GameObject LoadGameObject(string _name, params string[] _path)
    {
        return LoadObject(_name,_path) as GameObject;
    }

    public override object LoadObject(string _name)
    {
        return LoadObject(_name,"");
    }
    public override object LoadObject(string _name, params string[] _path)
    {
        return LoadObject<UnityEngine.Object>(_name,_path) as object;
    }
    public override T LoadObject<T>(string _name, params string[] _path)
    {
        if (_name == "Login") {
            Debug.Log("!");
        }
        if (gameObjectPool.ContainsKey(_name))
        {
            try
            {
                return (T)gameObjectPool[_name];
            }
            catch {
                return null;
            }
        }
        T go = Resources.Load<T>(TrunStringsIntoPath(_name, _path));
        gameObjectPool.Add(_name, go);
        return go;
    }

    public override ScriptableObject LoadScriptableObject(string _name)
    {
        return LoadScriptableObject(_name,"");
    }

    public override ScriptableObject LoadScriptableObject(string _name, params string[] _path)
    {
        return LoadObject(_name, _path) as ScriptableObject;
    }

    /// <summary>
    /// DFS搜尋檔案，目前棄用
    /// </summary>
    /// <param name="_fileName"></param>
    /// <param name="_objName"></param>
    /// <returns></returns>
    private object FindInDirectory(DirectoryInfo _fileName,string _objName) {
        //結果
        object result = null;

        //先尋找檔案
        FileInfo[] files = _fileName.GetFiles();
        int fileNumbers = files.Length;

        //尋找
        for (int index = 0; index < fileNumbers; index++)
            if (files[index].Name == _objName)
                return Resources.Load(files[index].FullName) as object;

        //取得資料夾
        DirectoryInfo[] infos = _fileName.GetDirectories();
        for (int index = 0; index < infos.Length; index++) {
            object obj = FindInDirectory(infos[index], _objName);
            if (obj != null)
            {
                result = obj;
                return result;
            }
        }
        return result;
    }
}
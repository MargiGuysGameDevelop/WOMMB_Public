using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class AssetBunbleLoadFactory : LoadFactory
{
    public override GameObject LoadGameObject(string _name)
    {
        throw new NotImplementedException();
    }

    public override GameObject LoadGameObject(string _name, params string[] _path)
    {
        throw new NotImplementedException();
    }

    public override object LoadObject(string _name)
    {
        throw new NotImplementedException();
    }

    public override object LoadObject(string _name, params string[] _pat)
    {
        throw new NotImplementedException();
    }

    public override T LoadObject<T>(string _name, params string[] _path)
    {
        throw new NotImplementedException();
    }

    public override ScriptableObject LoadScriptableObject(string _name)
    {
        throw new NotImplementedException();
    }

    public override ScriptableObject LoadScriptableObject(string _name, params string[] _pat)
    {
        throw new NotImplementedException();
    }
}

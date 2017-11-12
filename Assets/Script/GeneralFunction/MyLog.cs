using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class MyLog {

    public static void Log(string _message) {
        Debug.Log(_message);
    }

    public static void Log() {
        Debug.Log("Debug!");
    }

    public static void Log(object _obj) {
        Debug.Log(_obj.ToString());
    }
}

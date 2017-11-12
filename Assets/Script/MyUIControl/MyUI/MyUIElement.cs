using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 顯示至UI的自訂資料類別
/// </summary>
public class MyUIElementData
{
    public Sprite Icon;
    public string Name;
    public string Content;
    public MyDelegate.ButtonDelegate ButtonClickEvent = null;
}

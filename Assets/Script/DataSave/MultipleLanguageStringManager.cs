using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

public class MultipleLanguageStringManager
{
    public enum Languages
    {
        English,
        Chinese
    }

    static private MultipleLanguageStringManager instance = null;
    static public MultipleLanguageStringManager Instance {
        get {
            if (instance == null)
                instance = new MultipleLanguageStringManager();
            return instance;
        }
    }

    /// <summary>
    /// 語言
    /// </summary>
    private Languages Language = Languages.English;


}
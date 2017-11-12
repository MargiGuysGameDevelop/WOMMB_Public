using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FigureFactory
{
    static public FigureFactory Instance
    {
        get { return GameLogicManager.Instance.FigureFactory; }
    }

    private MyDelegate.GainFigure onCreateFigure;
    public MyDelegate.GainFigure OnCreateFigure {
        set { onCreateFigure = value; }
        get {
            if (onCreateFigure == null)
                onCreateFigure = GameLogicManager.Instance.CreateFigureEvent;
            return onCreateFigure;
        }
    }

    Dictionary<string,List<Figure>> figurePool = new Dictionary<string, List<Figure>>();

    /// <summary>
    /// 創造角色的函式(會出現在場上)
    /// </summary>
    /// <param name="_builder"></param>
    /// <returns></returns>
    public Figure CreateFigure(FigureBuilder _builder) {
        Figure newFigure = null;
       
        if (_builder.Name.Contains("MeatBall")) {
            newFigure = new MeatBall(_builder);
        }else
            newFigure = new Figure(_builder); ;


        if (figurePool.ContainsKey(_builder.Name))
        {
            return figurePool[_builder.Name][0];
        }
        else
        {
            List<Figure> figureList = new List<Figure>();
            figureList.Add(newFigure);
            figurePool.Add(_builder.Name,figureList);
            OnCreateFigure(newFigure);
            return newFigure;
        }
    }
}
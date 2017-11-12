using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FigureSystem : MySystem
{
    [SerializeField]
    protected Figure main;

    public Figure Main {
        get { return main; }
        set {
            main = value;
        }
    }

    public FigureFactory FigureFactory = new FigureFactory();

    public override void Start()
    {
        
    }

    public override void Update()
    {

    }
}
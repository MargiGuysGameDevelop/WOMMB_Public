using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MeatBallBuilder : FigureBuilder
{
    public MeatBallSetFactory.SetType Set;

    public MeatBallBuilder()
    {
        this.Name = "MeatBall";
        this.Set = MeatBallSetFactory.SetType.Orgin;
    }
}

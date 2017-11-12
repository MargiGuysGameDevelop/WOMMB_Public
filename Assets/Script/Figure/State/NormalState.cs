using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class NormalState : FigureState
{
    public override void Movement(float _x, float _y)
    {
        Figure.MovementAction(_x, _y, true);
    }

    public override void StopMovement(float _x, float _y)
    {
        Figure.MovementAction(0, 0, true);
    }
}
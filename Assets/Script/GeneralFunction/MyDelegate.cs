using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;

public class MyDelegate
{
    public delegate void VoidDelegate();

    public delegate void IntVoidDelegate(int _int);

    public delegate void ButtonDelegate(int _extraData, PointerEventData _data);
    public delegate void JoystickDelegate(float _x, float _y);


    public delegate void FigureActionListenerEvent(FigureActionListener _event);
    public delegate void FigureValueChangeListenerEvent(FigureValueChangeListener _event);
    public delegate void CombatListenerEvent(CombatListener _event);

    public delegate void FigureStateVoidDelegate(FigureStateEnum _state);

    public delegate void GainFigure(Figure _figure);
}

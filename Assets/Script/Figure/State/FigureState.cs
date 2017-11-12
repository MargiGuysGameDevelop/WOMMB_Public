using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class FigureState : FigureActionListener, FigureValueChangeListener, CombatListener
{

    Figure myFigure;

    public bool IsListeningAnyTime = false;

    public FigureState() {
    }

    public Figure Figure {
        set { myFigure = value; }
        get { return myFigure; }
    }

    public virtual void Attack(int _extraData, PointerEventData _data)
    {
        myFigure.AttackAction(_extraData,_data,true);
    }

    public virtual void StopAttack(int _extraData, PointerEventData _data)
    {
        myFigure.AttackAction(0,_data,false);
    }

    public virtual void Fly(Vector3 _force)
    {
        myFigure.FlyAction(_force,true,true);
    }

    public virtual void Hitted(int _damage, Vector3 _force)
    {
        myFigure.HittedAction(_damage,_force,true,true);
    }

    public virtual void Movement(float _x, float _y)
    {
        
    }

    public virtual void StopMovement(float _x, float _y)
    {
        
    }

    public virtual void StopSkill(int _skillIndex, PointerEventData _data)
    {
    }

    public virtual void UseSkill(int _skillIndex, PointerEventData _data)
    {
        myFigure.SkillAction(_skillIndex,_data);
    }


    public virtual void SpeedChange(float _times)
    {
        throw new NotImplementedException();
    }

    public virtual void HPChange(int _hp)
    {
        myFigure.HPChangeAction(_hp);
    }

    public virtual void CriticleTimesChange(float _times)
    {
        throw new NotImplementedException();
    }

    public virtual void SkillChange(Skill _skill) {
    }

    #region LifeCycle
    public virtual void Start() { }
    public virtual void Update() { }
    public virtual void Exit() { }
    #endregion

    #region AnimationEvent
    public virtual void LaunchProjection() {

    }
    #endregion
}
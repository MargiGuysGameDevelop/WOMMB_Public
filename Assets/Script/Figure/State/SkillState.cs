using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

class SkillState : FigureState
{
    Skill skill;

    RuntimeAnimatorController orginAOC;

    public override void Start()
    {
        //保存技能
        skill = Figure.CurrentSkill;
        //如有動作則將AOC覆蓋並執行動作，無則直接切換成一般動作
        if (skill.AOC == null) {
            Figure.ChangeState(FigureStateEnum.NormalState);
            throw new Exception("技能:" + skill.Name + "沒AOC，再去檢查一下");
        }
        //將覆蓋動作保存下來以備不時之需
        orginAOC = Figure.Behavior.Animator.runtimeAnimatorController;
        Figure.Behavior.Animator.runtimeAnimatorController = Figure.CurrentSkill.AOC;
        //如果動作為自行撥放
        if (skill.AnimationPlayType == Skill.AnimationPlay.Auto)
        {
            DoAction(true);
        }
    }

    public override void Exit()
    {
        //回復原樣
        Figure.Behavior.Animator.runtimeAnimatorController = orginAOC;
        AnimatorManager.Instance.SetAnimatorInt(
                AnimatorManager.Instance.SkillType,
                Figure.Behavior.Animator,
                0
                );
        DoAction(false);
    }

    public override void StopSkill(int _skillIndex, PointerEventData _data)
    {
        if (skill.AnimationPlayType == Skill.AnimationPlay.ExitButton) {
            DoAction(true);
            ExitImmediately();
        }
    }

    public override void Hitted(int _damage, Vector3 _force)
    {
        Figure.HittedAction(_damage, _force, true, false);
    }

    public override void HPChange(int _hp)
    {
    }

    public override void SpeedChange(float _times)
    {
    }

    public override void CriticleTimesChange(float _times)
    {
    }

    public override void LaunchProjection()
    {

    }

    private void DoAction(bool _isDoing) {
        AnimatorManager.Instance.SetAnimatorBool(
                AnimatorManager.Instance.isDoingAction,
                Figure.Behavior.Animator,
                _isDoing
                );
    }

    private void ExitImmediately() {
        AnimatorManager.Instance.SetAnimatorTrigger(
            AnimatorManager.Instance.Exit,
            Figure.Behavior.Animator
            );
    }
}
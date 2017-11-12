using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMBActionName : StateMachineBehaviour {

    [SerializeField]
    public FigureStateEnum state;

	public MyDelegate.FigureStateVoidDelegate EnterDelegate;
	public MyDelegate.FigureStateVoidDelegate ExitDelegate;

	private SMBFigureAnimator figureAnimator;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (EnterDelegate != null)
        {
            EnterDelegate(state);
        }

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

		if (ExitDelegate != null) ExitDelegate(state);
    }
}

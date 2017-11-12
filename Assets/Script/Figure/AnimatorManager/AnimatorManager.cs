using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// It is a singleton
/// Use to store animator hashing
/// and
/// functions about animator
/// </summary>
public class AnimatorManager{

    static private AnimatorManager instance;
    /// <summary>
    /// Singleton
    /// </summary>
    static public AnimatorManager Instance
    {
        get {
            if (instance == null)
                instance = new AnimatorManager();
            return instance;
        }
    }

    #region Hash
    //bool
    public int isDoingAction;
    public int isIdle;
	public int isHurt;
	public int isBroken;
	public int isGround;
	public int isInterrupt;
	//float
	public int YFlyTime;
	public int XZFlyTime;
	public int Vertical;
	public int Horizontal;
	//int
	public int SkillType;
    public int AttackType;
	//Trigger
	public int Dead;
    public int Exit;

    //State
    public int Idle;

    public AnimatorManager(){
		//bool
		isIdle = Animator.StringToHash ("isIdle");
		isHurt = Animator.StringToHash ("isHurt");
		isBroken = Animator.StringToHash ("isBroken");
		isGround = Animator.StringToHash ("isGround");
		isInterrupt = Animator.StringToHash ("isInterrupt");
        isDoingAction = Animator.StringToHash("isDoingAction");

		//float
		YFlyTime = Animator.StringToHash("YFlyTime");
		XZFlyTime = Animator.StringToHash("XZFlyTime");
		Vertical = Animator.StringToHash ("Vertical");
		Horizontal = Animator.StringToHash ("Horizontal");

		//int 
		SkillType= Animator.StringToHash("SkillType");
        AttackType = Animator.StringToHash("AttackType");

		//Trigger
		Dead = Animator.StringToHash ("Dead");
        Exit = Animator.StringToHash("Exit");

        //state
        Idle = Animator.StringToHash("Idle");
	}
    #endregion

    /// <summary>
    /// Enter Animation Clip By Hashing
    /// </summary>
    /// <param name="_nameHash"></param>
    /// <param name="_animator"></param>
    /// <param name="_time"></param>
    /// <param name="_layerIndex"></param>
    public void EnterAnimatorClip(int _nameHash,ref Animator _animator,float _time,int _layerIndex)
    {
        _animator.CrossFade(_nameHash,_time,_layerIndex);
    }

    /// <summary>
    /// Set Animator int 
    /// </summary>
    /// <param name="_index"></param>
    /// <param name="_behavior"></param>
    /// <param name="_value"></param>
    public void SetAnimatorInt(int _index,Animator _animator,int _value)
    {
        _animator.SetInteger(_index,_value);
    }

    /// <summary>
    /// Set Animator Float
    /// </summary>
    /// <param name="_index"></param>
    /// <param name="_behavior"></param>
    /// <param name="_value"></param>
    public void SetAnimatorFloat(int _index,Animator _animator, float _value)
    {
        _animator.SetFloat(_index, _value);
    }

    /// <summary>
    /// Set Unity Animator's Bool
    /// </summary>
    /// <param name="_index"></param>
    /// <param name="_behavior"></param>
    /// <param name="_value"></param>
    public void SetAnimatorBool(int _index,Animator _animator, bool _value)
    {

        _animator.SetBool(_index, _value);
    }

	public void SetAnimatorTrigger(int _index, Animator _animator)
    {
        _animator.SetTrigger (_index);
	}
}

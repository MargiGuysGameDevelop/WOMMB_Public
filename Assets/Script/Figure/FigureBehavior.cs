using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 掛載在角色物件身上的腳本，為角色邏輯的中樞
/// </summary>
public class FigureBehavior : MonoBehaviour
{
    #region 與Figure之橋接
    [SerializeField]
    public Figure myFigure;

    public void FigureInitialize(Figure _figure) {
        //
        myActionListener = _figure;
        myValueChangeListener = _figure;
        myCombatListener = _figure;

        myFigure = _figure;
    }

    #endregion

    #region 角色事件
    //Action 人物動作事件
    FigureActionListener myActionListener;

    //ValueChange 數值變化事件
    FigureValueChangeListener myValueChangeListener;

    //Combat 對戰事件
    CombatListener myCombatListener;
    #endregion

    #region GameObject及其出現於場上
    [SerializeField]
    protected GameObject myBody;
    /// <summary>
    /// 遊戲物件
    /// </summary>
    public GameObject Body {
        get {
            if (!myBody)
                myBody = gameObject;
            return myBody;
        }
    }
    #endregion

    #region Animator動作相關
    [SerializeField]
    protected Animator myAnim;
    public Animator Animator {
        get {
            if (!myAnim)
            {
                myAnim = GetComponent<Animator>();
                SMBActionName[] actionNames = Animator.GetBehaviours<SMBActionName>();
                for (int i = 0; i < actionNames.Length; i++) {
                    actionNames[i].EnterDelegate = ChangeState;
                }
            }
            return myAnim;
        }
    }

    public void ChangeState(FigureStateEnum _state) {
        myFigure.ChangeState(_state);
    }

    /// <summary>
    /// 動作物件使用函式，發射物體。
    /// </summary>
    public void LaunchProjection() {
        myFigure.LaunchProjection();
    }
    #endregion

    //       動作輸入   
    //       分別為      
    //       手動(PlayerInput)          
    //       自動(AIInput) 
    //       其他玩家(OthersInput)   
          
    #region 輸入
    public enum InputType {
        PlayerInput,AIInput,OthersInput
    }

    [Serializable]
    class Input {

        [HideInInspector]
        public FigureBehavior Behavior;
        public Input(FigureBehavior _fb)
        {
            Behavior = _fb;
            Movement = Behavior.Movement;
            StopMovent = Behavior.StopMovement;
            Attack = Behavior.Attack;
            StopAttack = Behavior.StopAttack;
            StopSkill = Behavior.StopSkill;
            Skill = Behavior.UseSkill;

        }

        protected MyDelegate.JoystickDelegate Movement;
        protected MyDelegate.JoystickDelegate StopMovent;

        protected MyDelegate.ButtonDelegate Attack;
        protected MyDelegate.ButtonDelegate StopAttack;

        protected MyDelegate.ButtonDelegate Skill;
        protected MyDelegate.ButtonDelegate StopSkill;

        /// <summary>
        /// 設定位移委派
        /// </summary>
        /// <param name="_movement"></param>
        /// <param name="_stopMovent"></param>
        public void MovementEventSet(
            ref MyDelegate.JoystickDelegate _movement,
            ref MyDelegate.JoystickDelegate _stopMovent
            ) {
            _movement -= Movement;
            _movement += Movement;
            _stopMovent -= StopMovent;
            _stopMovent += StopMovent;
        }
        /// <summary>
        /// 設定攻擊委派
        /// </summary>
        /// <param name="_attack"></param>
        /// <param name="_stopAttack"></param>
        public void AttackEventSet(
              ref MyDelegate.ButtonDelegate _attack,
              ref MyDelegate.ButtonDelegate _stopAttack
            ) {
            _attack -= Attack;
            _attack += Attack;
            _stopAttack -= StopAttack;
            _stopAttack += StopAttack;
        }

        /// <summary>
        /// 設定技能委派
        /// </summary>
        /// <param name="_skill"></param>
        /// <param name="_stopSkill"></param>
        public void SkillEventSet(
              ref MyDelegate.ButtonDelegate _skill,
              ref MyDelegate.ButtonDelegate _stopSkill
            ) {
            _skill -= Skill;
            _skill += Skill;
            _stopSkill -= StopSkill;
            _stopSkill += StopSkill;
        }
    }
    class PlayerInput : Input
    {
        public PlayerInput(FigureBehavior _fb) : base(_fb)
        {
        }

        #region 移動
        [SerializeField]
        MyJoystick myMovementController = null;

        public MyJoystick MovementController
        {
            set
            {
                myMovementController = value;

                MovementEventSet
                    (
                    ref myMovementController.onDrag,
                    ref myMovementController.onDragExit
                    );
            }
            get { return myMovementController; }
        }
        #endregion

        #region 攻擊
        [SerializeField]
        MyUnityUI myAttackButton = null;

        public MyUnityUI AttackButton{
            set {
                myAttackButton = value;

                AttackEventSet(
                    ref myAttackButton.onDown
                    ,ref myAttackButton.onUp);
            }
            get { return myAttackButton; }
        }

        [SerializeField]
        MyUnityUI[] mySkillButton = null;
        public MyUnityUI[] SkillButton{
            set {
                mySkillButton = value;
                for (int i = 0; i < value.Length; i++)
                {
                    SkillEventSet(
                        ref mySkillButton[i].onDown
                        , ref mySkillButton[i].onUp);
                }
            }
            get { return mySkillButton; }
        }

        #endregion
    }
    /// <summary>
    /// 未實做
    /// </summary>
    class AIInput : Input
    {
        public AIInput(FigureBehavior _fb) : base(_fb)
        {
        }
    }
    class OthersInput : Input
    {
        public OthersInput(FigureBehavior _fb) : base(_fb)
        {
        }
    }

    /// <summary>
    /// 輸入控制
    /// </summary>
    [SerializeField]
    Input input;
    public InputType Type;

    public void ChangeInput(InputType _type) {
        //input
        if (_type == InputType.AIInput)
        {
            Type = _type;
            input = new AIInput(this);
        }
        else
        {
            Type = _type;
            input = new PlayerInput(this);
        }
    }

    /// <summary>
    /// 輸入玩家控制的UI
    /// </summary>
    /// <param name="_movement"></param>
    /// <param name="_attack"></param>
    /// <param name="_skill"></param>
    public void InputUI(MyJoystick _movement,MyUnityUI _attack,MyUnityUI[] _skill) {
        PlayerInput myInput = input as PlayerInput;
        MyJoystick oldMovement = null;
        MyUnityUI oldAttack = null;
        MyUnityUI[] oldSkill = null;
        if (myInput != null) {
            oldMovement = myInput.MovementController;
            oldAttack = myInput.AttackButton;
            oldSkill = myInput.SkillButton;
        }
        PlayerInput newInput = new PlayerInput(this)
        {
            MovementController = _movement==null ? oldMovement : _movement,
            AttackButton = _attack==null ? oldAttack : _attack,
            SkillButton = _skill==null? oldSkill : _skill
        };
        input = newInput;
    }

    #endregion

    #region 移動方法

    /// <summary>
    /// 輸入兩軸移動參數進行移動
    /// </summary>
    /// <param name="_x">左右</param>
    /// <param name="_y">上下</param>
    protected virtual void Movement(float _x, float _y) {
        //if (myAnim != null) {
        //    MyLog.Log("(Movement) with Animator");
        //    return;
        //}
        //MyLog.Log("(Movement) no Animator");
        myActionListener.Movement(_x,_y);
    }

    protected virtual void StopMovement(float _x,float _y) {
        myActionListener.StopMovement(_x, _y);

    }
    #endregion

    #region 攻擊方法

    protected virtual void Attack(int _extraData, PointerEventData _data){
        myCombatListener.Attack(myFigure.AttackNumber,_data);
    }

    protected virtual void StopAttack(int _extraData,PointerEventData _data) {
        myCombatListener.StopAttack(_extraData,_data);
    }

    #endregion

    #region 技能方法
    protected virtual void UseSkill(int _skillIndex,PointerEventData _data) {
        myCombatListener.UseSkill(_skillIndex,_data);
    }
    protected virtual void StopSkill(int _skillIndex, PointerEventData _data)
    {
        myCombatListener.StopSkill(_skillIndex,_data);
    }
    #endregion

    #region 受擊方法

    protected void Hitted(int _damage,Vector3 _force) {
        myCombatListener.Hitted(_damage,_force);
    }

    /// <summary>
    /// 擊飛
    /// </summary>
    /// <param name="_force"></param>
    protected void Fly(Vector3 _force) {
        myCombatListener.Fly(_force);
    }

    #endregion

    #region UnityComponent
    private Transform myTransform;
    public Transform Transform {
        get { if (myTransform == null)
                myTransform = Body.transform;
            return myTransform;
        }
    }

    public Vector3 Position {
        set { myTransform.position = value; }
    }

    public Quaternion Rotation {
        set { myTransform.rotation = value; }
    }

    #endregion

    #region 場面生命週期控制

    public void DontDestoryOnLoad() {
        DontDestroyOnLoad(Body);
    }

    public void DestoryIt() {
        DestroyImmediate(Body);
    }

    #endregion

    #region UnityEditor
    #endregion
}
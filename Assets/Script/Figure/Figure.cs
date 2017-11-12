using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Reflection;

/// <summary>
/// 基礎角色類別。
/// 包含遊戲物件的資料
/// 為遊戲物件與自定義類別的溝通橋梁
/// </summary>
[Serializable]
public class Figure : FigureActionListener,FigureValueChangeListener,CombatListener
{
    public Figure() {
        InputType = FigureBehavior.InputType.AIInput;

        Initial();
    }
     
    public Figure(FigureBuilder _builder) {
        Behavior = _builder.GameObjectOnScene.GetComponent<FigureBehavior>();
        myValue = _builder.Value;
        Behavior.name = _builder.Name;
        InputType = _builder.Type;
        AttackNumber = _builder.AttackNumber;

        Initial();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    protected virtual void Initial() {
        //初始化狀態
        InitialState();
    }



    #region 數值
    [SerializeField]
    /// <summary>
    /// Value
    /// </summary>
    FigureValue myValue = FigureValue.DefaultValue;
    public FigureValue Value {
        get { return myValue; }
    }
    /// <summary>
    /// 普通攻擊次數
    /// </summary>
    public int AttackNumber = 1;
    /// <summary>
    /// 名稱
    /// </summary>
    public string Name {
        get { return Behavior.name; }
    }
    #endregion

    #region 狀態

    /// <summary>
    /// 初始化狀態
    /// </summary>
    protected virtual void InitialState() {
        int index = 0;
        FigureStateEnum[] figureStateEnum = Enum.GetValues(typeof(FigureStateEnum)) as FigureStateEnum[];
        myStateList = new FigureState[figureStateEnum.Length];
        foreach (FigureStateEnum _states in figureStateEnum)
        {
            var stateString = _states.ToString();
            Type stateType = Type.GetType(stateString);
            var stateObject = Activator.CreateInstance(stateType) as FigureState;
            stateObject.Figure = this;
            if (stateObject == null)
            {
                MyLog.Log(" state : " + stateString + " is null");
                break;
            }
            myStateList[index++] = stateObject;
        }
        //initial state is normal state
        ChangeState((int)FigureStateEnum.NormalState);
    }
    [SerializeField]
    protected FigureStateEnum currentStateEnum = FigureStateEnum.DeadState;
    /// <summary>
    /// 目前狀態(類別)
    /// </summary>
    [SerializeField]
    protected FigureState currentState;
    /// <summary>
    /// 狀態列表
    /// </summary>
    protected FigureState[] myStateList;
    /// <summary>
    /// 改變人物指定狀態的類別，傳回原先狀態
    /// (沒輸入則直接回傳目前狀態不改變原先狀態)
    /// 數字溢位則回傳Null
    /// </summary>
    /// <param name="_newState"></param>
    public FigureState ChangeState(FigureState _newState,int _type) {
        if (_newState == null)
            return myStateList[_type];
        if (_type > myStateList.Length)
            return null;
        return myStateList[_type];
    }
    /// <summary>
    /// 抽換指定的狀態
    /// </summary>
    /// <param name="_stateIndex"></param>
    public void ChangeState(FigureStateEnum _stateEnum) {
        //狀態枚舉
        int stateIndex = (int)_stateEnum;
        //過濾不合理情況
        if (myStateList.Length < (int)_stateEnum)
            return;
        if (myStateList[stateIndex] == null)
            return;
        //如前後一樣也無視
        if (currentStateEnum == _stateEnum)
            return;
        //新的狀態
        FigureStateEnum newEnum = _stateEnum;
        FigureState newState = myStateList[(int)_stateEnum];
        //替換掉舊的監聽者
        if (currentState != null)
        {
            currentState.Exit();
            RemoveActionListener = currentState;
            RemoveCombatListener = currentState;
            RemoveValueListener = currentState;
        }
        //更換成新的
        currentState = newState;
        currentStateEnum = newEnum;
        currentState.Start();
        AddActionListener = currentState;
        AddValueListener = currentState;
        AddCombatListener = currentState;
    }
    #endregion

    #region 宣告監聽事件/實做+呼叫其他監聽事件

    //
    //動作監聽
    //
    //

    List<FigureActionListener> actionlisteners = new List<FigureActionListener>();

    /// <summary>
    /// 新增事件監聽
    /// </summary>
    public FigureActionListener AddActionListener {
        set {
            if(!actionlisteners.Contains(value))
                actionlisteners.Add(value);
        }
    }

    /// <summary>
    /// 移除事件監聽
    /// </summary>
    public FigureActionListener RemoveActionListener {
        set {
                actionlisteners.Remove(value);
        }
    }


    //
    //數值監聽
    //
    //

    List<FigureValueChangeListener> valueListeners = new List<FigureValueChangeListener>();

    /// <summary>
    /// 新增數值監聽
    /// </summary>
    public FigureValueChangeListener AddValueListener {
        set {
            if (!valueListeners.Contains(value))
                valueListeners.Add(value);
        }
    }

    /// <summary>
    /// 移除數值監聽
    /// </summary>
    public FigureValueChangeListener RemoveValueListener
    {
        set { valueListeners.Remove(value); }
    }

    //
    //對抗事件監聽
    //
    //

    List<CombatListener> combatListeners = new List<CombatListener>();
    /// <summary>
    /// 新增對抗監聽事件
    /// </summary>
    public CombatListener AddCombatListener {
        set {
            if(!combatListeners.Contains(value))
                combatListeners.Add(value);
        }
    }
    /// <summary>
    /// 移除對抗監聽事件
    /// </summary>
    public CombatListener RemoveCombatListener {
        set { combatListeners.Remove(value); }
    }

    /// <summary>
    /// 對每個對戰監聽事件做操作
    /// </summary>
    /// <param name="_action"></param>
    protected void ForEachCombatListener(MyDelegate.CombatListenerEvent _action)
    {
        for (int i = 0; i < combatListeners.Count; i++)
        {
            _action(combatListeners[i]);
        }
    }


    /// <summary>
    /// 對每個動作監聽事件做操作
    /// </summary>
    /// <param name="_action"></param>
    protected void ForEachActionListener(MyDelegate.FigureActionListenerEvent _action)
    {
        for (int i = 0; i < actionlisteners.Count; i++)
        {
            _action(actionlisteners[i]);
        }
    }

    /// <summary>
    /// 對每個數值監聽事件做操作
    /// </summary>
    /// <param name="_action"></param>
    protected void ForEachValueListener(MyDelegate.FigureValueChangeListenerEvent _action)
    {
        for (int i = 0; i < valueListeners.Count; i++)
        {
            _action(valueListeners[i]);
        }
    }

    //
    //實做事件以及呼叫監聽
    //
    //

    public void Movement(float _x, float _y)
    {
        ForEachActionListener(
            (FigureActionListener _listener) => {
                _listener.Movement(_x, _y);
            }
            );
        
    }

    public void StopMovement(float _x, float _y)
    {
        ForEachActionListener(
            (FigureActionListener _listener) => {
                _listener.StopMovement(_x, _y);
            }
            );
    }

    public void HPChange(int _hp)
    {
        myValue.HP -= _hp;
        ForEachValueListener(
            (FigureValueChangeListener _listener) => {
                _listener.HPChange(_hp);
            }
            );
        
    }

    public void SpeedChange(float _times)
    {
        ForEachValueListener(
                (FigureValueChangeListener _listener) => {
                    _listener.SpeedChange(_times);
                }
            );
    }

    public void CriticleTimesChange(float _times)
    {
        ForEachValueListener(
                (FigureValueChangeListener _listener) => {
                    _listener.CriticleTimesChange(_times);
                }
            );
    }

    public void SkillChange(Skill _skill) {
        ForEachValueListener(
            _listener => _listener.SkillChange(_skill)
            );
    }

    public void Attack(int _extraData, PointerEventData _data)
    {
        ForEachCombatListener(
            (CombatListener _listener) => {
                _listener.Attack(_extraData, _data);
            }
            );
    }

    public void StopAttack(int _extraData, PointerEventData _data)
    {
        ForEachCombatListener(
            (CombatListener _listener) => {
                _listener.StopAttack(_extraData, _data);
            }
            );
    }

    public void Hitted(int _damage, Vector3 _force)
    {
        ForEachCombatListener(
            (CombatListener _listener) => {
                _listener.Hitted(_damage, _force);
            }
            );
    }

    public void Fly(Vector3 _force)
    {

        ForEachCombatListener(
            (CombatListener _listener) => {
                _listener.Fly(_force);
            }
            );
    }

    public void UseSkill(int _skillIndex, PointerEventData _data)
    {
        ForEachCombatListener(
            (CombatListener _listener) => {
                _listener.UseSkill(_skillIndex, _data);
            }
            );
    }

    public void StopSkill(int _skillIndex, PointerEventData _data)
    {
        ForEachCombatListener(
            (CombatListener _listener) => {
                _listener.StopSkill(_skillIndex, _data);
            }
            );
    }


    #endregion

    #region 輸入
    public FigureBehavior.InputType InputType {
        set { if (myBehavior != null) { myBehavior.ChangeInput(value); } }
        get { return Behavior.Type;}
    }

    /// <summary>
    /// 輸入玩家的UI
    /// 使角色設定為玩家操控
    /// </summary>
    /// <param name="_input"></param>
    public void SetUserInput(MyJoystick _movement, MyUnityUI _attack, MyUnityUI[] _skill) {
        if (myBehavior == null)
            return;
        //更新技能使得圖案跟上
        myBehavior.InputUI(_movement, _attack, _skill);
        for (int i=0;i<Skills.Length ;i++) {
            SkillChange(Skills[i]);
        }
    }

    #endregion

    #region Behavior
    protected FigureBehavior myBehavior;
    public FigureBehavior Behavior {
        set {
            myBehavior = value;
            myBehavior.FigureInitialize(this);
        }
        get { return myBehavior; }
    }

    public Transform Transform {
        get { return Behavior.Transform; }
    }

    public Vector3 Position {
        set { myBehavior.Position = value; }
    }
    public Quaternion Rotation {
        set { myBehavior.Rotation = value; }
    }
    #endregion

    #region 供State用的函式
    public virtual void MovementAction(float _x,float _y,bool _isAnimatorDo) {
        if (Behavior == null) return;

        if (_x != 0 || _y!=0)
        {
            //Rotation
            Behavior.Transform.LookAt(
                Behavior.Transform.position + new Vector3(_x, 0, _y));
        }

        //Animator
        if(_isAnimatorDo)
        AnimatorManager.Instance.SetAnimatorFloat(
            AnimatorManager.Instance.Vertical,
            Behavior.Animator,(Mathf.Abs(_x)+Mathf.Abs(_y))*0.5f);


    }

    public virtual void AttackAction(int _extraData, PointerEventData _data,bool _isStateChange) {

        //Animator
        AnimatorManager.Instance.SetAnimatorInt(
            AnimatorManager.Instance.AttackType,
            Behavior.Animator,
            _extraData
            );

    }

    /// <summary>
    /// 被攻擊時的動作
    /// </summary>
    /// <param name="_damage">傷害</param>
    /// <param name="_force">力</param>
    /// <param name="_isFixed">(棄用)是否被打退</param>
    /// <param name="isBroken">是否被擊飛</param>
    public virtual void HittedAction(int _damage,Vector3 _force,bool _isFixed,bool isBroken) {

        HPChange(_damage);

        if (!isBroken)
        {
            AnimatorManager.Instance.SetAnimatorBool(
                AnimatorManager.Instance.isBroken,
                Behavior.Animator,
                true
                );
            ChangeState(FigureStateEnum.FlyingState);
        }
    }

    public virtual void HPChangeAction(int _hp) {
        myValue.HP -= _hp;
    }

    public virtual void Dead(bool _isAnimatorDo,bool _isStateDo) {
        //Animnator
        if(_isAnimatorDo)
            AnimatorManager.Instance.SetAnimatorTrigger(
                AnimatorManager.Instance.Dead,Behavior.Animator
                );
        //FigureState
        if(_isStateDo)
            ChangeState(FigureStateEnum.DeadState);
    }

    public virtual void FlyAction(Vector3 _force,bool _isAnimatorDo,bool _isStateDo) {
        //Animator
        AnimatorManager.Instance.SetAnimatorBool(
            AnimatorManager.Instance.isBroken,
            Behavior.Animator,
            _isAnimatorDo
            );
        //State
        if(_isStateDo)
            ChangeState(FigureStateEnum.FlyingState);
    }

    public virtual void SkillAction(int _skillIndex,PointerEventData _uiData) {
        if (Skills == null)
            return;
        if (Skills.Length < _skillIndex)
            return;
        //設定技能
        CurrentSkill = Skills[_skillIndex];
        //進入施放技能的狀態
        ChangeState(FigureStateEnum.SkillState);
        //如果還是技能狀態則進入技能動作
        if(currentStateEnum== FigureStateEnum.SkillState)
            AnimatorManager.Instance.SetAnimatorInt(
                AnimatorManager.Instance.SkillType,
                Behavior.Animator,
                CurrentSkill.SkillNumber
                );
    }

    #endregion

    #region Skill
    [SerializeField]
    protected Skill[] skills = new Skill[4];
    public Skill[] Skills {
        set { skills = value; }
        get {  return skills; }
    }

    public Skill CurrentSkill = null;



    #endregion

    #region AnimationEvent
    /// <summary>
    /// 投射物體
    /// </summary>
    public void LaunchProjection() {
        currentState.LaunchProjection();
    }
    #endregion

    #region 與管理者接洽的函式

    public void DontDestoryOnLoad() {
        Behavior.DontDestoryOnLoad();
    }

    /// <summary>
    /// 儲存一般狀態的AOC
    /// 例如:
    /// 貢丸的套裝AOC
    /// </summary>
    RuntimeAnimatorController currentNormalController;

    /// <summary>
    /// 改變此套裝的AOC
    /// </summary>
    /// <param name="_aoc"></param>
    public void ChangeAOC(AnimatorOverrideController _aoc) {
        currentNormalController = Behavior.Animator.runtimeAnimatorController;
        Behavior.Animator.runtimeAnimatorController = _aoc;
    }

    /// <summary>
    /// 暫時改變AOC
    /// 可用RecoverAOC回復一般狀態
    /// 像是劇情等等的地方可以用
    /// </summary>
    /// <param name="_aoc"></param>
    public void ChangeRuntimeAnimationBriefly(AnimatorOverrideController _aoc) {
        Behavior.Animator.runtimeAnimatorController = _aoc;
        Behavior.Animator.Play(AnimatorManager.Instance.Idle,0);
    }

    /// <summary>
    /// 回復一般狀態的AOC
    /// </summary>
    public void RecoverAOC() {
        Behavior.Animator.runtimeAnimatorController = currentNormalController;
    }

    #endregion
}

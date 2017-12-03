using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public sealed class GameLogicManager {

    static private GameLogicManager instance;
    static public GameLogicManager Instance{
        get{
            if (instance == null)
            {
                instance = new GameLogicManager();
                instance.Start();
            }
            return instance;
        }
    }

    private GameMaster gm;

    public GameLogicManager() {

    }

    public GameLogicManager(GameMaster _gm) {
        gm = _gm;
    }

    /// <summary>
    /// 當此值被設定代表遊戲進入正常流程，Update將會作用
    /// </summary>
    public GameMaster GM {
        set { gm = value; }
    }

    //系統(mySystem)類組
    [SerializeField]
    FigureSystem myFigureSystem;            //人物系統
    SceneSystem mySceneSystem;              //場景系統
    

    //介面(myUIManager)類組
    ControllerManager myControlManager;     //控制UI
    PlayerStateGroup myStateManager;        //狀態UI
    LoginUIControl myLoginManager;          //登入UI
    FirstLoginControl myFirstLoginManager;  //首登UI


    /// <summary>
    /// 玩家輸入UI
    /// </summary>
    protected PlayerInputGroup PlayerInput
    {
        get
        {
            return myControlManager.UnityUI;
        }
    }

    /// <summary>
    /// 玩家狀態UI
    /// </summary>
    protected MyUIFigureState PlayerStateUI
    {
        get
        {
            return myStateManager.UnityUI;
        }
    }

    /// <summary>
    /// 登入UI
    /// </summary>
    public LoginUIGroup LoginUI {
        get {
            return myLoginManager.UnityUI;
        }
    }

    /// <summary>
    /// 玩家首次登入的選擇UI
    /// </summary>
    public FirstLoginGroup FirstLoginUI {
        get {
            return myFirstLoginManager.UnityUI;
        }
    }

    

    //角色生產器
    public FigureFactory FigureFactory {
        get {
            return myFigureSystem.FigureFactory; }
    }

    //初始化
    public void Start() {

        //UI
        myControlManager = new ControllerManager();
        myControlManager.Inject(myFigureSystem);
        myStateManager = new PlayerStateGroup();
        myStateManager.Inject(myFigureSystem);
        myLoginManager = new LoginUIControl();
        myLoginManager.Inject(myFigureSystem);
        myFirstLoginManager = new FirstLoginControl();
        
  
        //Start
        myControlManager.Start();
        myStateManager.Start();
        myLoginManager.Start();
        myFirstLoginManager.Start();

        //系統
        myFigureSystem = new FigureSystem();
        mySceneSystem = new SceneSystem();

        //Start
        myFigureSystem.Start();
        mySceneSystem.Start();
    }

    /// <summary>
    /// 每Frame呼叫
    /// </summary>
    public void Update() {
        //System
        //myFigureSystem.Update();
        //UI
        myControlManager.Update();
    }

    //創造角色時會觸發的函式
    public void CreateFigureEvent(Figure _figure) {
        //如果是玩家控制且目前玩家沒有可控制的物件時
        if (_figure.InputType == FigureBehavior.InputType.PlayerInput&&myFigureSystem.Main == null)
        {
            //將此物件設為主物件
            myFigureSystem.Main = _figure;

            //監聽技能變更
            myFigureSystem.Main.AddValueListener = PlayerInput;
            myFigureSystem.Main.SetUserInput(
                PlayerInput.Movement,
                PlayerInput.Attack,
                PlayerInput.Skills
                );

            //主攝影機看向它
            mySceneSystem.CameraTarget(_figure);

            //顯示狀態
            myStateManager.UnityUI.BindingFigure(myFigureSystem.Main);

            myFigureSystem.Main.DontDestoryOnLoad();

            return;
        }
        //其它角色(未實做)
    }

    /// <summary>
    /// 將有關玩家角色UI全數關閉
    /// </summary>
    public void HidePlayerFigureUI() {
        PlayerInput.Hide();
        PlayerStateUI.Hide();
    }

    /// <summary>
    /// 將有關玩家角色的UI全數開啟
    /// </summary>
    public void ShowPlayingFigureUI() {
        PlayerInput.Show();
        PlayerStateUI.Show();
    }

    /// <summary>
    /// 打開登入UI
    /// </summary>
    public void ShowLoginUI() {
        myLoginManager.UnityUI.Show();
    }
    /// <summary>
    /// 關閉登入UI
    /// </summary>
    public void HideLoginUI() {
        myLoginManager.UnityUI.Hide();
    }
    /// <summary>
    /// 開啟首次登入UI
    /// </summary>
    public void ShowFirstLoginUI() {
        FirstLoginUI.LoadData();
        FirstLoginUI.Show();
    }
    /// <summary>
    /// 關閉首次登入UI
    /// </summary>
    public void HideFirstLoginUI() {
        FirstLoginUI.Hide();
    }

    /// <summary>
    /// 短暫地改變玩家動作
    /// 像是劇情、登入等等
    /// </summary>
    public void ChangeMainAnimation(AnimatorOverrideController _aoc) {
        myFigureSystem.Main.ChangeRuntimeAnimationBriefly(_aoc);
    }
    /// <summary>
    /// 改變玩家位置
    /// </summary>
    /// <param name="_trans"></param>
    public void ChangeMainPosition(Vector3 _position) {
        myFigureSystem.Main.Position = _position;
    }
    /// <summary>
    /// 改變玩家旋轉
    /// </summary>
    /// <param name="_rotation"></param>
    public void ChangeMainRotation(Quaternion _rotation) {
        myFigureSystem.Main.Rotation = _rotation;
    }

    /// <summary>
    /// 改變玩家貢丸的套服，回傳結果。
    /// </summary>
    /// <returns>當主要玩家不是貢丸時回傳否</returns>
    public bool ChangeMainMeatBallSet(MeatBallSet _set) {
        MeatBall mb = myFigureSystem.Main as MeatBall;
        if (myFigureSystem.Main==null|| mb==null)
            return false;
        mb.SetSet(_set);
        return true;
    }

    /// <summary>
    /// 攝影機看向玩家
    /// </summary>
    public void CameraLookMain()
    {
        mySceneSystem.CameraTarget(myFigureSystem.Main);
    }

    /// <summary>
    /// 攝影機目標
    /// </summary>
    /// <param name="_figure"></param>
    public void CameraTarget(Figure _figure)
    {
        mySceneSystem.CameraTarget(_figure);
    }

    /// <summary>
    /// 設定攝影機位置
    /// </summary>
    /// <param name="_position"></param>
    public void CameraPosition(Vector3 _position)
    {
        mySceneSystem.CamaraPosition(_position);
    }

    /// <summary>
    /// 設定攝影機旋轉
    /// </summary>
    /// <param name="_rotation"></param>
    public void CameraRotation(Quaternion _rotation)
    {
        mySceneSystem.CameraRotation(_rotation);
    }
}

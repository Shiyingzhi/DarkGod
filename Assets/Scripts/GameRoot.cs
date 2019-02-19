using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using DarkGodAgreement;

public class GameRoot : MonoBehaviour {
    public static GameRoot instance = null;
    public LoginWin mLogonWin;
    public LoadingWin mLoadingWin;
    public HintWin mHintWin;

    public Action mRetrueAction = null;
    public Action mSyncMoveAction = null;

    private LogonSys mLogonSys;
    private ResSvc mResSvc;
    private AudioSvc mAudioSvc;
    private NetSvc mNetSvc;
    private ControllerManage mControllerMag;
    private MainGameSys mMainGameSys;
    private CombatSys mCombatSys;
    //玩家当前账号信息
    public UserDAO mUser = null;
    public List<RoleDAO> mRoleList = new List<RoleDAO>();
    public RoleDAO mCurrentRole = null;
    public 
    /// <summary>
    /// 游戏总启动
    /// </summary>
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        instance = this;
        InitCanvas();
        Init();

	}

    /// <summary>
    /// 初始化显示
    /// </summary>
    void InitCanvas()
    {
        Transform canvas = transform.Find("Canvas").transform;
        for (int i = 0; i < canvas.childCount; i++)
        {
            canvas.GetChild(i).gameObject.SetActive(false);
        }
        mHintWin.isShow();  
    }
    /// <summary>
    /// 初始化所有模块
    /// </summary>
    void Init()
    {
        //初始化服务模块
        mResSvc = GetComponent<ResSvc>();
        mResSvc.InitSvc();

        mNetSvc = GetComponent<NetSvc>();
        mNetSvc.InitSvc();

        mAudioSvc = GetComponent<AudioSvc>();
        mAudioSvc.InitAudio();

        //初始化业务系统
        mLogonSys = GetComponent<LogonSys>();
        mLogonSys.InitSys();

        mMainGameSys = GetComponent<MainGameSys>();
        mMainGameSys.InitMainGameSys();

        mCombatSys = GetComponent<CombatSys>();
        mCombatSys.InitCombatSys();
        //初始化控制器模块
        mControllerMag = new ControllerManage();
        mControllerMag.InitController();


        //进入登录系统
        mLogonSys.EnterLogin();
    }
    /// <summary>
    /// 显示提示
    /// </summary>
    /// <param name="str"></param>
    public static void ShowHint(string str)
    {
        instance.mHintWin.AddHint(str);
    }

    void FixedUpdate()
    {
        if (mRetrueAction != null)
        {
            mRetrueAction();
            mRetrueAction = null; 
        }
        if (mSyncMoveAction != null)
        {
            mSyncMoveAction();
            mSyncMoveAction = null; 
        }
    }

    

    /// <summary>
    /// 向服务器发送退出请求
    /// </summary>
    void OnDestroy()
    {
        try
        {
            mNetSvc.SendSys(GameSys.退出游戏, MethodController.EixtGame, mUser.name);

            NetSvc.instance.CloseClient();

        }
        catch(Exception e)
        {
            Debug.Log(e);
            Debug.Log("关闭客户端");
        }
    }
     
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using DarkGodAgreement;

public class GameRoot : MonoBehaviour {
    public static GameRoot intance = null;
    public LoginWin mLogonWin;
    public LoadingWin mLoadingWin;
    public HintWin mHintWin;

    public Action mRetrueAction = null;

    private LogonSys mLogonSys;
    private ResSvc mResSvc;
    private AudioSvc mAudioSvc;
    private NetSvc mNetSvc;
    private ControllerManage mControllerMag;
    private MainGameSys mMainGameSys;

    private int id;
    public int ID { get { return id; } set { id = value; } }
    /// <summary>
    /// 游戏总启动
    /// </summary>
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        intance = this;
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
        intance.mHintWin.AddHint(str);
    }

    void Update()
    {
        if (mRetrueAction != null)
        {
            Debug.Log("执行");
            mRetrueAction();
            mRetrueAction = null; 
        }
    }

    /// <summary>
    /// 向服务器发送退出请求
    /// </summary>
    void OnDestroy()
    {
        mNetSvc.SendSys(GameSys.退出游戏, MethodController.EixtGame, ID.ToString());

        NetSvc.instance.CloseClient();
        
    }
}

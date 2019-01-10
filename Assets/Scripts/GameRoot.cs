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
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        intance = this;
        InitCanvas();
        Init();

	}

    
    void InitCanvas()
    {
        Transform canvas = transform.Find("Canvas").transform;
        for (int i = 0; i < canvas.childCount; i++)
        {
            canvas.GetChild(i).gameObject.SetActive(false);
        }
        mHintWin.isShow();  
    }
    void Init()
    {
        //初始化服务模块
        mResSvc = GetComponent<ResSvc>();
        mResSvc.InitSvc();

        mNetSvc = GetComponent<NetSvc>();
        mNetSvc.InitSvc();

        mAudioSvc = GetComponent<AudioSvc>();
        mAudioSvc.Init();

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
    void OnDestroy()
    {
        mNetSvc.SendSys(GameSys.退出游戏, MethodController.EixtGame, ID.ToString());

        NetSvc.instance.CloseClient();
        
    }
}

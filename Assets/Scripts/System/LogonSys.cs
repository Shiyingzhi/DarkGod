using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogonSys : MonoBehaviour {
    public static LogonSys instance;

    public LoginWin mLoginWin;
    public CreateWin mCreateWin;
    public void InitSys()
    {
        instance = this;
    }
    /// <summary>
    /// 进入登录系统
    /// </summary>
    public void EnterLogin()
    {
        ResSvc.intance.AsyncLoadScene(GameConstant.LogonScene, () =>
            {
                mLoginWin.isShow();
                AudioSvc.instance.PlayBgClip(GameConstant.BgMuisc,true);
            });
    }

    public void SelectRole()
    {
        //释放登录面板
        mLoginWin.isShow(false);
        //进入选择角色面板
        mCreateWin.isShow();
        GameRoot.ShowHint("登录成功");

    }

}

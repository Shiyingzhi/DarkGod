using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkGodAgreement;
using UnityEngine.UI;
public class LogonSys : MonoBehaviour {
    public static LogonSys instance;

    public LoginWin mLoginWin;
    public FoundWin mFoundWin;
    public SelectRoleWin mSelectRoleWin;
    public RegisterWin mRegisterWin;
    /// <summary>
    /// 初始化登录系统
    /// </summary>
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
                AudioSvc.instance.PlayBgClip(GameConstant.BgLogon, true); 
            });
    }
    /// <summary>
    /// 进入选择角色面板
    /// </summary>
    public void SelectRole()
    {
        //提示登录
        GameRoot.ShowHint("登录成功");
        //释放登录面板
        mLoginWin.isShow(false);
        //查询账号角色
        NetSvc.instance.SendSys(GameSys.登录, MethodController.FindRole, GameRoot.instance.mUser.name);
    }

    public void FoundRole(int profession)
    {
        int roleId = 1;
        foreach (var item in mSelectRoleWin.mRoleArry)
        {
            if (item.transform.Find("Icon").GetComponent<Image>().sprite == null)
            {
                Debug.Log(roleId);
                Debug.Log(mFoundWin.mProfessionCurrent);
                string sendStr = string.Format("{0}-{1}-{2}-{3}", roleId.ToString(), profession.ToString(), mFoundWin.mInputField.text.ToString(), GameRoot.instance.mUser.name);
                NetSvc.instance.SendSys(GameSys.登录, MethodController.FoundRole, sendStr);
                return;
            }
            Debug.Log(roleId);
            roleId++;
        }
        
    }

}

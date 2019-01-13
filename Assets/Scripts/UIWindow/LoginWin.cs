using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkGodAgreement;
public class LoginWin : UIWinBase {

    public InputField mInputUser;
    public InputField mInputPass;
    /// <summary>
    /// 初始化
    /// </summary>
    protected override void InitWin()
    {
        AudioSvc.instance.PlayBgClip(GameConstant.BgLogon, true); 
        if (PlayerPrefs.HasKey("User") && PlayerPrefs.HasKey("Pass"))
        {
            mInputUser.text = PlayerPrefs.GetString("User");
            mInputPass.text = PlayerPrefs.GetString("Pass");
        }
        else
        {
            mInputUser.text = "";
            mInputPass.text = "";
        }
    }
    /// <summary>
    /// 关闭时回调
    /// </summary>
    protected override void RelWin()
    {
        this.gameObject.SetActive(false);
    }
    /// <summary>
    /// 进入游戏按钮
    /// </summary>
    public void OnInToClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButIntoGame);
        if (mInputUser.text != "" && mInputPass.text != "")
        {
            string user = mInputUser.text;
            string pass = mInputPass.text;
            PlayerPrefs.SetString("User", user);
            PlayerPrefs.SetString("Pass", pass);
            string SendStr = string.Format("{0},{1}", mInputUser.text, mInputPass.text);
            NetSvc.instance.SendSys(GameSys.登录,MethodController.LogonGame, SendStr);
            
        }
        else
        {
            GameRoot.ShowHint("用户名密码不能为空");
        }
    }
    /// <summary>
    /// 注册游戏按钮
    /// </summary>
    public void OnHelpClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        LogonSys.instance.mLoginWin.isShow(false);
        LogonSys.instance.mRegisterWin.isShow();
        
    }
}

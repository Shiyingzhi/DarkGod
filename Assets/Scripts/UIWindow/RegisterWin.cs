using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkGodAgreement;
public class RegisterWin : UIWinBase {
    public InputField mInpUser;
    public InputField mInpPassword;
    public InputField mInpRegisterPassword;
    public Animation mAnimation;
    /// <summary>
    /// 初始化
    /// </summary>
    protected override void InitWin()
    {
        mAnimation.Play();
        mInpUser.text = "";
        mInpPassword.text = "";
        mInpRegisterPassword.text = "";
    }
    /// <summary>
    /// 关闭时回调
    /// </summary>
    protected override void RelWin()
    {
        this.gameObject.SetActive(false);
    }
    /// <summary>
    /// 返回按钮
    /// </summary>
    public void OnCloseClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        LogonSys.instance.mRegisterWin.isShow(false);
        LogonSys.instance.mLoginWin.isShow();
    }
    /// <summary>
    /// 注册按钮
    /// </summary>
    public void OnRegisterButClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        if (mInpUser.text != "" && mInpPassword.text != "" || mInpRegisterPassword.text != "")
        {
            if (mInpPassword.text == mInpRegisterPassword.text)
            {
                string SendStr = string.Format("{0},{1}", mInpUser.text, mInpPassword.text);
                NetSvc.instance.SendSys(GameSys.注册, MethodController.Register, SendStr);
            }
            else
            {
                GameRoot.ShowHint("两次输入的密码不一致!");
            }
        }
        else
        {
            GameRoot.ShowHint("用户名密码不能为空！");
        }
    }
}

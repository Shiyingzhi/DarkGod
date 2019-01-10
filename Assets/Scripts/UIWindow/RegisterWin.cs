using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkGodAgreement;
public class RegisterWin : UIWinBase {
    public InputField inpUser;
    public InputField inpPassword;
    public InputField inpRegisterPassword;
    public Animation mAnimation;
    protected override void InitWin()
    {
        mAnimation.Play();
        inpUser.text = "";
        inpPassword.text = "";
        inpRegisterPassword.text = "";
    }
    protected override void RelWin()
    {
        this.gameObject.SetActive(false);
    }
    public void OnCloseClick()
    {
        LogonSys.instance.mRegisterWin.isShow(false);
        LogonSys.instance.mLoginWin.isShow();
    }

    public void OnRegisterButClick()
    {
        if (inpUser.text != "" && inpPassword.text != "" || inpRegisterPassword.text != "")
        {
            if (inpPassword.text == inpRegisterPassword.text)
            {
                string SendStr = string.Format("{0},{1}", inpUser.text, inpPassword.text);
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

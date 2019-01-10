using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkGodAgreement;
public class LoginWin : UIWinBase {

    public InputField inputUser;
    public InputField inputPass;
    public Button inToBut;
    public Button hintBut;

    protected override void InitWin()
    {
        if (PlayerPrefs.HasKey("User") && PlayerPrefs.HasKey("Pass"))
        {
            inputUser.text = PlayerPrefs.GetString("User");
            inputPass.text = PlayerPrefs.GetString("Pass");
        }
        else
        {
            inputUser.text = "";
            inputPass.text = "";
        }
    }

    protected override void RelWin()
    {
        this.gameObject.SetActive(false);
    }

    public void OnInToClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButIntoGame);
        if (inputUser.text != "" && inputPass.text != "")
        {
            string user = inputUser.text;
            string pass = inputPass.text;
            PlayerPrefs.SetString("User", user);
            PlayerPrefs.SetString("Pass", pass);
            string SendStr = string.Format("{0},{1}", inputUser.text, inputPass.text);
            NetSvc.instance.SendSys(GameSys.登录,MethodController.LogonGame, SendStr);
            LogonSys.instance.SelectRole();
        }
        else
        {
            GameRoot.ShowHint("用户名密码不能为空");
        }
    }

    public void OnHelpClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        GameRoot.ShowHint("正在开发中....");
    }
}

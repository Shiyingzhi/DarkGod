using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateWin : UIWinBase {

    public InputField mInputField;

    protected override void InitWin()
    {
        base.InitWin();
        mInputField.text = ResSvc.intance.GetXmlName(false);
    }

    public void OnRangeButClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        mInputField.text = ResSvc.intance.GetXmlName(false);
    }

    public void OnStartButClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        if (mInputField.text == "")
        {
            GameRoot.ShowHint("输入的名字不合法！请重新输入");
        }
        else
        {
            //TODO
            //发送网络请求
            ResSvc.intance.AsyncLoadScene(GameConstant.MainGameScene, () =>
                {
                    LogonSys.instance.mLoginWin.isShow(false);
                    LogonSys.instance.mCreateWin.isShow(false);
                    MainGameSys.instance.mGameWin.isShow();
                });
        }
    }
}

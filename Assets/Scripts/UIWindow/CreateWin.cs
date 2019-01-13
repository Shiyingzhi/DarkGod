using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateWin : UIWinBase {

    public InputField mInputField;

    /// <summary>
    /// 初始化
    /// </summary>
    protected override void InitWin()
    {
        base.InitWin();
        mInputField.text = ResSvc.intance.GetXmlName(false);
    }
    /// <summary>
    /// 随机名字按钮
    /// </summary>
    public void OnRangeButClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        mInputField.text = ResSvc.intance.GetXmlName(false);
    }
    /// <summary>
    /// 进入游戏按钮
    /// </summary>
    public void OnStartButClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        if (mInputField.text == "")
        {
            GameRoot.ShowHint("输入的名字不合法！请重新输入");
        }
        else
        {
            ResSvc.intance.AsyncLoadScene(GameConstant.MainGameScene, () =>
                {
                    //打开主场景UI
                    LogonSys.instance.mLoginWin.isShow(false);
                    LogonSys.instance.mCreateWin.isShow(false);
                    MainGameSys.instance.mGameWin.isShow();
                });
        }
    }
}

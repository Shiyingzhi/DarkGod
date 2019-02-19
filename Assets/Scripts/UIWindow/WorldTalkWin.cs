using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkGodAgreement;
public class WorldTalkWin : UIWinBase {
    public InputField mInputField;
    public Text mWorldText;
    public CanvasGroup mCanvasGroup;
    private string mSendString;

    protected override void InitWin()
    {
        mCanvasGroup.alpha = 1;
        mCanvasGroup.blocksRaycasts = true;
    }
    protected override void RelWin()
    {
        mCanvasGroup.alpha = 0;
        mCanvasGroup.blocksRaycasts = false;
    }
    public void OnCloseButClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        isTalk(false); 
    }

    public void OnSendButClick()
    {
        mSendString = "<color=#0000ff>"+MainGameSys.instance.mCurrentRole.playerName + "</color>:" + mInputField.text + "\n";
        mWorldText.text += mSendString;
        NetSvc.instance.SendSys(GameSys.聊天, MethodController.WorldTalk, mSendString);
        mInputField.text = "";
        if (MainGameSys.instance.mCurrentRole.worldTalkNum < 3)
        {
            MainGameSys.instance.mCurrentRole.worldTalkNum++;
            string Senddata = string.Format("{0}-{1}-{2}-{3}-{4}", MainGameSys.instance.mCurrentRole.combartNum.ToString()
                , MainGameSys.instance.mCurrentRole.intensifyNum.ToString()
                , MainGameSys.instance.mCurrentRole.killMOBSNum.ToString()
                , MainGameSys.instance.mCurrentRole.worldTalkNum.ToString()
                , MainGameSys.instance.mCurrentRole.killBossNum.ToString());
            NetSvc.instance.SendSys(GameSys.任务, MethodController.DailyTask, Senddata);
        }
    }
    public void UpdateWorldTalk(string talk)
    {
        mWorldText.text += talk;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkGodAgreement;
public class GuideWin : UIWinBase {

    public Text mName;
    public Text mTalk;
    public Image mIcon;

    public AutoGuideCfg mAutoGuideCfg;
    private int index;
    private string[] strArry;

    public bool CombastTask = false;
    public bool IntensifyTask = false;
    protected override void InitWin()
    {
        index = 1;
        mAutoGuideCfg = MainGameSys.instance.GetAutoGuideCfg();
        strArry = mAutoGuideCfg.dilogArr.Split('#');
        SetTalk();
    }

    public void SetTalk()
    {
        string[] talkArry = strArry[index].Split('|');
        if (talkArry[0] == "0")
        {
            mIcon.sprite = ResSvc.intance.LoadSprite(GameConstant.AssassinBg);
            mName.text = MainGameSys.instance.mCurrentRole.playerName;
        }
        else
        {
            switch (mAutoGuideCfg.npcID)
            {
                case 0:
                    mIcon.sprite = ResSvc.intance.LoadSprite(GameConstant.Wiseman);
                    mName.text = "智者";
                    break;
                case 1:
                    mIcon.sprite = ResSvc.intance.LoadSprite(GameConstant.General);
                    mName.text = "将军";
                    break;
                case 2:
                    mIcon.sprite = ResSvc.intance.LoadSprite(GameConstant.Artisan);
                    mName.text = "工匠";
                    break;
                case 3:
                    mIcon.sprite = ResSvc.intance.LoadSprite(GameConstant.Trader);
                    mName.text = "商人";
                    break;
                default:
                    mIcon.sprite = ResSvc.intance.LoadSprite(GameConstant.Guide);
                    mName.text = "小精灵";
                    break;
            }
        }
        mIcon.SetNativeSize();
        mTalk.text = talkArry[1].Replace("$name", MainGameSys.instance.mCurrentRole.playerName);
    }
    public void OnNextClick() 
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        index++;
        if (index == strArry.Length)
        {
            switch (MainGameSys.instance.mCurrentRole.guideID)
            {
                case 1001:
                    //发送完成任务请求
                    NetSvc.instance.SendSys(GameSys.任务, MethodController.Perform, MainGameSys.instance.mCurrentRole.guideID.ToString());
                    break;
                case 1002:
                    NetSvc.instance.SendSys(GameSys.任务, MethodController.Perform, MainGameSys.instance.mCurrentRole.guideID.ToString());
                    GameRoot.ShowHint("通关荒芜废墟");
                    MainGameSys.instance.OnCustomsPassClick();
                    
                    break;
                case 1003:
                    NetSvc.instance.SendSys(GameSys.任务, MethodController.Perform, MainGameSys.instance.mCurrentRole.guideID.ToString());
                    GameRoot.ShowHint("强化一次装备");
                    MainGameSys.instance.OnIntensifyButCilck();
                    break;
                case 1004:
                    NetSvc.instance.SendSys(GameSys.任务, MethodController.Perform, MainGameSys.instance.mCurrentRole.guideID.ToString());
                    GameRoot.ShowHint("开始你的冒险之旅吧");
                    break;
            }

            //关闭面板
            isShow(false);
            return;
        }
        SetTalk();
    }
}

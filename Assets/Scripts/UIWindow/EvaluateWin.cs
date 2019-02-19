using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DarkGodAgreement;

public class EvaluateWin : UIWinBase {
    public Image mProfessionIcon;
    public Image mStar1;
    public Image mStar2;
    public Image mStar3;
    public Text mBeHitNum;
    public Text mWinTimer;
    public Image mEvaluateLevel;
    public Animation mAnimation;

    private int mEvaluateLevelNum;
    protected override void InitWin()
    {
        mEvaluateLevelNum = 1;
        MainGameSys.instance.mGuideeWin.CombastTask = true;
        if (MainGameSys.instance.mCurrentRole.combartNum < 3)
        {
            MainGameSys.instance.mCurrentRole.combartNum++;
            string Senddata = string.Format("{0}-{1}-{2}-{3}-{4}", MainGameSys.instance.mCurrentRole.combartNum.ToString()
                , MainGameSys.instance.mCurrentRole.intensifyNum.ToString()
                , MainGameSys.instance.mCurrentRole.killMOBSNum.ToString()
                , MainGameSys.instance.mCurrentRole.worldTalkNum.ToString()
                , MainGameSys.instance.mCurrentRole.killBossNum.ToString());
            NetSvc.instance.SendSys(GameSys.任务, MethodController.DailyTask, Senddata);
        }
        switch (MainGameSys.instance.mCurrentRole.profession)
        {
            case DarkGodAgreement.Profession.Null:
                break;
            case DarkGodAgreement.Profession.暗影刺客:
                mProfessionIcon.sprite = ResSvc.intance.LoadSprite(GameConstant.AssassinBg);
                break;
        }

        if (CombatSys.instance.WinTimer < 120)
        {
            mEvaluateLevelNum++;
            mStar2.sprite = ResSvc.intance.LoadSprite(GameConstant.StarTrue);
        }
        else
        {
            mStar2.sprite = ResSvc.intance.LoadSprite(GameConstant.StarFales);
        }
        if (CombatSys.instance.BeHitNum <= 5)
        {
            mEvaluateLevelNum++;
            mStar3.sprite = ResSvc.intance.LoadSprite(GameConstant.StarTrue);
        }
        else
        {
            mStar3.sprite = ResSvc.intance.LoadSprite(GameConstant.StarFales);
        }
        mBeHitNum.text = "被攻击次数 :" + CombatSys.instance.BeHitNum.ToString();
        TimeSpan ts = new TimeSpan(0, 0, (int)(CombatSys.instance.WinTimer));
        mWinTimer.text = "通关时间：" + ts.Minutes.ToString() +"分" + ts.Seconds.ToString() + "秒";
        switch (mEvaluateLevelNum)
        {
            case 1:
                mEvaluateLevel.sprite = ResSvc.intance.LoadSprite("ResImages/fb-db");
                break;
            case 2:
                mEvaluateLevel.sprite = ResSvc.intance.LoadSprite("ResImages/fb-da");
                break;
            case 3:
                mEvaluateLevel.sprite = ResSvc.intance.LoadSprite("ResImages/fb-ds");
                break;
        }
        mAnimation.Play();
        string SendData = string.Format("{0}*{1}*{2}", "3000", "1630", "26");
        NetSvc.instance.SendSys(GameSys.奖励, MethodController.GetAward, SendData);
        CombatSys.instance.BeHitNum = 0;
        CombatSys.instance.WinTimer = 0;
    }


    public void RepetitionButClick()
    {
        isShow(false);
        CombatSys.instance.isCombat = false;
        CombatSys.instance.CloseCombar();
        MainGameSys.instance.IntoCombat(CombatSys.instance.CombarNum, MainGameSys.instance.SceneName, MainGameSys.instance.SceneCombarId);
    }

    public void ReturnButClick()
    {
        isShow(false);
        CombatSys.instance.isCombat = false;
        MainGameSys.instance.mGameWin.HideOnOff(0);
        CombatSys.instance.CloseCombar();
        MainGameSys.instance.ReturnCity();
    }
}

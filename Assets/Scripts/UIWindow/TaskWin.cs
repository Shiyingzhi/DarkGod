using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkGodAgreement;
public class TaskWin : UIWinBase {
    public Image mCombartShade;
    public Image mIntensifyShade;
    public Image mKillMOBSShade;
    public Image mWorldTalkShade;
    public Image mKillBossShade;

    public Image Task1Shade;
    public Image Task2Shade;
    public Image Task3Shade;
    public Image Task4Shade;
    public Image Task5Shade;

    public Text mCombartNum;
    public Text mIntensifyNum;
    public Text mKillMOBSNum;
    public Text mWorldTalkNum;
    public Text mKillBossNum;
    private int mCombartNumMax = 3;
    private int mIntensifyNumMax = 5;
    private int mKillMOBSNumMax = 15;
    private int mWorldTalkNumMax = 3;
    private int mKillBossNumMax = 3;

    protected override void InitWin()
    {
        TaskInit();
    }

    public void TaskInit()
    {
        Debug.Log(MainGameSys.instance.mCurrentRole.intensifyNum);
        mCombartNum.text = MainGameSys.instance.mCurrentRole.combartNum.ToString() + "/" + mCombartNumMax.ToString();
        mCombartShade.fillAmount = (float)MainGameSys.instance.mCurrentRole.combartNum / (float)mCombartNumMax;
        mIntensifyNum.text = MainGameSys.instance.mCurrentRole.intensifyNum.ToString() + "/" + mIntensifyNumMax.ToString();
        mIntensifyShade.fillAmount = (float)MainGameSys.instance.mCurrentRole.intensifyNum / (float)mIntensifyNumMax;
        mKillMOBSNum.text = MainGameSys.instance.mCurrentRole.killMOBSNum.ToString() + "/" + mKillMOBSNumMax.ToString();
        mKillMOBSShade.fillAmount = (float)MainGameSys.instance.mCurrentRole.killMOBSNum / (float)mKillMOBSNumMax;
        mWorldTalkNum.text = MainGameSys.instance.mCurrentRole.worldTalkNum.ToString() + "/" + mWorldTalkNumMax.ToString();
        mWorldTalkShade.fillAmount = (float)MainGameSys.instance.mCurrentRole.worldTalkNum / (float)mWorldTalkNumMax;
        mKillBossNum.text = MainGameSys.instance.mCurrentRole.killBossNum.ToString() + "/" + mKillBossNumMax.ToString();
        mKillBossShade.fillAmount = (float)MainGameSys.instance.mCurrentRole.killBossNum / (float)mKillBossNumMax;
    }


    public void OnTask1Click()
    {
        if (MainGameSys.instance.mCurrentRole.combartNum == mCombartNumMax)
        {
            string SendData = string.Format("{0}*{1}*{2}","1200","888","0");
            NetSvc.instance.SendSys(GameSys.奖励, MethodController.GetAward, SendData);
            Task1Shade.gameObject.SetActive(true);
        }
        else
        {
            GameRoot.ShowHint("未满足条件");
        }
    }
    public void OnTask2Click()
    {
        if (MainGameSys.instance.mCurrentRole.intensifyNum == mIntensifyNumMax)
        {
            string SendData = string.Format("{0}*{1}*{2}", "1600", "1888", "0");
            NetSvc.instance.SendSys(GameSys.奖励, MethodController.GetAward, SendData);
            Task2Shade.gameObject.SetActive(true);
        }
        else
        {
            GameRoot.ShowHint("未满足条件");
        }
    }
    public void OnTask3Click()
    {
        if (MainGameSys.instance.mCurrentRole.killMOBSNum == mKillMOBSNumMax)
        {
            string SendData = string.Format("{0}*{1}*{2}", "2100", "2888", "0");
            NetSvc.instance.SendSys(GameSys.奖励, MethodController.GetAward, SendData);
            Task3Shade.gameObject.SetActive(true);
        }
        else
        {
            GameRoot.ShowHint("未满足条件");
        }
    }
    public void OnTask4Click()
    {
        if (MainGameSys.instance.mCurrentRole.worldTalkNum == mWorldTalkNumMax)
        {
            string SendData = string.Format("{0}*{1}*{2}", "3200", "3888", "0");
            NetSvc.instance.SendSys(GameSys.奖励, MethodController.GetAward, SendData);
            Task4Shade.gameObject.SetActive(true);
        }
        else
        {
            GameRoot.ShowHint("未满足条件");
        }
    }
    public void OnTask5Click()
    {
        if (MainGameSys.instance.mCurrentRole.killBossNum == mKillBossNumMax)
        {
            string SendData = string.Format("{0}*{1}*{2}", "3500", "3888", "0");
            NetSvc.instance.SendSys(GameSys.奖励, MethodController.GetAward, SendData);
            Task5Shade.gameObject.SetActive(true);
        }
        else
        {
            GameRoot.ShowHint("未满足条件");
        }
        
    }

    public void OnCloseClick()
    {
        isShow(false);
    }
}

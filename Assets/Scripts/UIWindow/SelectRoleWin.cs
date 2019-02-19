using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectRoleWin : UIWinBase {

    public Image[] mRoleArry;
    public Toggle[] mTooggleArry;
    protected override void InitWin()
    {
        if (GameRoot.instance.mRoleList.Count == 0)
        {
            GameRoot.ShowHint("您还没有角色");
            return;
        }
        for (int i = 0; i <= GameRoot.instance.mRoleList.Count-1; i++)
        {
            Debug.Log("有角色执行了");
            mRoleArry[i].gameObject.transform.Find("Profession").GetComponent<Text>().text ="职业:" + GameRoot.instance.mRoleList[i].profession;
            mRoleArry[i].gameObject.transform.Find("PlayerName").GetComponent<Text>().text = GameRoot.instance.mRoleList[i].playerName;
            mRoleArry[i].gameObject.transform.Find("LV").GetComponent<Text>().text ="等级：" + GameRoot.instance.mRoleList[i].lv.ToString();
            switch (GameRoot.instance.mRoleList[i].profession)
            {
                case DarkGodAgreement.Profession.Null:
                    break;
                case DarkGodAgreement.Profession.暗影刺客:
                                mRoleArry[i].transform.Find("Icon").GetComponent<Image>().sprite = ResSvc.intance.LoadSprite(GameConstant.AssassinBg);
            mRoleArry[i].transform.Find("Icon").GetComponent<Image>().color = Color.white;
                    break;
            }

        }

    }

    public void OnFoundClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        if (mRoleArry[5].transform.Find("Icon").GetComponent<Image>().sprite !=null )
        {
            GameRoot.ShowHint("角色已经满了无法继续创建");
            return;
        }
        LogonSys.instance.mSelectRoleWin.isShow(false);
        LogonSys.instance.mFoundWin.isShow();
    }

    public void OnIntoClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        try
        {
            for (int i = 0; i < mTooggleArry.Length; i++)
            {
                if (mTooggleArry[i].isOn)
                {
                    if (MainGameSys.instance.SetRole(i))
                    {
                        isShow(false);
                        MainGameSys.instance.InToMainCity();
                        return;
                    }

                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            GameRoot.ShowHint("请选择一个角色");
        }
        
        
    }


}

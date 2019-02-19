using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DarkGodAgreement;
public class IntensifyWin : UIWinBase {
    public Image mIcom;
    public Image[] mStarLv;
    public Text mCurrentStarLv;
    public Text mCurrentAddHp;
    public Text mCurrentAddAttack;
    public Text mCurrentAddDefense;
    public Text mToAddHp;
    public Text mToAddAttack;
    public Text mToAddDefenes;
    public Text mNeedLv;
    public Text mNeedCoin;
    public Text mNeedCrystal;
    public Text mCurrentCoin;

    public Toggle[] EquipToggle;

    private bool isShowIntensify = false;
    private StrongCfg Strong;
    private bool isImax = false;
    void Update()
    {
        if (isShowIntensify)
        {
            SetEquipPlane();
        }
    }
    protected override void InitWin()
    {
        isShowIntensify = true;
        SetEquipPlane();
    }
    protected override void RelWin()
    {
        isShowIntensify = false;
    }
    private void SetIconAndStarLv(int pos,int StarLv)
    {
        if (StarLv < 10)
        {
            isImax = false;
            mCurrentStarLv.text = StarLv.ToString() + "星级";
            mCurrentCoin.text = MainGameSys.instance.mCurrentRole.coin.ToString();
            Strong = ResSvc.intance.GetStrongCfg(pos, StarLv + 1);
            mToAddHp.text = "强化后     +" + Strong.mAddhp.ToString();
            mToAddAttack.text = "+" + Strong.mAddhurt.ToString();
            mToAddDefenes.text = "+" + Strong.mAdddef.ToString();

            mNeedLv.text = Strong.mMinlv.ToString();
            mNeedCoin.text = Strong.mCoin.ToString();
            mNeedCrystal.text = MainGameSys.instance.mCurrentRole.crystal.ToString() + "/" + Strong.mCrystal.ToString();
        }
        else
        {
            isImax = true;
            mCurrentStarLv.text = StarLv.ToString() + "星级";
            mCurrentCoin.text = MainGameSys.instance.mCurrentRole.coin.ToString();
            Strong = ResSvc.intance.GetStrongCfg(pos, StarLv);
            mToAddHp.text = "Imax";
            mToAddAttack.text = "Imax";
            mToAddDefenes.text = "Imax";

            mNeedLv.text = "Imax";
            mNeedCoin.text = "Imax";
            mNeedCrystal.text = MainGameSys.instance.mCurrentRole.crystal.ToString() + "/" + "Imax";
        }

        switch (pos)
        {
            case 0:
                mIcom.sprite = ResSvc.intance.LoadSprite(GameConstant.HeadIcon);
                mCurrentAddHp.text = "Hp:" + MainGameSys.instance.mCurrentRole.hp.ToString()+"    当前";
                mCurrentAddAttack.text = "力量:" + MainGameSys.instance.mCurrentRole.strength.ToString();
                mCurrentAddDefense.text = "体力:" + MainGameSys.instance.mCurrentRole.physicalPower.ToString();
                break;
            case 1:
                mIcom.sprite = ResSvc.intance.LoadSprite(GameConstant.BodyIcon);
                mCurrentAddHp.text = "Hp:" + MainGameSys.instance.mCurrentRole.hp.ToString() + "    当前";
                mCurrentAddAttack.text = "敏捷:" + MainGameSys.instance.mCurrentRole.agility.ToString();
                mCurrentAddDefense.text = "体力:" + MainGameSys.instance.mCurrentRole.physicalPower.ToString();
                break;
            case 2:
                mIcom.sprite = ResSvc.intance.LoadSprite(GameConstant.WaistIcon);
                mCurrentAddHp.text = "敏捷:" + MainGameSys.instance.mCurrentRole.agility.ToString() + "    当前";
                mCurrentAddAttack.text = "智力:" + MainGameSys.instance.mCurrentRole.intelligence.ToString();
                mCurrentAddDefense.text = "体力:" + MainGameSys.instance.mCurrentRole.physicalPower.ToString();
                break;
            case 3:
                mIcom.sprite = ResSvc.intance.LoadSprite(GameConstant.HandIcon);
                mCurrentAddHp.text = "力量:" + MainGameSys.instance.mCurrentRole.strength.ToString() + "    当前";
                mCurrentAddAttack.text = "智力:" + MainGameSys.instance.mCurrentRole.intelligence.ToString();
                mCurrentAddDefense.text = "体力:" + MainGameSys.instance.mCurrentRole.physicalPower.ToString();
                break;
            case 4:
                mIcom.sprite = ResSvc.intance.LoadSprite(GameConstant.LegIcon);
                mCurrentAddHp.text = "力量:" + MainGameSys.instance.mCurrentRole.strength.ToString() + "    当前";
                mCurrentAddAttack.text = "智力:" + MainGameSys.instance.mCurrentRole.intelligence.ToString();
                mCurrentAddDefense.text = "体力:" + MainGameSys.instance.mCurrentRole.physicalPower.ToString();
                break;
            case 5:
                mIcom.sprite = ResSvc.intance.LoadSprite(GameConstant.FootIcon);
                mCurrentAddHp.text = "力量:" + MainGameSys.instance.mCurrentRole.strength.ToString() + "    当前";
                mCurrentAddAttack.text = "智力:" + MainGameSys.instance.mCurrentRole.intelligence.ToString();
                mCurrentAddDefense.text = "敏捷:" + MainGameSys.instance.mCurrentRole.agility.ToString();
                break;
        }

        for (int i = 0; i < mStarLv.Length; i++)
        {
            if (i < StarLv)
            {
                mStarLv[i].sprite = ResSvc.intance.LoadSprite(GameConstant.StarTrue);
            }
            else
            {
                mStarLv[i].sprite = ResSvc.intance.LoadSprite(GameConstant.StarFales);
            }
        }

    }

    private void SetEquipPlane()
    {
        Debug.Log(EquipToggle.Length);
        for (int i = 0; i < EquipToggle.Length; i++)
        {
            if (EquipToggle[i].isOn)
            {
                Debug.Log(MainGameSys.instance.mCurrentRole.equipLv[i]);
                SetIconAndStarLv(i, MainGameSys.instance.mCurrentRole.equipLv[i]);
            }
        }
    }

    public void OnIntensifyButClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        if (isImax)
        {
            GameRoot.ShowHint("当前星级已满~!!!");
            return;
        }
        if (Strong.mStarlv <= 10)
        {
            if (MainGameSys.instance.mCurrentRole.coin < Strong.mCoin)
            {
                Debug.Log(Strong.mCoin);
                GameRoot.ShowHint("金币数量不足");
                return;
            }
            if (MainGameSys.instance.mCurrentRole.lv < Strong.mMinlv)
            {
                GameRoot.ShowHint("等级不符合要求");
                return;
            }
            if (MainGameSys.instance.mCurrentRole.crystal < Strong.mCrystal)
            {
                GameRoot.ShowHint("水晶数量不足");
                return;
            }
            //强化升级装备
            string data = string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-", Strong.mPos.ToString(), Strong.mStarlv.ToString(), Strong.mAddhp.ToString(), Strong.mAddhurt.ToString(), Strong.mAdddef.ToString(), Strong.mCoin, Strong.mCrystal);
            NetSvc.instance.SendSys(GameSys.强化, MethodController.IntensifyEquip, data);
            
        }
        
        
    }
    public void CloseButClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        MainGameSys.instance.OnCloseIntensifyButCilck();
    }
}

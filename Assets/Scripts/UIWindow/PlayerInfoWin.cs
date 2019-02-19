using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInfoWin : UIWinBase {

    public Text mExp;
    public Image mExpStrip;
    public Text mTired;
    public Image mTiredStrip;
    public Text mProfession;
    public Text mAttackPower;
    public Text mDamage;
    public Text mDefense;
    public Text mPlayerName;
    public Text mLv;
    protected override void InitWin()
    {
        mExp.text = MainGameSys.instance.mCurrentRole.currentExp.ToString() + "/" + MainGameSys.instance.mCurrentRole.exp.ToString();
        mExpStrip.fillAmount = (float)MainGameSys.instance.mCurrentRole.currentExp / (float)MainGameSys.instance.mCurrentRole.exp;
        mTired.text = MainGameSys.instance.mCurrentRole.currentTired.ToString() + "/" + MainGameSys.instance.mCurrentRole.tired.ToString();
        mTiredStrip.fillAmount = MainGameSys.instance.mCurrentRole.currentTired / MainGameSys.instance.mCurrentRole.tired;
        mProfession.text = MainGameSys.instance.mCurrentRole.profession.ToString();
        mAttackPower.text = MainGameSys.instance.mCurrentRole.attackNum.ToString();
        mDamage.text = ((MainGameSys.instance.mCurrentRole.strength + MainGameSys.instance.mCurrentRole.intelligence) * 2).ToString();
        mDefense.text = ((MainGameSys.instance.mCurrentRole.physicalPower + MainGameSys.instance.mCurrentRole.agility) * 2).ToString();
        mPlayerName.text = MainGameSys.instance.mCurrentRole.playerName;
        mLv.text = "Lv:" + MainGameSys.instance.mCurrentRole.lv.ToString();
    }
    protected override void RelWin()
    {
        this.gameObject.SetActive(false);
        MainGameSys.instance.HideInfo();
    }
    public void Hied()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        isShow(false);
    }

}

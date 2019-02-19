using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomsPassWin : UIWinBase {
    public Image mLevel1Shade;
    public Image mLevel2Shade;
    public Image mLevel3Shade;
    public Image mLevel4Shade;
    public Image mLevel5Shade;
    public Image mLevel6Shade;
    public Image mLevel7Shade;
    public Image mLevel8Shade;
    public Image mLevel9Shade;
    public Image mLevel10Shade;

    public Animation mAnimation;
    private int PageNum = 1;
    protected override void InitWin()
    {
        ReleaseLevels(MainGameSys.instance.mCurrentRole.lv);
    }

    public void ReleaseLevels(int lv)
    {
        if (lv > 0)
        {
            mLevel1Shade.gameObject.SetActive(false);
            if (lv > 3)
            {
                mLevel2Shade.gameObject.SetActive(false);
                if (lv > 5)
                {
                    mLevel3Shade.gameObject.SetActive(false);
                    if (lv > 7)
                    {
                        mLevel4Shade.gameObject.SetActive(false);
                        if (lv > 10)
                        {
                            mLevel5Shade.gameObject.SetActive(false);
                            if (lv > 15)
                            {
                                mLevel6Shade.gameObject.SetActive(false);
                                if (lv > 18)
                                {
                                    mLevel7Shade.gameObject.SetActive(false);
                                    if (lv > 22)
                                    {
                                        mLevel8Shade.gameObject.SetActive(false);
                                        if (lv > 26)
                                        {
                                            mLevel9Shade.gameObject.SetActive(false);
                                            if (lv > 30)
                                            {
                                                mLevel10Shade.gameObject.SetActive(false);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void OnRightClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        Debug.Log("321");
        if (PageNum == 2) return;
        PageNum = 2;
        mAnimation.Play("CustomsPass");
    }

    public void OnLeftClick() 
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        Debug.Log("123");
        if (PageNum == 1) return;
        PageNum = 1;
        mAnimation.Play("CustomsPassBack");
    }
    public void OnmCustomsPassCloseClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        isShow(false);
    }

    public void OnLevel1ButClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
         MainGameSys.instance.IntoCombat(GameConstant.Combat1,GameConstant.CombatScene,GameConstant.CombatSceneId);
    }
    public void OnLevel2ButClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        GameRoot.ShowHint("更多关卡敬请期待！！！");
    }
}

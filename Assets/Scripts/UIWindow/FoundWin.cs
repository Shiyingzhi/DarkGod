using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkGodAgreement;
public class FoundWin : UIWinBase {

    public InputField mInputField;
    public Text mProfession;
    public int mProfessionCurrent = 1;      //所选择的职业
    public Text mProDis;

    private Dictionary<Profession, string> ProfessionDisDic = new Dictionary<Profession, string>();
    /// <summary>
    /// 初始化
    /// </summary>
    protected override void InitWin()
    {
        try
        {
            ProfessionDisDic.Add(Profession.暗影刺客, "拥有性感的躯壳却掩饰不了黑暗和冷酷，善用匕首与月刃，可以杀人于无形之中");
            mProfession.text = ((Profession)mProfessionCurrent).ToString();
            mProDis.text = ProfessionDisDic[(Profession)mProfessionCurrent];
            mInputField.text = ResSvc.intance.GetXmlName(false);
        }
        catch
        {
 
        }
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
    /// 创建角色按钮
    /// </summary>
    public void OnFoundButClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        if (mInputField.text == "")
        {
            GameRoot.ShowHint("输入的名字不合法！请重新输入");
        }
        else
        {
            Debug.Log("创建角色");
            LogonSys.instance.FoundRole(mProfessionCurrent);
            
        }
    }
    public void OnLeftClick()
    {
        if (mProfessionCurrent > ProfessionDisDic.Count)
        {
            mProfessionCurrent = 1;
        }
        mProfessionCurrent++;
        mProfession.text = ((Profession)mProfessionCurrent).ToString();
        mProDis.text = ProfessionDisDic[(Profession)mProfessionCurrent];
    }

    public void OnReghtClick()
    {
        if (mProfessionCurrent < 1)
        {
            mProfessionCurrent = ProfessionDisDic.Count;
        }
        mProfessionCurrent++;
        mProfession.text = ((Profession)mProfessionCurrent).ToString();
        mProDis.text = ProfessionDisDic[(Profession)mProfessionCurrent];
    }
}

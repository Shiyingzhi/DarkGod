using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonsterHp : UIWinBase {
    public Image mHpShade;
    public Image mIcon;
    public Text mMonsterName;
    
    public void UpdateHp(string iconStr,string name,float val)
    {
        mIcon.sprite = ResSvc.intance.LoadSprite("ResImages/" + iconStr);
        mMonsterName.text = name;
        mHpShade.fillAmount = val;
    }
}

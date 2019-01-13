using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingWin : UIWinBase {

    public Text mTextTips;
    public Image mImgFG;
    public Image mImgPoint;
    public Text mTextPrg;

    private float fgWidth;
    /// <summary>
    /// 初始化
    /// </summary>
    protected override void InitWin()
    {
        fgWidth = GetComponent<RectTransform>().sizeDelta.x;

        mTextTips.text = "";
        mTextPrg.text = "0%";
        mImgFG.fillAmount = 0;
        mImgPoint.transform.localPosition = new Vector3(-620, 5.3f, 0);
    }
    /// <summary>
    /// 更新进度条
    /// </summary>
    /// <param name="prg"></param>
    public void SetProgress(float prg)
    {
        mTextPrg.text = ((int)(prg * 100)).ToString() + "%";
        mImgFG.fillAmount = prg;

        mImgPoint.GetComponent<RectTransform>().anchoredPosition = new Vector2(prg * fgWidth, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingWin : UIWinBase {

    public Text textTips;
    public Image imgFG;
    public Image imgPoint;
    public Text textPrg;

    private float fgWidth;
    protected override void InitWin()
    {
        fgWidth = GetComponent<RectTransform>().sizeDelta.x;

        textTips.text = "";
        textPrg.text = "0%";
        imgFG.fillAmount = 0;
        imgPoint.transform.localPosition = new Vector3(-620, 5.3f, 0);
    }
    public void SetProgress(float prg)
    {
        textPrg.text = ((int)(prg * 100)).ToString() + "%";
        imgFG.fillAmount = prg;

        imgPoint.GetComponent<RectTransform>().anchoredPosition = new Vector2(prg * fgWidth, 0);
    }
}

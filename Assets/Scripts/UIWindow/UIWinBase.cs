using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWinBase : MonoBehaviour {
    /// <summary>
    /// 显示/隐藏当前UI
    /// </summary>
    /// <param name="isShow"></param>
    public void isShow(bool isShow = true)
    {
        if (this.gameObject.activeSelf != isShow)
        {
            this.gameObject.SetActive(isShow);
        }
        if (isShow == true)
        {
            InitWin();
        }
        else
        {
            RelWin();
        }
    }
    /// <summary>
    /// 初始化UI
    /// </summary>
    protected virtual void InitWin() { }
    /// <summary>
    /// 释放UI
    /// </summary>
    protected virtual void RelWin() { }
    
}

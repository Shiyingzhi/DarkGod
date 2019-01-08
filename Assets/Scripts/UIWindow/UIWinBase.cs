using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWinBase : MonoBehaviour {

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

    protected virtual void InitWin() { }
    protected virtual void RelWin() { }
    
}

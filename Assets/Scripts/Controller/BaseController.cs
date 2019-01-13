using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DarkGodAgreement;
using UnityEngine;
public abstract class BaseController
{
    protected ReturnSys mCurrentReturnSys;
    private Action mExecute = null;
    /// <summary>
    /// 处理服务器的消息
    /// </summary>
    public virtual void ProcessMessage(ReturnSys sys,string data) { }
    /// <summary>
    /// 执行事件
    /// </summary>
    /// <param name="sys"></param>
    /// <param name="action"></param>
    public void DisposeData(ReturnSys sys,Action action)
    {
        if (mCurrentReturnSys == sys)
        {
            GameRoot.intance.mRetrueAction = action;
        }
        else
        {
            Debug.LogError("返回类型不匹配！当前控制器：" + mCurrentReturnSys + "传递控制器：" + sys);
        }
    }
    
}


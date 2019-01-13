using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class HintWin : UIWinBase {

    public Animation mAnimation;
    public Text mText;

    private Queue<string> HintQue = new Queue<string>();
    private bool isShowHint;
    /// <summary>
    /// 初始化
    /// </summary>
    protected override void InitWin()
    {
        mText.gameObject.SetActive(false);
    }
    /// <summary>
    /// 添加显示信息
    /// </summary>
    /// <param name="str"></param>
    public void AddHint(string str)
    {
        lock (HintQue)
        {
            HintQue.Enqueue(str);
        }
    }
    void Update()
    {
        if (HintQue.Count > 0&&isShowHint ==false)
        {
            isShowHint = true;
            string str = HintQue.Dequeue();
            ShowHint(str);
        }
    }
    /// <summary>
    /// 显示提示信息
    /// </summary>
    /// <param name="str"></param>
    private void ShowHint(string str)
    {
        mText.gameObject.SetActive(true);
        mText.text = str;
        AnimationClip clip = mAnimation.clip;
        mAnimation.Play();

        StartCoroutine(WaitTime(clip.length, () =>
            {
                isShowHint = false;
                mText.gameObject.SetActive(false);
            }));
    }

    /// <summary>
    /// 携程等待当前信息显示完毕继续播放
    /// </summary>
    /// <param name="time"></param>
    /// <param name="call"></param>
    /// <returns></returns>
    private IEnumerator WaitTime(float time, Action call)
    {
        yield return new WaitForSeconds(time);
        if (call != null)
        {
            call();
        }
    }
}

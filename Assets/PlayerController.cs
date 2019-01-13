using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour {
    public Animator mAnimator;
    public CharacterController mCharacterController;

    private bool isMove = false;
    public Vector2 mDir = Vector2.zero;

    private float mTargetBlend;
    private float mCurrentBlend;
    public Vector2 Dir
    {
        get { return mDir; }
        //接受传递过来的方向
        set
        {
            mDir = value;
            if (mDir != Vector2.zero)
            {
                isMove = true;
            }
            else
            {
                isMove = false;
            }
        }
    }

    private Vector3 mCamOff;
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        mCamOff = transform.position - Camera.main.transform.position;
    }
    void Update()
    {
        if (mCurrentBlend != mTargetBlend)
        {
            UpdateMixBlend();
        }
        if (isMove)
        {
            //相机跟随
            SetCam();
            //设置方向
            SetDit();
            //移动
            SetMove();
        }

    }
    /// <summary>
    /// 设置玩家角色方向
    /// </summary>
    public void SetDit()
    {
        float angle = Vector2.SignedAngle(mDir, new Vector2(0, 1)) + Camera.main.transform.eulerAngles.y;
        Vector3 eulerAngles = new Vector3(0, angle, 0);
        transform.localEulerAngles = eulerAngles;
    }
    /// <summary>
    ///角色移动
    /// </summary>
    private void SetMove()
    {
        mCharacterController.Move(transform.forward * Time.deltaTime * 5);
    }
    /// <summary>
    /// 摄像机跟随
    /// </summary>
    private void SetCam()
    {
        Camera.main.transform.position = transform.position - mCamOff;
    }
    /// <summary>
    /// 主城动画切换
    /// </summary>
    /// <param name="blend"></param>
    public void SetBlend(float blend)
    {
        mTargetBlend = blend;
    }
    /// <summary>
    /// 移动平滑度
    /// </summary>
    private void UpdateMixBlend()
    {
        if (Mathf.Abs(mCurrentBlend - mTargetBlend) < 5f * Time.deltaTime)
        {
            mCurrentBlend = mTargetBlend;
        }
        else if(mCurrentBlend > mTargetBlend)
        {
            mCurrentBlend -= 5f * Time.deltaTime;
        }else
        {
            mCurrentBlend += 5f * Time.deltaTime;
        }
        mAnimator.SetFloat("Blend", mCurrentBlend);
    }
}



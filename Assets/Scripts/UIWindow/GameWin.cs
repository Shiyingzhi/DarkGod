using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class GameWin : UIWinBase {
    public Animation mAnim;
    public Image mRkPiont;
    public Image mRk;
    private bool isOpen = true;

    private Vector2 StartPos = Vector2.zero;
    private Vector3 EndPos = Vector3.zero;
    /// <summary>
    /// 初始化
    /// </summary>
    protected override void InitWin()
    {
        //加载音乐
        AudioSvc.instance.PlayBgClip(GameConstant.BgMainCity, true);
        //加载游戏主角
        MapCfg mapData = ResSvc.intance.GetMapData(GameConstant.MainCityScene);
        MainGameSys.instance.LoadPlayer(mapData);
        //隐藏摇杆按钮
        mRkPiont.gameObject.SetActive(false);
        //初始化摇杆
        ControlRocker();
    }
    /// <summary>
    /// 功能切换按钮
    /// </summary>

    public void OnExchangeButClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        if (isOpen)
        {
            isOpen = false;
            mAnim.Play("ExchangCloseAnim");
        }
        else
        {
            isOpen = true;
            mAnim.Play("ExchangOpenAnim");
        }
    }
    /// <summary>
    /// 初始化摇杆
    /// </summary>
    public void ControlRocker()
    {
        PEListener PE = mRk.gameObject.AddComponent<PEListener>();
        PE.OnClickDown = (PointerEventData eve) => 
        {
            mRkPiont.gameObject.SetActive(true);
            StartPos = mRkPiont.transform.position;
            mRkPiont.transform.position = eve.position;
        };
        PE.OnClickUp = (PointerEventData eve) =>
        {
            mRkPiont.gameObject.SetActive(false);
            mRkPiont.transform.localPosition = Vector2.zero;
            MainGameSys.instance.SetDir(Vector2.zero);
        };
        PE.OnClickDrag = (PointerEventData eve) =>
        {
            Vector2 dir = eve.position - StartPos;
            float len = dir.magnitude;
            if (len > GameConstant.RockerEange)
            {
                //将image控制在摇杆范围内
                Vector2 clampDir = Vector2.ClampMagnitude(dir, GameConstant.RockerEange); 
                mRkPiont.transform.position = StartPos + clampDir;
            }
            else
            {
                mRkPiont.transform.position = eve.position;
            }
            MainGameSys.instance.SetDir(dir.normalized);
             
        };

    }


}

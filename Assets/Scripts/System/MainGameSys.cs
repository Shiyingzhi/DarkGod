using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameSys : MonoBehaviour {
    public static MainGameSys instance;

    public GameWin mGameWin;

    private PlayerController mPlayerCont;


    /// <summary>
    /// 初始化主城系统
    /// </summary>
    public void InitMainGameSys()
    {
        instance = this;
    }
    /// <summary>
    /// 加载玩家角色
    /// </summary>
    /// <param name="mapData"></param>
    public void LoadPlayer(MapCfg mapData)
    {
        //加载游戏角色
        GameObject player = ResSvc.intance.LoadGameObjcet(GameConstant.CityPlayerName, true);
        player.transform.position = mapData.mPlayerBornPos;
        player.transform.localEulerAngles = mapData.mPlayerRote;
        player.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        //初始化摄像机
        Camera.main.transform.position = mapData.mCamPos;
        Camera.main.transform.eulerAngles = mapData.mCamRote;

        mPlayerCont = player.GetComponent<PlayerController>();
        mPlayerCont.Init();
    }
    /// <summary>
    /// 设置角色移动方向
    /// </summary>
    /// <param name="dir"></param>
    public void SetDir(Vector2 dir)
    {
        if (dir == Vector2.zero)
        {
            //设置动画停止
            mPlayerCont.SetBlend(0);
            mPlayerCont.Dir = dir;
        }
        else 
        {
            //设置动画移动
            mPlayerCont.SetBlend(1);
            mPlayerCont.Dir = dir;
        }
    }
}

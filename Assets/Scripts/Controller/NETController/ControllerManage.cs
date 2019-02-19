using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DarkGodAgreement;
using UnityEngine;

public class ControllerManage
{
    private Dictionary<ReturnSys, BaseController> ServerMassageController = new Dictionary<ReturnSys, BaseController>();

    public static ControllerManage instance;
    /// <summary>
    /// 初始化控制器
    /// </summary>
    public void InitController()
    {
        instance = this;
        ServerMassageController.Add(ReturnSys.登录成功, new LogonController());
        ServerMassageController.Add(ReturnSys.登录失败, new LogonComeNothingController());
        ServerMassageController.Add(ReturnSys.注册成功, new RegisterController());
        ServerMassageController.Add(ReturnSys.注册失败, new RegisterComeNothingController());
        ServerMassageController.Add(ReturnSys.账号已登录, new HaveLoginController());
        ServerMassageController.Add(ReturnSys.选择角色, new SelectRoleController());
        ServerMassageController.Add(ReturnSys.创建成功, new FoundTrueController());
        ServerMassageController.Add(ReturnSys.完成任务, new PerformController());
        ServerMassageController.Add(ReturnSys.等级提升, new UplvController());
        ServerMassageController.Add(ReturnSys.新玩家加入, new NewPlyaerIntoController());
        ServerMassageController.Add(ReturnSys.加载当前在线玩家, new CurrentPlayerController());
        ServerMassageController.Add(ReturnSys.主城移动同步, new PlayerCityMoveController());
        ServerMassageController.Add(ReturnSys.停止移动, new PlayerMoveStopController());
        ServerMassageController.Add(ReturnSys.强化, new IntensifyController());
        ServerMassageController.Add(ReturnSys.世界聊天, new WorldTalkController());
        ServerMassageController.Add(ReturnSys.完成每日任务, new DailyTaskController());
        ServerMassageController.Add(ReturnSys.玩家进入副本, new PlayerJoinCombartController());
    }
    /// <summary>
    /// 选择控制器
    /// </summary>
    /// <param name="sys"></param>
    /// <param name="data"></param>
    public void SelectController(ReturnSys sys,string data)
    {
        BaseController currentController = null;
        if (ServerMassageController.ContainsKey(sys))
        {
            currentController = ServerMassageController[sys];
            currentController.ProcessMessage(sys, data);
        }
        else
        {
            Debug.Log("选择的控制器不存在" + currentController);
        }
    }
}


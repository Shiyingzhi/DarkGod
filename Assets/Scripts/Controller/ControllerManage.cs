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


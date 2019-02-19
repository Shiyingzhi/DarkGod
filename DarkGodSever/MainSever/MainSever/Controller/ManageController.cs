using System;
using System.Collections.Generic;
using System.Text;
using DarkGodAgreement;
using System.Reflection;
namespace MainSever.Controller
{
    public class ManageController
    {
        private Dictionary<GameSys, BaseController> ControllerDic = new Dictionary<GameSys, BaseController>();
        public ManageController()
        {
            //初始化所有控制器
            ControllerDic.Add(GameSys.登录, new LogonController());
            ControllerDic.Add(GameSys.注册, new RegisterController());
            ControllerDic.Add(GameSys.退出游戏,new EixtController());
            ControllerDic.Add(GameSys.任务, new TaskController());
            ControllerDic.Add(GameSys.同步, new SyncController());
            ControllerDic.Add(GameSys.强化, new IntensifyController());
            ControllerDic.Add(GameSys.聊天, new TalkController());
            ControllerDic.Add(GameSys.奖励, new AwardController());
        }

        public void SelectController(Client client,GameSys sys,MethodController controller,string data)
        {
            BaseController currentController = null;
            Console.WriteLine("控制器："+sys.ToString());
            Console.WriteLine("执行方法："+controller.ToString());
            //Console.WriteLine("接受到的数据：" + data);
            if (ControllerDic.ContainsKey(sys))
            {
                currentController = ControllerDic[sys];
            }
            else
            {
                Console.WriteLine("传递过来的控制器不纯在" + sys);
            }
            MethodInfo mt = currentController.GetType().GetMethod(controller.ToString());
            if(mt ==null)
            {
                Console.WriteLine("要执行的方法不存在" + controller.ToString());
            }
            object[] go = new object[] {client,data };
            Console.WriteLine(currentController.ToString() + go.ToString());
            mt.Invoke(currentController, go);
            //object returnObj = 
            //if (returnObj ==null)
            //{
            //    Console.WriteLine("不需要返回数据");
            //}
            //else
            //{
            //    client.SendMassageSys(returnObj.ToString());
            //}
        }
    }
}

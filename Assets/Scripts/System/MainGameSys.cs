using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkGodAgreement;
using UnityEngine.AI;
public class MainGameSys : MonoBehaviour {
    public static MainGameSys instance;

    public GameWin mGameWin;
    public PlayerInfoWin mPlayerInfoWin;
    public GuideWin mGuideeWin;
    public IntensifyWin mIntensifyWin;
    public WorldTalkWin mWorldTalkWin;
    public CustomsPassWin mCustomsPassWin;
    public MonsterHp mMonsterHp;
    public TaskWin mTaskWin;
    public EvaluateWin mEvaluateWin;

    public PlayerController mPlayerCont;
    public RoleDAO mCurrentRole;
    private Transform mPlayerInfoCam;
    private Transform[] NpcTransformArry;

    private AutoGuideCfg mCurrentAutoGuideCfg;
    private NavMeshAgent mNav;

    public bool isCity = false;
    public bool PlayerAnimEventInfo = false;
    public int RoleHp;
    private float HintNum;

    public int SceneCombarId;
    public string SceneName;
    GameObject player;
    public Dictionary<int, GameObject> GamersDic = new Dictionary<int, GameObject>();
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
    public void LoadPlayer(MapCfg mapData, Profession profession)
    {
        //加载游戏角色
        player = ResSvc.intance.LoadGameObjcet(profession, true);
        player.name = "Player";
        player.transform.position = mapData.mPlayerBornPos;
        player.transform.localEulerAngles = mapData.mPlayerRote;
        player.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        mNav = player.GetComponent<NavMeshAgent>();
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
        StopNavTask();
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
    //设置当前角色
    public bool SetRole(int index)
    {
        mCurrentRole = GameRoot.instance.mRoleList[index];
        if (mCurrentRole == null)
        {
            return false;
        }
        else
        {
            NetSvc.instance.SendSys(GameSys.登录, MethodController.SetRole, index.ToString());
            return true;
        }
    }
    public void ReturnCity()
    {
        ResSvc.intance.AsyncLoadScene(GameConstant.MainGameScene, () =>
        {
            //加载游戏主角
            AudioSvc.instance.PlayBgClip(GameConstant.BgMainCity, true);
            mGameWin.HideOnOff();
            mGameWin.CityState();
            mGameWin.ShowRoleHp(false);
            MapCfg mapData = ResSvc.intance.GetMapData(GameConstant.MainCitySceneId);
            MainGameSys.instance.LoadPlayer(mapData, mCurrentRole.profession);
            mGameWin.HideOnOff();
            mWorldTalkWin.isShow();
            mWorldTalkWin.isTalk(false);
            //设置人物展示相机
            if (mPlayerInfoCam != null)
            {
                mPlayerInfoCam.gameObject.SetActive(false);
            }
            //找到NPC的位置
            NpcTransformArry = GameObject.FindGameObjectWithTag("NpcTransform").GetComponent<NpcTransform>().NpcTransformArry;
            isCity = true;
            //发送消息通知服务器上线了
            string data = string.Format("{0},{1},{2}*{3}*{4}*", GameRoot.instance.mUser.id.ToString(), ((int)mCurrentRole.profession).ToString(), player.transform.position.x.ToString(), player.transform.position.y.ToString(), player.transform.position.z.ToString());
            NetSvc.instance.SendSys(GameSys.同步, MethodController.JoinCity, data);
            Invoke("SyncLoadRole", 0.5f);
        }); 
    }
    public void InToMainCity()
    {
        //进入主城
        ResSvc.intance.AsyncLoadScene(GameConstant.MainGameScene, () =>
        {
            //加载游戏主角
            mGameWin.ShowRoleHp(false);
            MapCfg mapData = ResSvc.intance.GetMapData(GameConstant.MainCitySceneId);
            MainGameSys.instance.LoadPlayer(mapData, mCurrentRole.profession);
            mGameWin.isShow();
            mWorldTalkWin.isShow();
            mWorldTalkWin.isTalk(false);
            //设置人物展示相机
            if (mPlayerInfoCam != null)
            {
                mPlayerInfoCam.gameObject.SetActive(false);
            }
            //找到NPC的位置
            NpcTransformArry = GameObject.FindGameObjectWithTag("NpcTransform").GetComponent<NpcTransform>().NpcTransformArry;
            isCity = true;
            //发送消息通知服务器上线了
            string data = string.Format("{0},{1},{2}*{3}*{4}*", GameRoot.instance.mUser.id.ToString(), ((int)mCurrentRole.profession).ToString(), player.transform.position.x.ToString(), player.transform.position.y.ToString(), player.transform.position.z.ToString());
            NetSvc.instance.SendSys(GameSys.同步, MethodController.JoinCity, data);
            Invoke("SyncLoadRole", 0.5f);
        });
    }
    public void IntoCombat(int combat, string scenesName, int mapId)
    {
        //进入副本
        mGameWin.SkillCDInit();
        mGameWin.HideOnOff(0);
        mCustomsPassWin.isShow(false);
        RoleHp = mCurrentRole.hp;
        mGameWin.SetRoleHp(1, RoleHp);
        ResSvc.intance.AsyncLoadScene(scenesName, () =>
        {
            SceneName = scenesName;
            mGameWin.ShowRoleHp(true);
            mGameWin.HideOnOff();
            //加载游戏主角
            MapCfg mapData = ResSvc.intance.GetMapData(mapId);
            SceneCombarId = mapId;
            MainGameSys.instance.LoadPlayer(mapData, MainGameSys.instance.mCurrentRole.profession);
            mGameWin.CombatState();
            mPlayerCont.AnimCombat();
            isCity = false;
            CombatSys.instance.isCombat = true;
            //切换BGM
            AudioSvc.instance.PlayBgClip(GameConstant.忍龙BGM, true,1);
            //发送消息通知服务器进入副本了
            CombatSys.instance.IntoCombat(combat);
            string data = string.Format("{0},{1},", GameRoot.instance.mUser.id.ToString(), ((int)mCurrentRole.profession).ToString());
            NetSvc.instance.SendSys(GameSys.同步, MethodController.JoinCombart, data);
        });
    }
    
    /// <summary>
    /// 计算战斗力
    /// </summary>
    /// <returns></returns>
    public int AttackCost()
    {
        int AttackNum = mCurrentRole.lv * 1000 + mCurrentRole.strength * 50 + mCurrentRole.intelligence * 50 + mCurrentRole.physicalPower * 30 + mCurrentRole.agility * 30;
        return AttackNum;
    }
    /// <summary>
    /// 显示角色信息
    /// </summary>
    public void ShowInfo()
    {
        if (isCity == false) return;
        StopNavTask();
        //显示展示相机
        if (mPlayerInfoCam == null)
        {
            mPlayerInfoCam = GameObject.FindGameObjectWithTag("PlayerInfoCam").transform;
            DontDestroyOnLoad(mPlayerInfoCam);
        }
        mPlayerInfoCam.localPosition = mPlayerCont.transform.position + mPlayerCont.transform.forward * 3.8f + new Vector3(0, 1.2f, 0);
        mPlayerInfoCam.localEulerAngles = new Vector3(0, 180 + mPlayerCont.transform.localEulerAngles.y, 0);
        mPlayerInfoCam.gameObject.SetActive(true);

        //显示UI面板
        mPlayerInfoWin.isShow();
    }
    /// <summary>
    /// 关闭信息面板
    /// </summary>
    public void HideInfo()
    {
        mPlayerInfoCam.gameObject.SetActive(false);
    }

    private bool isGuide = false;   //是否在自动任务寻路
    /// <summary>
    /// 运行自动寻路
    /// </summary>
    /// <param name="guideeCfg"></param>
    public void RunTask(AutoGuideCfg guideeCfg)
    {
        if (guideeCfg != null)
        {
            mCurrentAutoGuideCfg = guideeCfg;
        }
        if (mCurrentAutoGuideCfg.npcID != -1)
        {
            float dis = Vector3.Distance(mPlayerCont.transform.position, NpcTransformArry[mCurrentAutoGuideCfg.npcID].position);
            if (dis < 0.5f)
            {
                isGuide = false;
                //mNav.isStopped = true;
                mPlayerCont.SetBlend(0);
                mNav.enabled = false;
                OpenSpeakWin();
            }
            else
            {
                isGuide = true;
                mNav.enabled = true;
                mNav.speed = 5;
                mNav.SetDestination(NpcTransformArry[mCurrentAutoGuideCfg.npcID].position);
                mPlayerCont.SetBlend(1);
            }

        }
        else
        {
            GameRoot.ShowHint("没有更多任务了，敬请关注后续更新");
        }
    }
    /// <summary>
    /// 打开对话框
    /// </summary>
    public void OpenSpeakWin()
    {
        mGuideeWin.isShow();
    }
    /// <summary>
    /// 停止自动任务导航
    /// </summary>
    private void StopNavTask()
    {
        if (isGuide)
        {
            isGuide = false;
            mNav.isStopped = true;
            mPlayerCont.SetBlend(0);
            mNav.enabled = false;
        }
    }
    /// <summary>
    /// 更新摄像头位置
    /// </summary>
    void LateUpdate()
    {
        if (isGuide)
        {
            float dis = Vector3.Distance(mPlayerCont.transform.position, NpcTransformArry[mCurrentAutoGuideCfg.npcID].position);
            if (dis < 0.5f)
            {
                isGuide = false;
                mNav.isStopped = true;
                mPlayerCont.SetBlend(0);
                mNav.enabled = false;
                OpenSpeakWin();
            }
            mPlayerCont.SetCam();
        }
    }

    public AutoGuideCfg GetAutoGuideCfg()
    {
        return mCurrentAutoGuideCfg;
    }

    public void UpdateIntensify(int strength, int intelligence, int physicalPower, int agility, int attackNum, int coin, int cryal, int[] equipLv,int hp)
    {
        mCurrentRole.hp = hp;
        mCurrentRole.strength = strength;
        mCurrentRole.intelligence = intelligence;
        mCurrentRole.physicalPower = physicalPower;
        mCurrentRole.agility = agility;
        mCurrentRole.attackNum = attackNum;
        mCurrentRole.coin = coin;
        mCurrentRole.crystal = cryal;
        mCurrentRole.equipLv = equipLv;
        UpdateMainCatyInfo();

    }

    public void UpdateMainCatyInfo()
    {
        mGameWin.InitProperty();
    }
    public void UpdateCombatInfo()
    {
        mGameWin.CombatStatUpdate();
    }

    public GameObject GetPlayerGameObject(int id)
    {
        GameObject player =null;
        if (!GamersDic.TryGetValue(id, out player))
        {
            Debug.Log("游戏物体id不存在"+id);
            return null;
        }
        return player;
    }


    private void SyncLoadRole()
    {
        Debug.Log("SyncLoad");
        NetSvc.instance.SendSys(GameSys.同步, MethodController.SyncLoadRole, "");
    }

    public void OnIntensifyButCilck()
    {
        mIntensifyWin.isShow();
    }
    public void OnCloseIntensifyButCilck()
    {
        mIntensifyWin.isShow(false);
    }
    public void OnCustomsPassClick()
    {
        mCustomsPassWin.isShow();
    }

    public void OnWorldTalkButClick()
    {
        mWorldTalkWin.isTalk();
    }

    public void UpdateWorldTalk(string talk)
    {
        mWorldTalkWin.UpdateWorldTalk(talk);
    }

    public void AttackBut(bool isAttack)
    {
        mPlayerCont.Attack(isAttack);
    }
    public void Attack2But()
    {
        mPlayerCont.Attack2();
    }
    public void Attack3But()
    {
        mPlayerCont.Attack3();
    }
    public void Attack4But()
    {
        mPlayerCont.Attack4();
    }
    public void ResetAttack()
    {
        mPlayerCont.ResetAttack();
    }
    public void Skill1()
    {
        mPlayerCont.Skill1();
    }

    public void Skill2()
    {
        mPlayerCont.Skill2();
    }

    public void Skill3()
    {
        mPlayerCont.Skill3();
    }

    public void PlayerHurt(int damage)
    {
        if (mPlayerCont.Skill_2 || mPlayerCont.Skill_3) return;
        mPlayerCont.mAnimator.SetTrigger("Hit");
        RoleHp -= damage;
        mGameWin.SetRoleHp(((float)RoleHp/(float)(mCurrentRole.hp)),RoleHp);
        AudioSvc.instance.PlayRoleClip(GameConstant.AssassinHit, 1);

    }
    public void SetHpUI(string iconStr, string name, float val)
    {
        mMonsterHp.UpdateHp(iconStr,name, val);
    }
    public void HpUIisShow(bool isShow)
    {
        mMonsterHp.isShow(isShow);
    }

    public void OnTaskClick()
    {
        mTaskWin.isShow();
    }

    public void SetCombastTask(bool isTrue)
    {
        mGuideeWin.CombastTask = isTrue;
    }

    public void SetIntensifyTask(bool isTrue)
    {
        mGuideeWin.IntensifyTask = isTrue;
    }

    public void UpdateTaskWin()
    {
        mTaskWin.TaskInit();
    }
    public void ShowEvaluateWin()
    {
        mEvaluateWin.isShow();
    }
    public void TheCombatUpLvevel()
    {
        RoleHp = mCurrentRole.hp;
        mGameWin.SetRoleHp(1, RoleHp);
    }

}

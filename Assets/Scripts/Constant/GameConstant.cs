using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameConstant {
    //场景
    public const string LogonScene = "SampleScene";
    public const string MainGameScene = "SceneMainCity";
    public const string CombatScene = "SceneCombat";
    //场景id
    public const int MainCitySceneId = 10000;
    public const int CombatSceneId = 10001;

    //副本id
    public const int Combat1 = 20001;

    //主城玩家模型名字
    public const string CityPlayerName = "PrefabPlayer/Assassin";

    //怪物模型
    public const string SoldierMon = "PrefabNPC/MonsterSoldier_1";
    public const string BoosMon = "PrefabNPC/MonsterBoss_1";

    //动画控制器
    public const string AnimCityCon = "ResAnimator/AssassinCity";
    public const string AnimCombatCon = "ResAnimator/AssassinCombat";

    //BG音效
    public const string BgLogon = "bgLogin";
    public const string BgMainCity = "bgMainCity";
    public const string BgCombat = "bgHuangYe";
    public const string 忍龙BGM = "CombartBg";
    //进入游戏按钮音效
    public const string ButIntoGame = "uiLoginBtn";

    //常规按钮点击音效
    public const string ButOnClick = "uiClickBtn";
    public const string RoleUp = "Up";

    //刺客音效
    public const string ButAttack1 = "Attack1";
    public const string ButAttack2 = "Attack2";
    public const string ButAttack3 = "Attack3";
    public const string ButAttack4 = "Attack4";

    public const string AssassinHit = "assassin_Hit";

    public const string ButSkill1 = "Skill1";
    public const string ButSkill2 = "Skill2";
    public const string ButSkill3 = "Skill3";
    public const string ButSkill3_2 = "Skill3_2";
    //技能特效
    public const string Attack1Eff = "Attack1";
    public const string Attack2Eff = "Attack2";
    public const string Attack3Eff = "Attack3";
    public const string Attack4Eff = "Attack4";

    public const string Skill1Eff = "Skill1";
    public const string Skill2_1Eff = "Skill2_1";
    public const string Skill2_2Eff = "Skill2_2";
    public const string Skill3_1Eff = "Skill3_1";
    public const string Skill3_2Eff = "Skill3_2";
    //玩家随机名字Xml文件路径
    public const string XmlRangeName = "ResCfg/rdname";
    //主城地图Xml路径
    public const string XmlMap = "ResCfg/map";
    //任务引导Xml路径
    public const string XmlAutoGuide = "ResCfg/guide";
    //强化配置Xml路径
    public const string XmlStrongCfg = "ResCfg/strong";
    //摇杆范围
    public const int RockerEange = 60;

    //角色背景
    public const string AssassinBg = "ResImages/assassin";
    //NPC背景
    public const string Wiseman = "ResImages/npc0";
    public const string General = "ResImages/npc1";
    public const string Artisan = "ResImages/npc2";
    public const string Trader = "ResImages/npc3";
    public const string Guide = "ResImages/npcguide";
    //角色头像
    public const string AssassinHead = "ResImages/headAssassin";

    //装备Icon
    public const string HeadIcon = "ResImages/toukui";
    public const string BodyIcon = "ResImages/body";
    public const string WaistIcon = "ResImages/yaobu";
    public const string HandIcon = "ResImages/hand";
    public const string LegIcon = "ResImages/leg";
    public const string FootIcon = "ResImages/foot";

    //装备星级
    public const string StarTrue = "ResImages/star2";
    public const string StarFales = "ResImages/star1";

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemy
{
    Soldier,
    Boss
}
public class CombatSys : MonoBehaviour {

    public static CombatSys instance;
    public bool isCombat = false;

    public int CombarNum;

    public int BeHitNum;
    public float WinTimer;

    private bool EventInit = false;
    public Dictionary<GameObject, MonsterController> MonsterDic = new Dictionary<GameObject, MonsterController>();
    public Dictionary<int, GameObject> TriggerMonsterDic = new Dictionary<int, GameObject>();

    /// <summary>
    /// 初始化战斗系统
    /// </summary> 
    public void InitCombatSys()
    {
        instance = this;
    }


    public void Attackmonster(int damage,GameObject monster)
    {
        if (MonsterDic.ContainsKey(monster))
        {
            MonsterDic[monster].Hurt(damage);
        }
        
    }

    void Update()
    {
        if(isCombat)
        {
            WinTimer += Time.deltaTime;
        }
        
    }
    public void IntoCombat(int combat)
    {
        switch (combat)
        {
            //进入一号副本
            case GameConstant.Combat1:
                CombarNum = combat;
                Transform[] point = GameObject.Find("EnemyPoint").GetComponentsInChildren<Transform>();
                LoadMonsder(Enemy.Soldier, point[1],1);
                LoadMonsder(Enemy.Soldier,point[1],2);
                LoadMonsder(Enemy.Soldier, point[1],3);
                LoadMonsder(Enemy.Soldier, point[2],4);
                LoadMonsder(Enemy.Soldier, point[2],5);
                LoadMonsder(Enemy.Soldier, point[3],6);
                LoadMonsder(Enemy.Soldier, point[3],7);
                LoadMonsder(Enemy.Boss, point[4],8);
                if (EventInit==false)
                {
                    //战士动画添加帧事件
                    MonsterDic[TriggerMonsterDic[1].gameObject].AddAnimationEvent();
                    //Boss动画添加帧事件
                    MonsterDic[TriggerMonsterDic[8].gameObject].AddAnimationEvent();
                    EventInit = true;
                }
                break;
        }
    }
    
    public void LoadMonsder(Enemy enemy,Transform point,int Num)
    {

        switch (enemy)
        {
            case Enemy.Soldier:
                GameObject Solider = ResSvc.intance.LoadGameObjcet(enemy, true);
                Solider.AddComponent<SoldierDAO>();
                MonSoldierController SoldierController = Solider.AddComponent<MonSoldierController>();
                SoldierController.mAnimator = Solider.GetComponent<Animator>();
                SoldierController.mAudioSource = SoldierController.GetComponent<AudioSource>();
                SoldierController.mPoint = SoldierController.transform.Find("AttackPoint").transform;
                Solider.transform.parent = point;
                Solider.transform.localPosition = new Vector3(0, 0, 0);
                Solider.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                Solider.transform.localPosition = new Vector3(Random.Range(0, 6), 0, Random.Range(0, 6));
                MonsterDic.Add(Solider, SoldierController);
                Solider.gameObject.SetActive(false);
                TriggerMonsterDic.Add(Num, Solider);
                break;
            case Enemy.Boss:
                GameObject Boss = ResSvc.intance.LoadGameObjcet(enemy, true);
                Boss.AddComponent<Boss1DAO>();
                BossController BossController = Boss.AddComponent<BossController>();
                BossController.mAnimator = BossController.GetComponent<Animator>();
                BossController.mAudioSource = BossController.GetComponent<AudioSource>();
                BossController.mPoint = BossController.transform.Find("AttackPoint").transform;
                Boss.transform.parent = point;
                Boss.transform.localPosition = Vector3.zero;
                Boss.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                MonsterDic.Add(Boss, BossController);
                Boss.gameObject.SetActive(false);
                TriggerMonsterDic.Add(Num, Boss);
                break;
        }
    }

    public void Skill1()
    {
        
    }

    public void Skill2()
    {
        
    }

    public void Skill3()
    {
        
    }
    #region 一号副本触发器
    public void Combat1Trigger1()
    {
        TriggerMonsterDic[1].gameObject.SetActive(true);
        TriggerMonsterDic[2].gameObject.SetActive(true);
        TriggerMonsterDic[3].gameObject.SetActive(true);
        MonsterDic[TriggerMonsterDic[1].gameObject].Born();
        MonsterDic[TriggerMonsterDic[2].gameObject].Born();
        MonsterDic[TriggerMonsterDic[3].gameObject].Born();
    }

    public void Combat1Trigger2()
    {
        TriggerMonsterDic[4].gameObject.SetActive(true);
        TriggerMonsterDic[5].gameObject.SetActive(true);
        MonsterDic[TriggerMonsterDic[4].gameObject].Born();
        MonsterDic[TriggerMonsterDic[5].gameObject].Born();
    }
    public void Combat1Trigger3()
    {
        TriggerMonsterDic[6].gameObject.SetActive(true);
        TriggerMonsterDic[7].gameObject.SetActive(true);
        MonsterDic[TriggerMonsterDic[6].gameObject].Born();
        MonsterDic[TriggerMonsterDic[7].gameObject].Born();
    }
    public void Combat1Trigger4()
    {
        TriggerMonsterDic[8].gameObject.SetActive(true);
        MonsterDic[TriggerMonsterDic[8].gameObject].Born();
    }
    #endregion

    public void PlayerHurt(int damage)
    {
        BeHitNum++;
        MainGameSys.instance.PlayerHurt(damage);
    }

    public void CloseCombar()
    {
        MonsterDic.Clear();
        TriggerMonsterDic.Clear();
    }
}

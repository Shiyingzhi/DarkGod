using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkGodAgreement;
public class PlayerController : MonoBehaviour {
    public Animator mAnimator;
    public CharacterController mCharacterController;
    public Transform mAttackPotion;

    private bool isMove = false;
    public Vector2 mDir = Vector2.zero;
    private float Speed = 5;

    private float mTargetBlend;
    private float mCurrentBlend;

    private ParticleSystem[] EffSys;
    string Movedata;

    public bool Skill_2 = false;
    public bool Skill_3 = false;
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
        //移动同步
        InvokeRepeating("SyncMove", 0, 0.033f);   
    }
    void FixedUpdate()
    {
        if (!mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.CityTree"))
        {
            if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.dagger_attack_1") ||
                mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.dagger_attack_5") ||
                mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.dagger_attack_3") ||
                mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.dagger_attack_4"))
            {
                Speed = 0.5f;
            }
            if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.dagger_attack_2") ||
                mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.dagger_skill_1")||
                mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.dagger_hit"))
            {
                Speed = 0;
            }
            if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.dagger_skill_2"))
            {
                Speed = 1;
            }
        }
        else
        {
            Speed = 5;
        }
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
        if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.dagger_attack_2") ||
            mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.dagger_skill_1")||
            mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.dagger_hit")) return;
        float angle = Vector2.SignedAngle(mDir, new Vector2(0, 1)) + Camera.main.transform.eulerAngles.y;
        Vector3 eulerAngles = new Vector3(0, angle, 0);
        transform.localEulerAngles = eulerAngles;
    }
    /// <summary>
    ///角色移动
    /// </summary>
    private void SetMove()
    {
        mCharacterController.Move(transform.forward * Time.deltaTime * Speed);
    }
    /// <summary>
    /// 摄像机跟随
    /// </summary>
    public void SetCam()
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

    private void SyncMove()
    {
        if (isMove && MainGameSys.instance.isCity)
        {
            Movedata = string.Format("{0}*{1}*{2}*{3}*{4}*{5}*", transform.position.x.ToString(), transform.position.y.ToString(), transform.position.z.ToString(),
            transform.eulerAngles.x.ToString(), transform.eulerAngles.y.ToString(), transform.eulerAngles.z.ToString());
            NetSvc.instance.SendSys(GameSys.同步, MethodController.SyncMove, Movedata);
        }
        else if (MainGameSys.instance.isCity)
        {
            NetSvc.instance.SendSys(GameSys.同步, MethodController.SyncMoveStop, "");
        }
        
    }
    
    private void AddAnimationEvent()
    {
        //获取动画组件中所有动画
        AnimationClip[] clips = mAnimator.runtimeAnimatorController.animationClips;
        for (int i = 0; i < clips.Length; i++)
        {
            //根据动画名字  找到你要添加的动画
            if (string.Equals(clips[i].name, "dagger_attack_1"))
            {
                //添加动画事件
                AnimationEvent events = new AnimationEvent();
 
                //添加第一个事件  带参数
                events.functionName = "dagger_attack_1";
                events.time = 0.02f;
                //events.stringParameter = "参数0";
                clips[i].AddEvent(events);
            }
            if (string.Equals(clips[i].name, "dagger_attack_5"))
            {
                //添加动画事件
                AnimationEvent events = new AnimationEvent();

                //添加第一个事件  带参数
                events.functionName = "dagger_attack_5";
                events.time = 0.02f;
                //events.stringParameter = "参数0";
                clips[i].AddEvent(events);
            }
            if (string.Equals(clips[i].name, "dagger_attack_3"))
            {
                //添加动画事件
                AnimationEvent events = new AnimationEvent();

                //添加第一个事件  带参数
                events.functionName = "dagger_attack_3";
                events.time = 0.02f;
                //events.stringParameter = "参数0";
                clips[i].AddEvent(events);
            }
            if (string.Equals(clips[i].name, "dagger_attack_4"))
            {
                //添加动画事件
                AnimationEvent events = new AnimationEvent();

                //添加第一个事件  带参数
                events.functionName = "dagger_attack_4";
                events.time = 0.02f;
                //events.stringParameter = "参数0";
                clips[i].AddEvent(events);
            }
            if (string.Equals(clips[i].name, "dagger_attack_2"))
            {
                //添加动画事件
                AnimationEvent events = new AnimationEvent();

                //添加第一个事件  带参数
                events.functionName = "dagger_attack_2";
                events.time = 0.02f;
                //events.stringParameter = "参数0";
                clips[i].AddEvent(events);
            }
            if (string.Equals(clips[i].name, "dagger_skill_1"))
            {
                //添加动画事件
                AnimationEvent events = new AnimationEvent();

                //添加第一个事件  带参数
                events.functionName = "dagger_skill_1";
                events.time = 0.02f;
                //events.stringParameter = "参数0";
                clips[i].AddEvent(events);

                events.functionName = "dagger_skill_1";
                events.time = 0.14f;
                //events.stringParameter = "参数0";
                clips[i].AddEvent(events);

                events.functionName = "dagger_skill_1";
                events.time = 0.19f;
                //events.stringParameter = "参数0";
                clips[i].AddEvent(events);

                events.functionName = "dagger_skill_1";
                events.time = 0.28f;
                //events.stringParameter = "参数0";
                clips[i].AddEvent(events);

                events.functionName = "dagger_skill_1";
                events.time = 0.34f;
                //events.stringParameter = "参数0";
                clips[i].AddEvent(events);

                events.functionName = "dagger_skill_1";
                events.time = 0.42f;
                //events.stringParameter = "参数0";
                clips[i].AddEvent(events);

                events.functionName = "dagger_skill_1_2";
                events.time = 1.03f;
                //events.stringParameter = "参数0";
                clips[i].AddEvent(events);

                
            }
            if (string.Equals(clips[i].name, "dagger_skill_2"))
            {
                //添加动画事件
                AnimationEvent events = new AnimationEvent();

                //添加第一个事件  带参数
                events.functionName = "dagger_skill_2";
                events.time = 0.02f;
                //events.stringParameter = "参数0";
                clips[i].AddEvent(events);

                events.functionName = "dagger_skill_2";
                events.time = 0.2f;
                //events.stringParameter = "参数0";
                clips[i].AddEvent(events);

                events.functionName = "dagger_skill_2";
                events.time = 0.4f;
                //events.stringParameter = "参数0";
                clips[i].AddEvent(events);

                events.functionName = "dagger_skill_2";
                events.time = 0.6f;
                //events.stringParameter = "参数0";
                clips[i].AddEvent(events);

                events.functionName = "dagger_skill_2";
                events.time = 0.6f;
                //events.stringParameter = "参数0";
                clips[i].AddEvent(events);

                events.functionName = "dagger_skill_2_2";
                events.time = 1.06f;
                //events.stringParameter = "参数0";
                clips[i].AddEvent(events);
            }
        }
        mAnimator.Rebind();
    }
    #region 帧动画事件
    public void dagger_attack_1()
    {
        EffSys[0].Play();
        AttackRangeDamage(mAttackPotion.position, 1,((MainGameSys.instance.mCurrentRole.strength + MainGameSys.instance.mCurrentRole.intelligence) * 2));
        AudioSvc.instance.PlayButClip(GameConstant.ButAttack1);
    }
    private void AttackRangeDamage(Vector3 potion, float dis,int damage)
    {
        Collider[] cols = Physics.OverlapSphere(potion, dis, LayerMask.NameToLayer("layername"));
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].tag == "Enemy")
            {
                Debug.Log(damage);
                CombatSys.instance.Attackmonster(damage, cols[i].gameObject);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 1, 0), 2);
    }
    public void dagger_attack_5()
    {
        EffSys[1].Play();
        AttackRangeDamage(mAttackPotion.position, 1, ((MainGameSys.instance.mCurrentRole.strength + MainGameSys.instance.mCurrentRole.intelligence) * 2));
        AudioSvc.instance.PlayRoleClip(GameConstant.ButAttack2);
    }
    public void dagger_attack_3()
    {
        EffSys[2].Play();
        AttackRangeDamage(mAttackPotion.position, 1, ((MainGameSys.instance.mCurrentRole.strength + MainGameSys.instance.mCurrentRole.intelligence) * 2));
        AudioSvc.instance.PlayRoleClip(GameConstant.ButAttack3);
    }
    public void dagger_attack_4()
    {
        EffSys[3].Play();
        AttackRangeDamage(mAttackPotion.position, 1, ((MainGameSys.instance.mCurrentRole.strength + MainGameSys.instance.mCurrentRole.intelligence) * 2));
        AudioSvc.instance.PlayRoleClip(GameConstant.ButAttack4);
    }
    public void dagger_attack_2()
    {
        EffSys[4].Play();
        AttackRangeDamage(mAttackPotion.position, 1, (int)((MainGameSys.instance.mCurrentRole.strength + MainGameSys.instance.mCurrentRole.intelligence) * 2 * 2.5f));
        AudioSvc.instance.PlayRoleClip(GameConstant.ButSkill1, 0.5f);
    }
    public void dagger_skill_1()
    {
        if (Skill_2 == false)
        {
            Skill_2 = true;
            EffSys[5].Play();
            EffSys[6].Play();
            AudioSvc.instance.PlayRoleClip(GameConstant.ButSkill2, 0.5f);
        }
        else
        {
            AttackRangeDamage(mAttackPotion.position, 3, (int)((MainGameSys.instance.mCurrentRole.strength + MainGameSys.instance.mCurrentRole.intelligence) * 2 * 1.3f));
        }       
    }

    public void dagger_skill_1_2()
    {
        Skill_2 = false;
        AttackRangeDamage(mAttackPotion.position, 3, (int)((MainGameSys.instance.mCurrentRole.strength + MainGameSys.instance.mCurrentRole.intelligence) * 2 * 2f));        
    }

    public void dagger_skill_2()
    {
        if (Skill_3 == false)
        {
            EffSys[7].Play();
            EffSys[8].Play();
            AudioSvc.instance.PlayRoleClip(GameConstant.ButSkill3, 0.5f);
            Skill_3 = true;

        }
        else
        {
            AttackRangeDamage(transform.position + new Vector3(0, 1, 0), 3, (int)((MainGameSys.instance.mCurrentRole.strength + MainGameSys.instance.mCurrentRole.intelligence) * 2 * 1.5f));
        }                    
    }
    public void dagger_skill_2_2()
    {
        Skill_3 = false;
        AttackRangeDamage(transform.position + new Vector3(0, 1, 0), 3, (int)((MainGameSys.instance.mCurrentRole.strength + MainGameSys.instance.mCurrentRole.intelligence) * 2 * 2f));
        AudioSvc.instance.PlayRoleClip(GameConstant.ButSkill3_2, 0.5f);
    }
    #endregion
    public void AnimCombat()
    {
        mAnimator.runtimeAnimatorController = ResSvc.intance.LoadAnimatorController(GameConstant.AnimCombatCon);
        Debug.Log("切换状态机控制器");
        EffSys = gameObject.transform.GetComponentsInChildren<ParticleSystem>();
        if (MainGameSys.instance.PlayerAnimEventInfo == false)
        {
            AddAnimationEvent();
            MainGameSys.instance.PlayerAnimEventInfo = true;
        }
        
    }

    public void Attack(bool isAttack)
    {
        mAnimator.SetBool("Attack1",isAttack);
    }
    public void Attack2() 
    {
        mAnimator.SetBool("Attack2",true);
    }
    public void Attack3()
    {        
        mAnimator.SetBool("Attack3",true);
    }
    public void Attack4()
    {
        mAnimator.SetBool("Attack4",true);
    }
    public void ResetAttack()
    {
        mAnimator.SetBool("Attack1", false);
        mAnimator.SetBool("Attack2", false);
        mAnimator.SetBool("Attack3", false);
        mAnimator.SetBool("Attack4", false);
    }

    public void Skill1()
    {
        mAnimator.SetTrigger("Skill1");
    }

    public void Skill2()
    {
        mAnimator.SetTrigger("Skill2");
    }

    public void Skill3()
    {
        mAnimator.SetTrigger("Skill3");
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("触发碰撞");
        switch (CombatSys.instance.CombarNum)
        {
            //启用一号副本触发器
            case GameConstant.Combat1:
                switch (collider.name)
                {
                    case "CubeTigger1":
                        CombatSys.instance.Combat1Trigger1();
                        break;
                    case "CubeTigger2":
                        CombatSys.instance.Combat1Trigger2();
                        break;
                    case "CubeTigger3":
                        CombatSys.instance.Combat1Trigger3();
                        break;
                    case "CubeTigger4":
                        CombatSys.instance.Combat1Trigger4();
                        break;
                }
                Destroy(collider.gameObject);
                break;
        }
        
    }
}



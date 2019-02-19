using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkGodAgreement;

public class BossController : MonsterController {

    public override void Attack()
    {
        mAnimator.SetTrigger("Attack");
    }
    public override void Hurt(int damage)
    {
        int CostDamage = damage - MonsderDAO.PhylacticPower;
        MonsderDAO.CurrentHp = MonsderDAO.CurrentHp - CostDamage;
        Debug.Log(MonsderDAO.CurrentHp);
        if (MonsderDAO.CurrentHp <= 0)
        {
            if (isDie == true) return;
            isDie = true;
            MainGameSys.instance.HpUIisShow(false);
            mAnimator.SetTrigger("Die");
            AudioClip clip = ResSvc.intance.LoadCiip("SoldierDie", true);
            mAudioSource.clip = clip;
            mAudioSource.Play();
        }
        else
        {
            MainGameSys.instance.HpUIisShow(true);
            MainGameSys.instance.SetHpUI("headBoss", "兽族巨人", ((float)MonsderDAO.CurrentHp / (float)MonsderDAO.MaxHp));

        }
        AudioSvc.instance.PlayElseClip("AssWeapon", 0.4f);

    }

    public override void Born()
    {
        Enemy = Enemy.Boss;
        MonsderDAO = GetComponent<Boss1DAO>();
        MonsderDAO.CurrentHp = 30000;
        MonsderDAO.MaxHp = 30000;
        MonsderDAO.AttackNum = 700;
        MonsderDAO.PhylacticPower = 800;
        mAnimator.SetTrigger("Born");
        AudioClip clip = ResSvc.intance.LoadCiip("BossBorn", true);
        mAudioSource.clip = clip;
        mAudioSource.Play();
    }
    public override void Chase()
    {
        mAnimator.SetFloat("Blend", 1);
    }
    public override void AddAnimationEvent()
    {
        //获取动画组件中所有动画
        AnimationClip[] clips = mAnimator.runtimeAnimatorController.animationClips;
        for (int i = 0; i < clips.Length; i++)
        {
            //根据动画名字  找到你要添加的动画
            if (string.Equals(clips[i].name, "7_skill"))
            {
                //添加动画事件
                AnimationEvent events = new AnimationEvent();

                //添加第一个事件  带参数
                events.functionName = "Boss1Attack";
                events.time = 0.5f;
                //events.stringParameter = "参数0";
                clips[i].AddEvent(events);
            }

            if (string.Equals(clips[i].name, "7_die"))
            {
                //添加动画事件
                AnimationEvent events = new AnimationEvent();

                //添加第一个事件  带参数
                events.functionName = "BossDie";
                events.time = 1.8f;
                //events.stringParameter = "参数0";
                clips[i].AddEvent(events);
            }


        }
        mAnimator.Rebind();
    }
    public void Boss1Attack()
    {
        AudioClip clip = ResSvc.intance.LoadCiip("BossAttack", true);
        mAudioSource.clip = clip;
        mAudioSource.Play();
        AttackRangeDamage(mPoint.position, 2, 0);
    }

    public void BossDie()
    {
        if (MainGameSys.instance.mCurrentRole.killBossNum < 3)
        {
            MainGameSys.instance.mCurrentRole.killBossNum++;
            string Senddata = string.Format("{0}-{1}-{2}-{3}-{4}", MainGameSys.instance.mCurrentRole.combartNum.ToString()
                , MainGameSys.instance.mCurrentRole.intensifyNum.ToString()
                , MainGameSys.instance.mCurrentRole.killMOBSNum.ToString()
                , MainGameSys.instance.mCurrentRole.worldTalkNum.ToString()
                , MainGameSys.instance.mCurrentRole.killBossNum.ToString());
            NetSvc.instance.SendSys(GameSys.任务, MethodController.DailyTask, Senddata);
        }
        string SendData = string.Format("{0}*{1}*{2}", "500", "300", "0");
        NetSvc.instance.SendSys(GameSys.奖励, MethodController.GetAward, SendData);
        MainGameSys.instance.ShowEvaluateWin();
        Destroy(this.gameObject);
    }

}

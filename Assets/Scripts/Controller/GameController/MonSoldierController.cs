using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkGodAgreement;
public class MonSoldierController : MonsterController {
    
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
            MainGameSys.instance.HpUIisShow(false);
            if (isDie == true) return;
            isDie = true;
            mAnimator.SetTrigger("Die");
            AudioClip clip = ResSvc.intance.LoadCiip("SoldierDie", true);
            mAudioSource.clip = clip;
            mAudioSource.Play();
        }
        else
        { 
            
            MainGameSys.instance.HpUIisShow(true);
            MainGameSys.instance.SetHpUI("task", "兽人", ((float)MonsderDAO.CurrentHp / (float)MonsderDAO.MaxHp));
            mAnimator.SetTrigger("Hurt");
            AudioClip clip = ResSvc.intance.LoadCiip("MonHurt", true);
            mAudioSource.clip = clip;
            mAudioSource.Play();
            AudioClip clip2 = ResSvc.intance.LoadCiip("AssWeapon", true);
        }
        AudioSvc.instance.PlayElseClip("AssWeapon", 0.4f);
    }

    public override void Born()
    {
        Enemy = Enemy.Soldier;
        MonsderDAO = GetComponent<SoldierDAO>();
        MonsderDAO.CurrentHp = 15000;
        MonsderDAO.MaxHp = 15000;
        MonsderDAO.AttackNum = 300;
        MonsderDAO.PhylacticPower = 500;
        mAnimator.SetTrigger("Born");
        AudioClip clip = ResSvc.intance.LoadCiip("SoldierBorn", true);
        mAudioSource.clip = clip;
        mAudioSource.volume = 0.5f;
        mAudioSource.Play();
    }
    public override void Chase()
    {
        mAnimator.SetFloat("Blend", 1);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(mPoint.position, 1);
    }
    public override void AddAnimationEvent()
    {
        //获取动画组件中所有动画
        AnimationClip[] clips = mAnimator.runtimeAnimatorController.animationClips;
        for (int i = 0; i < clips.Length; i++)
        {
            //根据动画名字  找到你要添加的动画
            if (string.Equals(clips[i].name, "10_attack"))
            {
                //添加动画事件
                AnimationEvent events = new AnimationEvent();

                //添加第一个事件  带参数
                events.functionName = "SoldierAttack";
                events.time = 0.5f;
                //events.stringParameter = "参数0";
                clips[i].AddEvent(events);
            }

            if (string.Equals(clips[i].name, "10_die"))
            {
                //添加动画事件
                AnimationEvent events = new AnimationEvent();

                //添加第一个事件  带参数
                events.functionName = "SoldierDie";
                events.time = 1.1f;
                //events.stringParameter = "参数0";
                clips[i].AddEvent(events);
            }
            

        }
        mAnimator.Rebind();
    }
    public void SoldierAttack()
    {
        AudioClip clip = ResSvc.intance.LoadCiip("SoldierAttack", true);
        mAudioSource.clip = clip;
        mAudioSource.volume = 1;
        mAudioSource.Play();
        AttackRangeDamage(mPoint.position, 1, 0);
    }

    public void SoldierDie()
    {
        if (MainGameSys.instance.mCurrentRole.killMOBSNum < 15)
        {
            MainGameSys.instance.mCurrentRole.killMOBSNum++;
            string Senddata = string.Format("{0}-{1}-{2}-{3}-{4}", MainGameSys.instance.mCurrentRole.combartNum.ToString()
                , MainGameSys.instance.mCurrentRole.intensifyNum.ToString()
                , MainGameSys.instance.mCurrentRole.killMOBSNum.ToString()
                , MainGameSys.instance.mCurrentRole.worldTalkNum.ToString()
                , MainGameSys.instance.mCurrentRole.killBossNum.ToString());
            NetSvc.instance.SendSys(GameSys.任务, MethodController.DailyTask, Senddata);
        }
        string SendData = string.Format("{0}*{1}*{2}", "200", "150", "0");
        NetSvc.instance.SendSys(GameSys.奖励, MethodController.GetAward, SendData);
        Destroy(this.gameObject);
    }
}

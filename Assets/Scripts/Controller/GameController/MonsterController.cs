using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MonsterController : MonoBehaviour {
    public Animator mAnimator;
    public AudioSource mAudioSource;
    public Transform mPoint;
    public bool isMove = true;
    public bool isDie = false;
    public Enemy Enemy;

    protected FSMSystem Fsm;
    protected MonsderDAO MonsderDAO;

    void Start()
    {
        Fsm = new FSMSystem();
        ChaseState Chase = new ChaseState(Fsm);
        Chase.AddTransition(Transition.AttackPlayer, StateID.Attack);
        Fsm.AddStateID(Chase);
        AttackState Attack = new AttackState(Fsm);
        Attack.AddTransition(Transition.NoChasePlayer, StateID.Chase);
        Fsm.AddStateID(Attack);
    }
    void Update()
    {
        Fsm.UpdateFsm(this);   
    }
    public virtual void Hurt(int damage) {  }

    public virtual void Attack() { }
    public virtual void Chase() { }

    public virtual void Die() { }

    public virtual void Born(){}
    /// <summary>
    /// 添加动画帧事件
    /// </summary>
    public virtual void AddAnimationEvent() { }
    public void AttackRangeDamage(Vector3 potion, float dis, int damage)
    {
        Collider[] cols = Physics.OverlapSphere(potion, dis, LayerMask.NameToLayer("layername"));
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].tag == "Player")
            {
                Debug.Log("攻击成功");
                Debug.Log(MonsderDAO.AttackNum);
                CombatSys.instance.PlayerHurt(MonsderDAO.AttackNum); 
            }
        }
    }

    public bool IsMove()
    {
        if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Born")||
            mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit")||
            mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack")||
            mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            isMove = false;
        }
        else
        {
            isMove = true;
        }
        return isMove;
    }


}

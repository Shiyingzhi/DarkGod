using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : FSMState {
    private Transform mTransform;
    public AttackState(FSMSystem fsm):base(fsm)
    {
        stateId = StateID.Attack;
        mTransform = GameObject.Find("Player").GetComponent<Transform>();
    }
    public override void Act(MonsterController npc)
    {
        if (npc.isDie) return;
        npc.Attack();
    }

    public override void Reason(MonsterController npc)
    {
        if (npc.isDie) return;
        switch (npc.Enemy)
        {
            case Enemy.Soldier:
                if (Vector3.Distance(mTransform.position, npc.transform.position) > 1)
                {
                    fsm.PerformTransition(Transition.NoChasePlayer);
                }
                break;
            case Enemy.Boss:
                if (Vector3.Distance(mTransform.position, npc.transform.position) > 3)
                {
                    fsm.PerformTransition(Transition.NoChasePlayer);
                }
                break;
        }
        
    }
}

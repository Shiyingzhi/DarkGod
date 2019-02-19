using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : FSMState {
    private Transform mTransform;
    public ChaseState(FSMSystem fsm)
        : base(fsm)
    {
        stateId = StateID.Chase;
        mTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    public override void Act(MonsterController npc)
    {
        if (npc.IsMove())
        {
            npc.transform.LookAt(mTransform.position);
            npc.transform.Translate(Vector3.forward * 3 * Time.deltaTime);
            npc.Chase();
        }
    }

    public override void Reason(MonsterController npc)
    {
        if (npc.isDie) return;
        switch (npc.Enemy)
        {
            case Enemy.Soldier:
                if (Vector3.Distance(mTransform.position, npc.transform.position) < 1)
                {
                    fsm.PerformTransition(Transition.AttackPlayer);
                }
                break;
            case Enemy.Boss:
                if (Vector3.Distance(mTransform.position, npc.transform.position) < 2)
                {
                    fsm.PerformTransition(Transition.AttackPlayer);
                }
                break;
        }
        
    }
}

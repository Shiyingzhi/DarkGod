using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateID
{
    Null,
    Idle,
    Chase,
    Attack
}
public enum Transition
{
    Null,
    AttackPlayer,
    NoChasePlayer
}
public abstract class FSMState 
{

    protected StateID stateId;
    protected FSMSystem fsm;
    public FSMState(FSMSystem fsm)
    {
        this.fsm = fsm;
    }

    public StateID ID
    {
        get { return stateId; }
    }
    private Dictionary<Transition, StateID> StateDic = new Dictionary<Transition, StateID>();

    /// <summary>
    /// 添加转换条件
    /// </summary>
    public void AddTransition(Transition transi, StateID id)
    {
        if (transi == Transition.Null)
        {
            Debug.LogWarning("添加的Transition不能为null");
            return;
        }
        if (id == StateID.Null)
        {
            Debug.LogWarning("添加的StateID不能为null");
            return;
        }
        if (StateDic.ContainsKey(transi))
        {
            Debug.LogWarning("添加的Transition已经存在");
            return;
        }
        StateDic.Add(transi, id);
    }
    /// <summary>
    /// 移除
    /// </summary>
    public void DletelTransition(Transition trans)
    {
        if (trans == Transition.Null)
        {
            Debug.LogWarning("您要移除的transition不能为null");
            return;
        }
        if (StateDic.ContainsKey(trans))
        {
            StateDic.Remove(trans);
        }
        else
        {
            Debug.LogWarning("您好移除的transition不存在" + trans);
        }
    }
    /// <summary>
    /// 获得转换条件对应的StateID
    /// </summary>
    public StateID GetStateID(Transition transi)
    {
        if (StateDic.ContainsKey(transi))
        {
            return StateDic[transi];
        }
        else
        {
            return StateID.Null;
        }
    }
    /// <summary>
    /// 进去当前状态前
    /// </summary>
    public virtual void DoBeforeEntering() { }
    /// <summary>
    /// 离开当前状态
    /// </summary>
    public virtual void DoAfterLeaving() { }
    /// <summary>
    /// 当前状态
    /// </summary>
    public abstract void Act(MonsterController npc);
    /// <summary>
    /// 转换状态的条件
    /// </summary>
    public abstract void Reason(MonsterController npc);
}

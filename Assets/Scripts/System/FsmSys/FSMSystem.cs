using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMSystem {

    private Dictionary<StateID, FSMState> StateDic = new Dictionary<StateID, FSMState>();
    private StateID stateId;
    private FSMState FSMstate;

    public void UpdateFsm(MonsterController npc)
    {
        FSMstate.Act(npc);
        FSMstate.Reason(npc);
    }
    /// <summary>
    /// 添加状态
    /// </summary>
    public void AddStateID(FSMState State)
    {
        if (State == null)
        {
            Debug.LogWarning("传递是状态+" + State + "不能为空"); return;
        }
        if (FSMstate == null)
        {
            FSMstate = State;
            stateId = State.ID;
        }
        if (StateDic.ContainsKey(State.ID))
        {
            Debug.LogWarning("添加的" + State.ID + "已经存在！"); return;
        }
        StateDic.Add(State.ID, State);
 
    }
    public void DeletState(StateID id)
    {
        if (id ==  StateID.Null)
        {
            Debug.LogWarning("要删除的id不能为空"); return;
        }
        if (StateDic.ContainsKey(id))
        {
            StateDic.Remove(id);
        }
        else
        {
            Debug.LogWarning("要删除的id不存在");
        }
    }
    public void PerformTransition(Transition trans)
    {
        if (trans == Transition.Null)
        {
            Debug.LogWarning("trans为null无法发生转换"); return;
        }
        StateID id = FSMstate.GetStateID(trans);
        if (id == StateID.Null)
        {
            Debug.LogWarning("id为null，无法发生转换"); return;
        }
        if (StateDic.ContainsKey(id))
        {
            FSMState state = StateDic[id];
            FSMstate.DoAfterLeaving();
            FSMstate = state;
            stateId = id;
            FSMstate.DoBeforeEntering();
        }
        else
        {
            Debug.LogWarning("要转换的" + id + "不存在");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsderDAO : MonoBehaviour {

    public int CurrentHp;
    public int MaxHp;
    public int AttackNum;
    public int PhylacticPower;
    public MonsderDAO(int hp,int maxHp,int attackNum,int phylacticPower)
    {
        this.CurrentHp = hp;
        this.MaxHp = maxHp;
        this.AttackNum = attackNum;
        this.PhylacticPower = phylacticPower;
    }
}

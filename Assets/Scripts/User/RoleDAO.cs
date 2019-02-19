using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DarkGodAgreement;

    public class RoleDAO
    {
        public string userid { get; set; }
        public int roleid { get; set; }

        public Profession profession { get; set; }
        public int hp { get; set; }
        public int lv { get; set; }
        public string playerName { get; set; }
        public int exp { get; set; }
        public int strength { get; set; }
        public int intelligence { get; set; }
        public int physicalPower { get; set; }
        public int agility { get; set; }
        public int tired { get; set; }
        public int currentExp { get; set; }
        public int currentTired { get; set; }
        public int attackNum { get; set; }
        public int guideID { get; set; }
        public int crystal { get; set; }
        public int coin { get; set; }

        public int[] equipLv { get; set; }

        public int combartNum { get; set; }
        public int intensifyNum { get; set; }
        public int killMOBSNum { get; set; }
        public int worldTalkNum { get; set; }
        public int killBossNum { get; set; }
        public RoleDAO(string userid, int roleid, Profession profession, int lv, string playerName,int exp
            , int strength, int intelligence, int physicalPower, int agility, int tired, int currentExp, int currentTired, int attackNum
            , int guideID, int coin, int hp, int crystal, int[] equipLv
            , int combartNum = 0, int intensifyNum = 0, int killMobsNum = 0, int worldTalkNum = 0, int killBossNum = 0)
        {
            this.userid = userid;
            this.roleid = roleid;
            this.profession = profession;
            this.lv = lv;
            this.playerName = playerName;
            this.exp = exp;
            this.strength = strength;
            this.intelligence = intelligence;
            this.physicalPower = physicalPower;
            this.agility = agility;
            this.tired = tired;
            this.currentExp = currentExp;
            this.currentTired = currentTired;
            this.attackNum = attackNum;
            this.guideID = guideID;
            this.coin = coin;
            this.hp = hp;
            this.crystal = crystal;
            this.equipLv = equipLv;
            this.combartNum = combartNum;
            this.intensifyNum = intensifyNum;
            this.killMOBSNum = killMobsNum;
            this.worldTalkNum = worldTalkNum;
            this.killBossNum = killBossNum;
        }
    }


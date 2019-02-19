using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AutoGuideCfg : BaseDate<AutoGuideCfg>
{
    public int npcID;
    public string dilogArr;
    public int actID;
    public int coin;
    public int exp;
}

public class MapCfg : BaseDate<MapCfg>
{
    public string mMapName;
    public string mSceneName;
    public Vector3 mCamPos;
    public Vector3 mCamRote;
    public Vector3 mPlayerBornPos;
    public Vector3 mPlayerRote;
    
}

public class StrongCfg : BaseDate<StrongCfg>
{
    public int mPos;
    public int mStarlv;
    public int mAddhp;
    public int mAddhurt;
    public int mAdddef;
    public int mMinlv;
    public int mCoin;
    public int mCrystal;

}

public class BaseDate<T>
{
    public int ID;
}


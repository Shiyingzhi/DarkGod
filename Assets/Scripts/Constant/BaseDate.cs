using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class MapCfg : BaseDate<MapCfg>
{
    public string mMapName;
    public string mSceneName;
    public Vector3 mCamPos;
    public Vector3 mCamRote;
    public Vector3 mPlayerBornPos;
    public Vector3 mPlayerRote;
    
}
public class BaseDate<T>
{
    public int ID;
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Xml;
using DarkGodAgreement;

public class ResSvc : MonoBehaviour {
    //加载场景后回调
    private Action prgCB = null;
    public static ResSvc intance = null;

    private Dictionary<Profession, string> mProfession = new Dictionary<Profession, string>();
    private Dictionary<Enemy, string> mEnemy = new Dictionary<Enemy, string>();
    void Update()
    {
        if (prgCB != null)
        {
            prgCB();
        }
    }
    /// <summary>
    /// 初始化加载服务
    /// </summary>
    public void InitSvc()
    {
        intance = this;
        mProfession.Add(Profession.暗影刺客, GameConstant.CityPlayerName);
        mEnemy.Add(Enemy.Soldier, GameConstant.SoldierMon);
        mEnemy.Add(Enemy.Boss, GameConstant.BoosMon);
        InitRangeNameXml();
        InitMapXml();
        InitGuideXml();
        InitStrongCfg();
        
    }
    /// <summary>
    /// 异步加载场景
    /// </summary>
    public void AsyncLoadScene(string sceneName,Action callBack ) 
    {
        GameRoot.instance.mLoadingWin.isShow(); 
        

        AsyncOperation SceneAsync = SceneManager.LoadSceneAsync(sceneName);
        prgCB = () =>
        {
            float val = SceneAsync.progress;
            GameRoot.instance.mLoadingWin.SetProgress(val);
            if (val == 1)
            {
                if (callBack != null)
                {
                    callBack();
                }
                SceneAsync = null;
                GameRoot.instance.mLoadingWin.isShow(false);
                prgCB = null;
            }
        };
    }
    //音频字典
    private Dictionary<string, AudioClip> adDic = new Dictionary<string, AudioClip>();
    //游戏物体字典
    private Dictionary<string, GameObject> GameObjectDic = new Dictionary<string, GameObject>();
    //加载游戏角色物体
    public GameObject LoadGameObjcet(Profession profession,bool isSave)
    {
        string path ="";
        if (!mProfession.TryGetValue(profession, out path))
        {
            Debug.Log("职业：" + profession + "路径不正确");
        }
        GameObject go;
        if (!GameObjectDic.TryGetValue(path, out go))
        {
            go = Resources.Load<GameObject>(path);
            if (isSave)
            {
                GameObjectDic.Add(path, go);
            }
        }
        return GameObject.Instantiate(go);
    }

    //怪物模型字典
    private Dictionary<string, GameObject> EnemytDic = new Dictionary<string, GameObject>();
    //加载怪物模型
    public GameObject LoadGameObjcet(Enemy enemy, bool isSave)
    {
        string path = "";
        if (!mEnemy.TryGetValue(enemy, out path))
        {
            Debug.Log("职业：" + enemy + "路径不正确");
        }
        GameObject go;
        if (!EnemytDic.TryGetValue(path, out go))
        {
            go = Resources.Load<GameObject>(path);
            if (isSave)
            {
                EnemytDic.Add(path, go);
            }
        }
        return GameObject.Instantiate(go);
    }

    //加载Icon
    public Sprite LoadSprite(string path)
    {
        Sprite sprite = Resources.Load<Sprite>(path);
        if (sprite == null)
        {
            Debug.Log("加载路径不正确" + path);
        }
        return sprite;
    }
    public RuntimeAnimatorController LoadAnimatorController(string path)
    {
        return  Resources.Load<RuntimeAnimatorController>(path);
    }
    //加载音频
    public AudioClip LoadCiip(string path, bool isSave)
    {
        AudioClip au = null;
        if (!adDic.TryGetValue(path, out au))
        {
            au = Resources.Load<AudioClip>("ResAudio/" + path);
            if (isSave)
            {
                adDic.Add(path, au);
            }
        }
        return au;
    }
    //获取随机名字
    public string GetXmlName(bool man = true)
    {
        if (man)
        {
            return string.Format("{0}{1}", 
                surnameList[UnityEngine.Random.Range(0, surnameList.Count - 1)],
                manList[UnityEngine.Random.Range(0, manList.Count - 1)]);
        }
        else
        {
            return string.Format("{0}{1}",
                surnameList[UnityEngine.Random.Range(0, surnameList.Count - 1)],
                womanList[UnityEngine.Random.Range(0, womanList.Count - 1)]);
        }
        
    }

    #region 解析随机名字Xml文件
    private List<string> surnameList = new List<string>();
    private List<string> manList = new List<string>(); 
    private List<string> womanList = new List<string>();
    private void InitRangeNameXml()
    {
        TextAsset xmlText = Resources.Load<TextAsset>(GameConstant.XmlRangeName);
        if (xmlText == null)
        {
            Debug.LogError("路径不正确无法读取" + GameConstant.XmlRangeName);
            return;
        }
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(xmlText.text);
        XmlNodeList root = xml.SelectSingleNode("root").ChildNodes;
        for (int i = 0; i < root.Count; i++)
        {
            XmlElement ele = root[i] as XmlElement;
            if (ele.GetAttributeNode("ID") == null)
            {
                continue;
            }
            //int id = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
            foreach (XmlElement item in root[i].ChildNodes)
            {
                switch (item.Name)
                {
                    case"surname":
                        surnameList.Add(item.InnerText);
                        break;
                    case "man":
                        manList.Add(item.InnerText);
                        break;
                    case "woman":
                        womanList.Add(item.InnerText);
                        break;
                }
            }
        }
        
    }
    #endregion
    //地图字典
    private Dictionary<int, MapCfg> mMapDic = new Dictionary<int, MapCfg>();
    #region 解析地图xml文件
    public void InitMapXml()
    {
        TextAsset xmlText = Resources.Load<TextAsset>(GameConstant.XmlMap);
        if (xmlText == null)
        {
            Debug.LogError("路径不正确无法读取" + GameConstant.XmlMap);
            return;
        }
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(xmlText.text);
        XmlNodeList root = xml.SelectSingleNode("root").ChildNodes;
        for (int i = 0; i < root.Count; i++)
        {
            XmlElement ele = root[i] as XmlElement;
            if (ele.GetAttributeNode("ID") == null)
            {
                continue;
            }
            int id = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
            MapCfg map = new MapCfg
            {
                ID = id
            };
            foreach (XmlElement item in root[i].ChildNodes)
            {
                switch (item.Name)
                {
                    case"mapName":
                        map.mMapName = item.InnerText;
                        break;
                    case "sceneName":
                        map.mSceneName = item.InnerText;
                        break;
                    case "mainCamPos":
                        string[] CamVector3 = item.InnerText.Split(',');
                        map.mCamPos = new Vector3(float.Parse(CamVector3[0]), float.Parse(CamVector3[1]), float.Parse(CamVector3[2]));
                        break;
                    case "mainCamRote":
                        string[] CamRoteVector3 = item.InnerText.Split(',');
                        map.mCamRote = new Vector3(float.Parse(CamRoteVector3[0]), float.Parse(CamRoteVector3[1]), float.Parse(CamRoteVector3[2]));
                        break;
                    case "playerBornPos":
                        string[] PlayerVector3 = item.InnerText.Split(',');
                        map.mPlayerBornPos = new Vector3(float.Parse(PlayerVector3[0]), float.Parse(PlayerVector3[1]), float.Parse(PlayerVector3[2]));
                        break;
                    case "playerBornRote":
                        string[] PlayerRoteVector3 = item.InnerText.Split(',');
                        map.mPlayerRote = new Vector3(float.Parse(PlayerRoteVector3[0]), float.Parse(PlayerRoteVector3[1]), float.Parse(PlayerRoteVector3[2]));
                        break;
                }
            }
            mMapDic.Add(id, map);
        }
    }
    #endregion
    //获取地图数据
    public MapCfg GetMapData(int id)
    {
        MapCfg mapData;
        if (mMapDic.TryGetValue(id, out mapData))
        {
            return mapData;
        }
        return null;

    }
    private Dictionary<int,AutoGuideCfg> AutoGuideCfgDic = new Dictionary<int,AutoGuideCfg>();
    #region 解析任务引导xml文件
    public void InitGuideXml()
    {
        TextAsset xmlText = Resources.Load<TextAsset>(GameConstant.XmlAutoGuide);
        if (xmlText == null)
        {
            Debug.LogError("路径不正确无法读取" + GameConstant.XmlAutoGuide);
            return;
        }
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(xmlText.text);
        XmlNodeList root = xml.SelectSingleNode("root").ChildNodes;
        for (int i = 0; i < root.Count; i++)
        {
            XmlElement ele = root[i] as XmlElement;
            if (ele.GetAttributeNode("ID") == null)
            {
                continue;
            }
            int id = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
            AutoGuideCfg guide = new AutoGuideCfg
            {
                ID = id
            };
            foreach (XmlElement item in root[i].ChildNodes)
            {
                switch (item.Name)
                {
                    case "npcID":
                        guide.npcID = int.Parse(item.InnerText);
                        break;
                    case "dilogArr":
                        guide.dilogArr = item.InnerText;
                        break;
                    case "actID":
                        guide.actID = int.Parse(item.InnerText);
                        break;
                    case "coin":
                        guide.coin = int.Parse(item.InnerText);
                        break;
                    case "exp":
                        guide.exp = int.Parse(item.InnerText); 
                        break;
                }
            }
            AutoGuideCfgDic.Add(id, guide);
        }
    }
    #endregion

    public AutoGuideCfg GetAutoGuidCfg(int id)
    {
        AutoGuideCfg guide = null;
        Debug.Log(id);
        if (!AutoGuideCfgDic.TryGetValue(id, out guide))
        {
            return null;
        }
        return guide;
    }
    private Dictionary<int,Dictionary<int,StrongCfg>> StrongCfgDic = new Dictionary<int,Dictionary<int, StrongCfg>>();
    #region 解析强化配置文件
    private void InitStrongCfg()
    {
        TextAsset xmlText = Resources.Load<TextAsset>(GameConstant.XmlStrongCfg);
        if (xmlText == null)
        {
            Debug.LogError("路径不正确无法读取" + GameConstant.XmlStrongCfg);
            return;
        }
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(xmlText.text);
        XmlNodeList root = xml.SelectSingleNode("root").ChildNodes;
        for (int i = 0; i < root.Count; i++)
        {
            XmlElement ele = root[i] as XmlElement;
            if (ele.GetAttributeNode("ID") == null)
            {
                continue;
            }
            int id = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
            StrongCfg sc = new StrongCfg
            {
                ID = id
            };
            foreach (XmlElement item in root[i].ChildNodes)
            {
                switch (item.Name)
                {
                    case "pos":
                        sc.mPos = int.Parse(item.InnerText);
                        break;
                    case "starlv":
                        sc.mStarlv = int.Parse(item.InnerText);
                        break;
                    case "addhp":
                        sc.mAddhp = int.Parse(item.InnerText);
                        break;
                    case "addhurt":
                        sc.mAddhurt = int.Parse(item.InnerText);
                        break;
                    case "adddef":
                        sc.mAdddef = int.Parse(item.InnerText);
                        break;
                    case "minlv":
                        sc.mMinlv = int.Parse(item.InnerText);
                        break;
                    case "coin":
                        sc.mCoin = int.Parse(item.InnerText);
                        break;
                    case "crystal":
                        sc.mCrystal = int.Parse(item.InnerText);
                        break;
                }
            }
            Dictionary<int, StrongCfg> dic = null;
            if (StrongCfgDic.TryGetValue(sc.mPos, out dic))
            {
                dic.Add(sc.mStarlv, sc);
            }else
            {
                dic = new Dictionary<int,StrongCfg>();
                dic.Add(sc.mStarlv,sc);

                StrongCfgDic.Add(sc.mPos, dic);
            }
        }
    }
    #endregion

    public StrongCfg GetStrongCfg(int id ,int starlv)
    {
        StrongCfg CuttentStrong = null;
        Dictionary<int, StrongCfg> StrongDic = null;
        if (StrongCfgDic.TryGetValue(id, out StrongDic))
        {
            if (StrongDic.ContainsKey(starlv))
            {
                CuttentStrong = StrongDic[starlv];
            }
        }
        return CuttentStrong;
    }
}

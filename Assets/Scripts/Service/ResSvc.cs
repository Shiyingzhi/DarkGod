using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Xml;

public class ResSvc : MonoBehaviour {

    private Action prgCB = null;
    public static ResSvc intance = null;

    void Update()
    {
        if (prgCB != null)
        {
            prgCB();
        }
    }
    public void InitSvc()
    {
        intance = this;
        InitXml();
    }
    /// <summary>
    /// 异步加载场景
    /// </summary>
    public void AsyncLoadScene(string sceneName,Action callBack ) 
    {
        GameRoot.intance.mLogonWin.isShow();
        

        AsyncOperation SceneAsync = SceneManager.LoadSceneAsync(sceneName);
        prgCB = () =>
        {
            float val = SceneAsync.progress;
            GameRoot.intance.mLoadingWin.SetProgress(val);
            if (val == 1)
            {
                if (callBack != null)
                {
                    callBack();
                }
                SceneAsync = null;
                GameRoot.intance.mLoadingWin.isShow(false);
                prgCB = null;
            }
        };
    }

    private Dictionary<string, AudioClip> adDic = new Dictionary<string, AudioClip>();
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

    #region 解析Xml文件
    private List<string> surnameList = new List<string>();
    private List<string> manList = new List<string>();
    private List<string> womanList = new List<string>();
    private void InitXml()
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
            int id = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
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
}

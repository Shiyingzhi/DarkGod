using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSvc : MonoBehaviour {
    public static AudioSvc instance = null;

    public AudioSource bgAduio;
    public AudioSource butAduio;
    /// <summary>
    /// 初始化音乐服务
    /// </summary>
    public void InitAudio() { instance = this; }
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="name"></param>
    /// <param name="isLoop"></param>
    public void PlayBgClip(string name, bool isLoop)
    {
        if (bgAduio.clip == null || bgAduio.name != name)
        {
            AudioClip clip = ResSvc.intance.LoadCiip(name,true);
            bgAduio.clip = clip;
            bgAduio.loop = isLoop;
            bgAduio.Play();
        }
    }
    /// <summary>
    /// 播放声音片段
    /// </summary>
    /// <param name="name"></param>
    public void PlayButClip(string name)
    {
        AudioClip clip = ResSvc.intance.LoadCiip(name, true);
        butAduio.clip = clip;
        butAduio.Play();
    }
}

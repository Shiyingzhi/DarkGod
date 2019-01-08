using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSvc : MonoBehaviour {
    public static AudioSvc instance = null;

    public AudioSource bgAduio;
    public AudioSource butAduio;

    public void Init() { instance = this; }

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

    public void PlayButClip(string name)
    {
        AudioClip clip = ResSvc.intance.LoadCiip(name, true);
        butAduio.clip = clip;
        butAduio.Play();
    }
}

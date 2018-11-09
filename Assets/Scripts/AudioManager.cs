using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource[] audioSources;
    [Range(0f, 1f)]
    public float volumeMax = 0.5f;
    [Range(0f, 1f)]
    public float volumeMin = 0f;
    public float muteSpeed = 2f;

    public static bool isMuted;

    private void Start()
    {
        isMuted = false;
    }

    private void LateUpdate()
    {
        Mute();
    }

    public void Mute()
    {
        foreach (AudioSource a in audioSources)
        {
            if (!isMuted)
            {
                a.volume = Mathf.Lerp(a.volume, volumeMax, muteSpeed * Time.deltaTime);
                //a.mute = false;
            }
            else
            {
                a.volume = Mathf.Lerp(a.volume, volumeMin, muteSpeed * Time.deltaTime);
                //a.mute = true;
            }
        }
    }

}

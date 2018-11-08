using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource[] audioSources;

    public static bool isMuted;

    private void Start()
    {
        isMuted = false;
    }

    private void LateUpdate()
    {
        TurnOffSound();
    }

    public void TurnOffSound()
    {
        foreach (AudioSource a in audioSources)
        {
            if (!isMuted)
            {
                a.mute = false;
            }
            else
            {
                a.mute = true;
            }
        }
    }

}

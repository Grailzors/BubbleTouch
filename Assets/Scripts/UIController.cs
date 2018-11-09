using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {



    public void ToggleSprite(GameObject obj, Sprite On, Sprite Off)
    {
        if (obj.GetComponent<Image>().sprite == On)
        {
            obj.GetComponent<Image>().sprite = Off;
        }
        else
        {
            obj.GetComponent<Image>().sprite = On;
        }
    }

    /*
    public void ToggleSound()
    {
        if (UIManager.isSound)
        {
            foreach (Image s in soundButton)
            {
                isSound = false;
                AudioManager.isMuted = true;
                s.GetComponent<Image>().sprite = soundOff;
                //print("turning sound off");
            }
        }
        else if (!UIManager.isSound)
        {
            foreach (Image s in soundButton)
            {
                isSound = true;
                AudioManager.isMuted = false;
                s.GetComponent<Image>().sprite = soundOn;
                //print("turning sound on");
            }
        }
    }
    */
}

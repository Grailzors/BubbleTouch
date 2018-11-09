using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugFrameRate : MonoBehaviour {

    /*
     * REMOVE THIS SCRIPT BEFORE RELEASE
    */
    public Text txt;

    private int fps;
    private int frames;

    private void Start()
    {
        fps = 0;
        frames = 0;

        StartCoroutine(GetFPS());
    }

    // Update is called once per frame
    void Update ()
    {
        frames += 1;		
	}

    private void LateUpdate()
    {
        txt.text = "FPS: " + fps + " Frames: " + frames;
    }

    IEnumerator GetFPS()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);

            fps = frames;
            frames = 0;
        }
    }

}

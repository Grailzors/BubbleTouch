using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public static GameObject[] pucks;

    private bool colorToggle;
    private Color currentColor;
    private Color newColor;

    private void Awake()
    {

        pucks = GameObject.FindGameObjectsWithTag("Puck");
    }
}



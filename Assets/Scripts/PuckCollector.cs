using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckCollector : MonoBehaviour {

    [SerializeField]
    public static GameObject[] pucks;


    private void Awake()
    {
        pucks = GameObject.FindGameObjectsWithTag("Puck");
    }
}

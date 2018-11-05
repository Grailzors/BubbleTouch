using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuckCollector : MonoBehaviour {


    [SerializeField]
    public static GameObject[] pucks;

    private void Awake()
    {
        GetPucks();
    }

    void GetPucks()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            pucks = GameObject.FindGameObjectsWithTag("Puck");
        }
    }
}

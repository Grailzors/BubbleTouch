using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int seed;

    public static int resizeSeed;
    //public static List<GameObject> pucks;
    public static GameObject[] pucks;


    private void Start()
    {
        pucks = GameObject.FindGameObjectsWithTag("Puck");
        //GetPucks();
    }

    private void Update()
    {
        seed = resizeSeed;
        
    }

    /*
    void GetPucks()
    {
        foreach(GameObject p in GameObject.FindGameObjectsWithTag("Puck"))
        {
            print(p.name);
            pucks.Add(p);
        }  
    }
    */

}



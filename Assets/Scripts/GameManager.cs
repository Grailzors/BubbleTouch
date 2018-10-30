using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int seed;

    public static int resizeSeed;
    //public static List<GameObject> pucks;
    public static List<GameObject> pucks;


    private void Start()
    {
        pucks = new List<GameObject>();
        GetPucks();

        foreach (GameObject p in pucks)
        {
            print(p.name);
        }

    }

    private void Update()
    {
        seed = resizeSeed;
        
    }

    void GetPucks()
    {
        foreach(GameObject p in GameObject.FindGameObjectsWithTag("Puck"))
        {
            pucks.Add(p);
        }  
    }
    

}



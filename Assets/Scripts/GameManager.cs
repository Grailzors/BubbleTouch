using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int seed;

    public static int resizeSeed;

    private void Update()
    {
        seed = resizeSeed;
    }


}



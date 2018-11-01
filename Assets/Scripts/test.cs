using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    public Color main;
    public Color highlight;
    public float strength;

    // Update is called once per frame
    void Update ()
    {
        GetComponent<Renderer>().material.SetColor("Color_ACFF11DB", main);

        GetComponent<Renderer>().material.SetColor("Color_CDA4FA5F", highlight);

        GetComponent<Renderer>().material.SetFloat("Vector1_C1BE8C16", strength);
    }
}

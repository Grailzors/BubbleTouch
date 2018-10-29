using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckController : MonoBehaviour {

    [Header("Puck Controls")]
    [Range(1f,7f)]
    public float size = 1f;

    [Header("Color Controls")]
    public float colorChangeTime = 5f;
    [Range(0f, 1f)]
    public float colorChangeSpeed = 0.7f;
    [Space]
    public float minHue = 0f;
    public float maxHue = 1f;
    public float minSaturation = 0.6f;
    public float maxSaturation = 0.9f;
    public float minValue = 0.8f;
    public float maxValue = 1f;
    public float minAlpha = 1f;
    public float maxAlpha = 1f;

    private float t;
    private bool clicked;
    private bool colorToggle;
    private Color currentColor;

    void Start ()
    {
        InitialPuckScale();
        InitialPuckColor();
        clicked = false;
        t = 0f;

        StartCoroutine(TriggerChangeColor());
    }
	
	void Update ()
    {
        ScalePuck();
        ChangeColor();

        GetComponent<Renderer>().material.color = currentColor;
    }

    void InitialPuckScale()
    {
        size = Mathf.Clamp(Random.Range(1f, 8f), 1f, 7f);

        transform.localScale = new Vector3(size, size, 1f);
    }
    
    public Color NewRandColor()
    {
        return Random.ColorHSV(minHue, maxHue, minSaturation, maxSaturation, minValue, maxValue, minAlpha, maxAlpha);
    }

    void InitialPuckColor()
    {
        currentColor = NewRandColor();

        GetComponent<Renderer>().material.color = currentColor;
    }

    void ScalePuck()
    {
        transform.localScale = new Vector3(size, size, 1f);
    }

    void ChangeColor()
    {
        if (colorToggle)
        {
            /*
            t += Time.deltaTime * colorChangeSpeed;
            t = Mathf.Clamp(t, 0f, 1f);
            */

            // the bug here is that the NewRandColor() func is being updated every frame 
            currentColor = Color.Lerp(currentColor, NewRandColor(), colorChangeSpeed);

            print("Changing Color");
            print(t);
            print(currentColor);
        }
        else if (!colorToggle)
        {
            t = 0f;
            print("Done");
        }
    }

    IEnumerator TriggerChangeColor()
    {
        while (clicked == false)
        {
            yield return new WaitForSeconds(colorChangeTime);

            Debug.Log("ChangingColor");
            if (colorToggle)
            {
                colorToggle = false;
            }
            else if (!colorToggle)
            {
                colorToggle = true;
            }
        }
    }


}

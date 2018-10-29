using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckController : MonoBehaviour {

    [Header("Puck Controls")]
    [Range(1f,7f)]
    public float size = 1f;
    public float sizeMin = 1f;
    public float sizeMax = 7f;

    [Header("Color Controls")]
    public float colorChangeTime;
    public float colorChangeMin = 5f;
    public float colorChangeMax = 15f;
    [Range(0f, 1f)]
    public float colorChangeSpeed = 1f;
    [Space]
    public float minHue = 0f;
    public float maxHue = 1f;
    public float minSaturation = 0.6f;
    public float maxSaturation = 0.9f;
    public float minValue = 0.8f;
    public float maxValue = 1f;
    public float minAlpha = 1f;
    public float maxAlpha = 1f;

    private bool clicked;
    private bool colorToggle;
    private Color currentColor;
    private Color newColor;

    void Start ()
    {
        InitialPuckScale();
        InitialPuckColor();
        clicked = false;
        colorChangeTime = Random.Range(colorChangeMin, colorChangeMax);

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
            currentColor = Color.Lerp(currentColor, newColor, (colorChangeSpeed * colorChangeSpeed) * (Time.deltaTime / 2));
        }
    }

    IEnumerator TriggerChangeColor()
    {
        while (clicked == false)
        {
            yield return new WaitForSeconds(colorChangeTime);

            colorChangeTime = Random.Range(colorChangeMin, colorChangeMax);

            Debug.Log("ChangingColor");
            if (colorToggle)
            {
                colorToggle = false;
                currentColor = newColor;
            }
            else if (!colorToggle)
            {
                colorToggle = true;
                newColor = NewRandColor();
            }
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Background Color Controls")]
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


    public static Camera mainCam;
    public static GameObject[] pucks;

    private bool colorToggle;
    private Color currentColor;
    private Color newColor;

    private void Awake()
    {
        mainCam = Camera.main;
        pucks = GameObject.FindGameObjectsWithTag("Puck");
    }

    private void Start()
    {
        currentColor = NewRandColor();
        StartCoroutine(TriggerBackgroundColorChange());
    }

    private void Update()
    {
        UpdateBackgroundColor();
    }

    void UpdateBackgroundColor()
    {
        if (colorToggle)
        {
            currentColor = Color.Lerp(currentColor, newColor, (colorChangeSpeed * colorChangeSpeed) * (Time.deltaTime / 2));
        }

        mainCam.backgroundColor = currentColor;
    }

    public Color NewRandColor()
    {
        return Random.ColorHSV(minHue, maxHue, minSaturation, maxSaturation, minValue, maxValue, minAlpha, maxAlpha);
    }

    IEnumerator TriggerBackgroundColorChange()
    {
        while (true)
        {
            yield return new WaitForSeconds(colorChangeTime);

            colorChangeTime = Random.Range(colorChangeMin, colorChangeMax);

            if (colorToggle)
            {
                colorToggle = false;
                currentColor = newColor;
            }
            else if (!colorToggle)
            {
                colorToggle = true;
                newColor = NewRandColor();

                Debug.Log("Changing Color");
            }
        }
    }
}



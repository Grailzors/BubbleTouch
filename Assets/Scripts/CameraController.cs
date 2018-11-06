using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CameraController : MonoBehaviour {

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
    public float minValue = 0.95f;
    public float maxValue = 1f;
    public float minAlpha = 1f;
    public float maxAlpha = 1f;

    [Header("Camera UI Controls")]
    public Vector3 mainMenuPos = new Vector3(-200,-200,-10);
    public Quaternion mainMenuRot = Quaternion.identity;
    [Space]
    public Vector3 gamePlayPos = new Vector3(0f,0f,-10f);
    public Quaternion gamePlayRot = Quaternion.identity;
    [Space]
    public float camSpeed = 1f;


    public static Camera mainCam;
    private bool colorToggle;
    private Color currentColor;
    private Color newColor;
    private Vector3 newPos;
    private Quaternion newRot;


    private void Awake()
    {
        mainCam = Camera.main;
        DontDestroyOnLoad(mainCam);
    }

    private void Start()
    {
        currentColor = NewRandColor();
        StartCoroutine(TriggerBackgroundColorChange());
    }


    private void LateUpdate()
    {
        UpdateBackgroundColor();
        UpdateCam();
    }

    void UpdateCam()
    {
        if (UIManager.inGame)
        {
            transform.position = Vector3.Lerp(transform.position, gamePlayPos, camSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, gamePlayRot, camSpeed * Time.deltaTime);
        }
        else if (!UIManager.inGame)
        {
            transform.position = Vector3.Lerp(transform.position, mainMenuPos, camSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, mainMenuRot, camSpeed * Time.deltaTime);
        }
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

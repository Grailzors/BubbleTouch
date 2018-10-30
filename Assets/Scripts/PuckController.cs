using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckController : MonoBehaviour {

    [Header("Puck Info")]
    public int puckID;
    public GameObject puckSibling;
    public bool clicked;

    [Header("Puck Controls")]
    [Range(1f,7f)]
    public float size = 1f;
    public float sizeMin = 1f;
    public float sizeMax = 7f;
    public float sizeChangeTime = 10f;
    public float addSizeAmount = 0.5f;
    public float subSizeAmount = 0.25f;

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

    private bool colorToggle;
    private Color currentColor;
    private Color newColor;
    private float initialSize;


    private void Awake()
    {
        SetPuckID();
    }

    void Start ()
    {
        InitialPuckScale();
        InitialPuckColor();
        clicked = false;
        colorChangeTime = Random.Range(colorChangeMin, colorChangeMax);

        StartCoroutine(TriggerChange());
    }
	
	void Update ()
    {
        ScalePuck();
        ChangeColor();

        GetComponent<Renderer>().material.color = currentColor;
    }

    private void OnMouseDown()
    {
        size += addSizeAmount;
        size = Mathf.Clamp(size, sizeMin, sizeMax);
        GameManager.resizeSeed = Random.Range(0, transform.parent.childCount + 1);

        if (!clicked)
        {
            StartCoroutine(ResetClicked());
        }

        /*
        if (GameManager.resizeSeed == puckID && GameManager.resizeSeed != this.puckID)
        {
            //size = new Vector3(subSizeAmount, subSizeAmount, 0f);
            transform.localScale = new Vector3(subSizeAmount, subSizeAmount, 0f);
            //transform.localScale -= new Vector3(subSizeAmount * 2, subSizeAmount * 2, 0f);
        }
        */
        transform.localScale = new Vector3(size, size, 0f);
        puckSibling.transform.localScale -= new Vector3(subSizeAmount, subSizeAmount, 0f);

    }

    void SetPuckID()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i).name == gameObject.name)
            {
                puckID = i;
            }
        }
    }

    void SetSiblingID()
    {
        for (int i = 0; 0 < GameManager.pucks.Count; i++)
        {
            GameObject obj = GameManager.pucks[i].gameObject;

            if ( obj.GetComponent<PuckController>().puckID != this.puckID)
            {
                puckSibling = obj;
                GameManager.pucks.Remove(obj);
            }
        }
    }

    void InitialPuckScale()
    {
        size = Mathf.Clamp(Random.Range(1f, 8f), 1f, 7f);
        initialSize = size;

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

    IEnumerator ResetClicked()
    {
        clicked = true;

        while (true)
        {
            yield return new WaitForSeconds(2);
            
            clicked = false; 
        }
    }

    IEnumerator TriggerChange()
    {
        while (true)
        {
            yield return new WaitForSeconds(sizeChangeTime);

            if (!clicked && transform.localScale.x != initialSize)
            {
                Debug.Log("reducing size");
                transform.localScale = new Vector3(subSizeAmount, subSizeAmount, 0f);
            }

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

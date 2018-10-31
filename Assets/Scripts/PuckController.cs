using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuckController : MonoBehaviour {

    [Header("Puck Info")]
    public int puckID;
    public GameObject puckSibling;
    public bool clicked;

    [Header("Puck Controls")]
    public float penetrationDepth;
    [Range(1f,7f)]
    public float size = 1f;
    public float scaleTime = 1f;
    public float sizeMin = 1f;
    public float sizeMax = 7f;
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
    private float newSize;
    private float newSiblingSize;

    
    private void Awake()
    {
        SetPuckID();
    }

    void Start ()
    {
        InitialPuckScale();
        InitialPuckColor();
        SetSiblingID();
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

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().maxDepenetrationVelocity = penetrationDepth;
    }

    private void OnMouseDown()
    {
        if (!clicked)
        {
            StartCoroutine(ResetClicked());
        }

        newSize = transform.localScale.x + addSizeAmount;
        newSize = Mathf.Clamp(newSize, sizeMin, sizeMax);

        newSiblingSize = puckSibling.transform.localScale.x - subSizeAmount;
        newSiblingSize = Mathf.Clamp(newSiblingSize, sizeMin, sizeMax);
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

    //This Function References the GameManager Script and pairs the objects in the scene
    void SetSiblingID()
    {
        int id = puckID + 1;
        int count = GameManager.pucks.Length - 1;

        foreach (GameObject obj in GameManager.pucks)
        {
            if (this.puckID == GameManager.pucks.Length - 1 && obj.GetComponent<PuckController>().puckID == 0)
            {
                puckSibling = GameManager.pucks[count];
            }
            else if (obj.GetComponent<PuckController>().puckID == id)
            {
                puckSibling = obj;
            }           
        }
    }

    void InitialPuckScale()
    {
        size = Random.Range(sizeMin, sizeMax - 1.5f);
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
        if (clicked)
        {
            //INCREASE THIS PUCK SCALE 
            transform.localScale = Vector3.Lerp(transform.localScale,
                new Vector3(newSize, newSize, 1f),
                scaleTime * (Time.deltaTime / 2));

            //DECREASE SIBLING SCALE
            puckSibling.transform.localScale = Vector3.Lerp(puckSibling.transform.localScale,
                new Vector3(newSiblingSize, newSiblingSize, 1f),
                scaleTime * (Time.deltaTime / 2));
        }
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

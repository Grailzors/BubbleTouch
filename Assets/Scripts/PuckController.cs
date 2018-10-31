using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuckController : MonoBehaviour {

    [Header("Puck Info")]
    public Text puckText;
    public int puckID;
    public GameObject puckSibling;
    public bool clicked;
    public int clickCount = 0;

    [Header("Puck Controls")]
    public float penetrationDepth;
    [Range(1f,7f)]
    public float size = 1f;
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

    private void LateUpdate()
    {
        UpdateClickCounter();
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

        clickCount += 1;
        size += addSizeAmount;

        transform.localScale = new Vector3(size, size, 0f);
        print("Increased Size: " + gameObject.name);

        puckSibling.transform.localScale -= new Vector3(subSizeAmount, subSizeAmount, 0f);
        print("Decreased Size: " + puckSibling.name);
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
    //together (NOT WORKING)
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
        size = Mathf.Clamp(transform.localScale.x, sizeMin, sizeMax);
        transform.localScale = new Vector3(size, size, 1f);
    }

    void ChangeColor()
    {
        if (colorToggle)
        {
            currentColor = Color.Lerp(currentColor, newColor, (colorChangeSpeed * colorChangeSpeed) * (Time.deltaTime / 2));
        }
    }

    void UpdateClickCounter()
    {
        puckText.text = clickCount.ToString();
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

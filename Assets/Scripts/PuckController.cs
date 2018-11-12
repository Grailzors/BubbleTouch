using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckController : MonoBehaviour {

    [Header("Puck Info")]
    public int puckID;
    public GameObject puckSibling;
    public bool isClicked;

    [Header("Puck Controls")]
    public float penetrationDepth;
    [Space]
    public float moveAmountMin = 10f;
    public float moveAmountMax = 20f;
    public float moveTimeMin = 1f;
    public float moveTimeMax = 4f;
    [Space]
    [Range(1f,7f)]
    public float size = 1f;
    public float sizeTime = 1f;
    public float sizeMin = 1f;
    public float sizeMax = 7f;
    public float addSizeAmount = 0.5f;
    public float subSizeAmount = 0.25f;
    [Space]
    public float dragLag = 2.5f;

    [Header("Puck Highlight Control")]
    [Range(0f,1f)]
    public float brighten = 0f;
    public float brightMin = 0f;
    public float brightMax = 1f;
    public float highlightSizeMax = 1.5f;

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
    private Color origColor;
    private float initialSize;
    private float newSize;
    private float newSiblingSize;
    private bool onDrag;
    private Vector3 touchPos;
    private Vector3 origSize;


    private void Awake()
    {
        SetPuckID();
    }

    void Start ()
    {
        InitialPuckScale();
        InitialPuckColor();
        SetSiblingID();

        isClicked = false;
        onDrag = false;
        colorChangeTime = Random.Range(colorChangeMin, colorChangeMax);
        origColor = currentColor;

        StartCoroutine(TriggerChange());
        StartCoroutine(MovePuck());
    }
	
	void Update ()
    {
        HighlightPuck();

        GetComponent<Renderer>().material.SetColor("Color_ACFF11DB", currentColor);

        ScalePuck();
        ChangeColor();
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().maxDepenetrationVelocity = penetrationDepth;
    }

    private void OnMouseDown()
    {
        origSize = transform.localScale;
    }

    private void OnMouseDrag()
    {
        onDrag = true;

        DragPuck();
        DragScale();
    }

    private void OnMouseUp()
    {
        onDrag = false;
        ResizePuck();
    }

    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Puck")
        {
            col.transform.GetComponent<Rigidbody>().AddForce((GetComponent<Rigidbody>().velocity * -1) / 2);
        }
        
    }

    void ResizePuck()
    {
        if (!isClicked)
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
                //print(gameObject.name + " " + i);
            }
        }
    }

    void DragPuck()
    {
        //the 30f on the z is the Camera offset
        if (Input.mousePresent)
        {
            touchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30f);
        }

        if (Input.touchCount > 0)
        {
            touchPos = new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, 30f);
        }

        Vector3 objPos = Camera.main.ScreenToWorldPoint(touchPos);

        //GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(transform.position, objPos, dragLag * Time.deltaTime));
        transform.position = Vector3.Lerp(transform.position, objPos, dragLag * Time.deltaTime);
    }

    void DragScale()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, origSize * 1.12f, dragLag * Time.deltaTime);
    }

    //This Function References the GameManager Script and pairs the objects in the scene
    void SetSiblingID()
    {
        int id = puckID + 1;
        int count = PuckCollector.pucks.Length - 1;

        foreach (GameObject obj in PuckCollector.pucks)
        {
            if (this.puckID == PuckCollector.pucks.Length - 1 && obj.GetComponent<PuckController>().puckID == 0)
            {
                puckSibling = PuckCollector.pucks[count];
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
        if (isClicked)
        {
            //INCREASE THIS PUCK SCALE 
            transform.localScale = Vector3.Lerp(transform.localScale,
                new Vector3(newSize, newSize, 1f),
                sizeTime * (Time.deltaTime / 2));

            //DECREASE SIBLING SCALE
            puckSibling.transform.localScale = Vector3.Lerp(puckSibling.transform.localScale,
                new Vector3(newSiblingSize, newSiblingSize, 1f),
                sizeTime * (Time.deltaTime / 2));
        }
    }
        
    void ChangeColor()
    {
        if (colorToggle)
        {
            currentColor = Color.Lerp(currentColor, newColor, (colorChangeSpeed * colorChangeSpeed) * (Time.deltaTime / 2));
        }
    }

    void HighlightPuck()
    {
        if (onDrag)
        {
            brighten += 2f * Time.deltaTime;
            brighten = Mathf.Clamp01(brighten);

            GetComponent<Renderer>().material.SetFloat("Vector1_C1BE8C16", Mathf.Lerp(brighten, brightMax, 0.001f * Time.deltaTime));
        }
        else if (!onDrag)
        {
            brighten -= 2.5f * Time.deltaTime;
            brighten = Mathf.Clamp01(brighten);

            GetComponent<Renderer>().material.SetFloat("Vector1_C1BE8C16", Mathf.Lerp(brighten, brightMin, 0.001f * Time.deltaTime));
        }
    }

    IEnumerator MovePuck()
    {
        float t = Random.Range(moveTimeMin, moveTimeMax);

        while (true)
        {
            yield return new WaitForSeconds(t);

            float m = Random.Range(moveAmountMin, moveAmountMax);
            int dir = Random.Range(0, 5);
            t = Random.Range(moveTimeMin, moveTimeMax);

            switch(dir)
            {
                case 0:
                    GetComponent<Rigidbody>().AddForce(Vector3.up * m);
                    break;
                case 1:
                    GetComponent<Rigidbody>().AddForce(Vector3.right * m);
                    break;
                case 2:
                    GetComponent<Rigidbody>().AddForce(Vector3.down * m);
                    break;
                case 3:
                    GetComponent<Rigidbody>().AddForce(Vector3.left * m);
                    break;
            }
        }
    }

    IEnumerator ResetClicked()
    {
        isClicked = true;

        while (true)
        {
            yield return new WaitForSeconds(2);
            
            isClicked = false;
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

                //Debug.Log("Changing Color");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    [Header("UI Canvases")]
    public GameObject mainMenu;
    public GameObject inGameMenu;

    [Header("UI Components")]
    public Image[] mainMenuComponents;
    public Image[] inGameComponents;
    public Image[] fadeComponents;
    [Space]
    public Image playButton;
    public Image backButton;
    public Image[] soundButton;
    public Image menuBG;
    public Text titleText;

    [Header("Sprites")]
    public Sprite soundOn;
    public Sprite soundOff;

    [Header("UI Controls")]
    public float toggleMenuTime = 1f;
    public float bgSlideTime = 1f;
    public float fadeTime = 1f;

    [Header("Debug Vars")]
    [SerializeField]
    private bool isSound;
    [SerializeField]
    private bool isHidden;
    [SerializeField]
    private float fadeInAmount;
    [SerializeField]
    private float fadeOutAmount;

    public static bool inGame;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        isSound = true;
        isHidden = true;
        inGame = false;
    }

    public void LateUpdate()
    {
        InGameMenuMove();
        FadeUI(mainMenu, inGameMenu, titleText, mainMenuComponents, inGame, false);
        FadeUI(mainMenu, inGameMenu, null, inGameComponents, inGame, true);
    }

    /*
    IEnumerator ToggleMenu(GameObject obj1, GameObject obj2)
    {
        bool t = true;

        while (t)
        {
            yield return new WaitForSeconds(toggleMenuTime);

            if (obj1.activeSelf)
            {
                obj1.SetActive(false);
                obj2.SetActive(true);
            }
            else if (!obj1.activeSelf)
            {
                obj1.SetActive(true);
                obj2.SetActive(false);
            }

            t = false;
        }
    }
    */

    public void LoadGamePlay()
    {
        //StartCoroutine(ToggleMenu(mainMenu, inGameMenu));
        inGame = true;
    }

    public void ReturnToMainMenu()
    {
        isHidden = true;

        //StartCoroutine(ToggleMenu(inGameMenu, mainMenu));
        inGame = false;
    }

    //Currently trying to make this work very close now
    void FadeUI(GameObject obj1, GameObject obj2, Text txt, Image[] img, bool con, bool reverse)
    {
        float time = fadeTime * Time.deltaTime;

        if (con && !reverse || !con && reverse)
        {
            fadeInAmount += time;
            fadeOutAmount = 1;

            if (txt != null)
            {
                txt.color = new Vector4(txt.color.r, txt.color.g, txt.color.b, Mathf.Lerp(txt.color.a, 0f, time));
            }
            
            if (img != null)
            {
                foreach (Image i in img)
                {
                    i.color = new Vector4(i.color.r, i.color.g, i.color.b, Mathf.Lerp(i.color.a, 0f, time));
                }
            }
        }
        else if (!con && !reverse || con && reverse)
        {
            fadeOutAmount -= time;
            fadeInAmount = 0;

            if (txt != null)
            {
                txt.color = new Vector4(txt.color.r, txt.color.g, txt.color.b, Mathf.Lerp(txt.color.a, 1f, time));
            }

            if (img != null)
            {
                foreach (Image i in img)
                {
                    i.color = new Vector4(i.color.r, i.color.g, i.color.b, Mathf.Lerp(i.color.a, 1f, time));
                }
            }
        }

        
        fadeInAmount = Mathf.Clamp01(fadeInAmount);
        fadeOutAmount = Mathf.Clamp01(fadeOutAmount);

        if (fadeInAmount == 0 || fadeOutAmount == 0)
        {
            obj1.SetActive(false);
            obj2.SetActive(true);
        }
        else if (fadeInAmount == 1 || fadeOutAmount == 1)
        {
            obj1.SetActive(true);
            obj2.SetActive(false);
        }
        
    }

    public void ToggleSound()
    {
        if (isSound)
        {
            foreach (Image s in soundButton)
            {
                isSound = false;
                s.GetComponent<Image>().sprite = soundOff;
                print("turning sound off");
            }
        }
        else if (!isSound)
        {
            foreach (Image s in soundButton)
            {
                isSound = true;
                s.GetComponent<Image>().sprite = soundOn;
                print("turning sound on");
            }
        }
    }

    public void ToggleInGameMenu()
    {
        if (!isHidden)
        {
            isHidden = true;
        }
        else if (isHidden)
        {
            isHidden = false;
        }
    }

    public void InGameMenuMove()
    {
        Vector2 b = menuBG.GetComponent<RectTransform>().anchoredPosition;
        float x = menuBG.GetComponent<RectTransform>().position.x;

        if (!isHidden)
        {
            menuBG.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(b, new Vector2(60f, 0f), bgSlideTime * Time.deltaTime);
            //print("Slide Out");
        }
        else if (isHidden)
        {
            menuBG.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(b, new Vector2(-60f, 0f), bgSlideTime * Time.deltaTime);
            //print("Slide In");
        }
    }
}

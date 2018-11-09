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
    public Image menuButton;
    public GameObject menuBG;
    public Text titleText;

    [Header("Sprites")]
    public Sprite soundOn;
    public Sprite soundOff;
    public Sprite OptionOn;
    public Sprite OptionOff;

    [Header("UI Controls")]
    public float toggleMenuTime = 1f;
    public float bgSlideTime = 1f;
    public float fadeTime = 1f;

    [Header("Debug Vars")]
    [SerializeField]
    public static bool isSound;
    [SerializeField]
    private bool isHidden;
    [SerializeField]
    private float fade;

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
        Fade();

        InGameMenuMove();
        FadeUI(titleText, mainMenuComponents, inGame, false);
        FadeUI(null, inGameComponents, inGame, true);

        ToggleMenu(mainMenu, inGameMenu, fade);
    }

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
    void FadeUI(Text txt, Image[] img, bool con, bool reverse)
    {
        float time = fadeTime * Time.deltaTime;

        if (con && !reverse)
        {
            //fade += time;

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
        else if (!con && !reverse)
        {
            fade -= time;

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

        //fade = Mathf.Clamp01(fade);
    }


    //CURRENTLY DOESN'T FADE THE IN GAME UI PROPERLY 
    void Fade()
    {
        if (inGame)
        {
            fade += fadeTime * (Time.deltaTime / 5);
            //print("adding");
        }
        else if (!inGame)
        {
            fade -= fadeTime * (Time.deltaTime / 5);
            //print("subtracking");
        }

        fade = Mathf.Clamp(fade, 0f, 1f);

    }

    void ToggleMenu(GameObject menu1, GameObject menu2, float value)
    {

        if (value == 1f)
        {
            menu1.SetActive(false);
            menu2.SetActive(true);

            //print("Main Menu On");

            
            if (value < 0.7f)
            {
                menu2.SetActive(true);
            }
            
        }
        else if (value < 0)
        {
            menu1.SetActive(true);
            menu2.SetActive(false);

            //print("Main Menu Off");

            
            if (value > 0.3f)
            {
                menu2.SetActive(false);
            }
            
        }
    }

    public void ToggleButton()
    {
        if (menuButton.GetComponent<Image>().sprite == OptionOn)
        {
            menuButton.GetComponent<Image>().sprite = OptionOff;
        }
        else
        {
            menuButton.GetComponent<Image>().sprite = OptionOn;
        }
    }

    /*
    public void ToggleSprite(GameObject obj, Sprite On, Sprite Off)
    {
        if (obj.GetComponent<Image>().sprite == On)
        {
            obj.GetComponent<Image>().sprite = Off;
        }
        else
        {
            obj.GetComponent<Image>().sprite = On;
        }
    }
    */

    public void ToggleSound()
    {
        if (isSound)
        {
            foreach (Image s in soundButton)
            {
                isSound = false;
                AudioManager.isMuted = true;
                s.GetComponent<Image>().sprite = soundOff;
                //print("turning sound off");
            }
        }
        else if (!isSound)
        {
            foreach (Image s in soundButton)
            {
                isSound = true;
                AudioManager.isMuted = false;
                s.GetComponent<Image>().sprite = soundOn;
                //print("turning sound on");
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

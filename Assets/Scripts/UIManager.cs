using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public static MenuType.menuType currentMenu = MenuType.menuType.main;

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
    private Sprite sprite1;

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
        //Fade();

        InGameMenuMove();
        //FadeUI(titleText, mainMenuComponents, inGame, false);
        //FadeUI(null, inGameComponents, inGame, true);

        //ToggleMenu(mainMenu, inGameMenu, fade);
    }

    public void LoadGamePlay()
    {
        //StartCoroutine(ToggleMenu(mainMenu, inGameMenu));
        inGame = true;
        currentMenu = MenuType.menuType.game;
    }

    public void ReturnToMainMenu()
    {
        isHidden = true;

        //StartCoroutine(ToggleMenu(inGameMenu, mainMenu));
        inGame = false;

        currentMenu = MenuType.menuType.main;
    }

    public void ToggleSound()
    {
        if (isSound)
        {
            foreach (Image s in soundButton)
            {
                isSound = false;
                AudioManager.isMuted = true;
                s.GetComponent<Image>().sprite = soundOff;
            }
        }
        else if (!isSound)
        {
            foreach (Image s in soundButton)
            {
                isSound = true;
                AudioManager.isMuted = false;
                s.GetComponent<Image>().sprite = soundOn;
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


[System.Serializable]
public class MenuType {

    public enum menuType { main, game }

}



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
    public Image playButton;
    public Image backButton;
    public Image[] soundButton;
    public Image menuBG;

    [Header("Sprites")]
    public Sprite soundOn;
    public Sprite soundOff;

    [Header("UI Controls")]
    public float bgSlideTime = 1f;

    [Header("Debug Vars")]
    [SerializeField]
    private bool isSound;
    [SerializeField]
    private bool isHidden;
    [Space]
    [SerializeField]
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
    }

    public void LoadGamePlay()
    {
        if (!inGameMenu.activeSelf)
        {
            inGame = true;
            inGameMenu.SetActive(true);
            mainMenu.SetActive(false);
        }
    }

    public void ReturnToMainMenu()
    {
        isHidden = true;

        if (!mainMenu.activeSelf)
        {
            inGame = false;
            mainMenu.SetActive(true);
            inGameMenu.SetActive(false);
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

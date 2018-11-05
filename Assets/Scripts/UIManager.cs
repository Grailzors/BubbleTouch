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

    [Space]
    [SerializeField]
    private bool isSound;
    [SerializeField]
    private bool isHidden;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        isSound = true;
        isHidden = true;
    }

    public void LateUpdate()
    {
        InGameMenuMove();
    }

    public void LoadGamePlay()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Additive);

        if (!inGameMenu.activeSelf)
        {
            inGameMenu.SetActive(true);
            mainMenu.SetActive(false);
        }
    }

    public void ReturnToMainMenu()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        SceneManager.UnloadSceneAsync("Main");

        if (!mainMenu.activeSelf)
        {
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
            print("Slide Out");
        }
        else if (isHidden)
        {
            menuBG.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(b, new Vector2(-60f, 0f), bgSlideTime * Time.deltaTime);
            print("Slide In");
        }
    }
}

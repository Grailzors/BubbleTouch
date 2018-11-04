using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    [Header("UI Components")]
    public Image playButton;
    public Image backButton;
    public Image[] soundButton;
    public Image menuBG;

    [Header("Sprites")]
    public Sprite soundOn;
    public Sprite soundOff;

    [SerializeField]
    private bool isSound;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        isSound = true;
    }

    public void LoadGamePlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ToggleSound()
    {
        //Have This function talk to the audio to turn of sound
        foreach (Image s in soundButton)
        {
            if (isSound)
            {
                isSound = false;
                s.GetComponent<Image>().sprite = soundOff;
            }
            else if (!isSound)
            {
                isSound = true;
                s.GetComponent<Image>().sprite = soundOn;
            }
        }
    }


}

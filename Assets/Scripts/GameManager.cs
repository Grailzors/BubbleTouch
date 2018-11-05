using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Camera mainCam;

    [SerializeField]
    public static GameObject[] pucks;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadUI();
        LoadCamera();
    }

    /*
     * DOESNT WORK AND IS BEING DEPRICATED GO WITH NEW IDEA INSTEAD
    private void OnLevelWasLoaded(int level)
    {
        switch(level)
        {
            case 1:
                GetPucks();
                break;
            default:
                break;
        }
    }
    */

    void GetPucks()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            pucks = GameObject.FindGameObjectsWithTag("Puck");
        }
    }

    void LoadCamera()
    {
        Instantiate(mainCam);
    }

    void LoadUI()
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }
}




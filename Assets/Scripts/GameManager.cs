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

    private void OnLevelWasLoaded(int level)
    {
        GetPucks();
    }

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




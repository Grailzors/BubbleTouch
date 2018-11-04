using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameObject[] pucks;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }

    private void Start()
    {
                
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
}




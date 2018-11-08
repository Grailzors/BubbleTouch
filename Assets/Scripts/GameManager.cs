using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Camera mainCam;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.LoadScene(2, LoadSceneMode.Additive);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    private void Start()
    {
        Instantiate(mainCam, mainCam.GetComponent<CameraController>().mainMenuPos, mainCam.GetComponent<CameraController>().mainMenuRot);
    }
}
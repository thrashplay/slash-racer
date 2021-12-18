using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameController gameController;

    public BooleanValue isPaused;

    private GameObject[] pauseObjects;

    void Start()
    {
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPaused");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
			{
				gameController.OnPause();
			} 
            else if (Time.timeScale == 0) 
            {
                gameController.OnUnpause();
			}
        }

        SetMenuVisible(isPaused.Value);
    }

    public void OnExitPressued()
    {
        gameController.OnQuit();
    }

    public void OnResumePressed()
    {
        gameController.OnUnpause();
    }

    private void SetMenuVisible(bool visible)
    {
        foreach (var pauseObject in pauseObjects)
        {
            pauseObject.SetActive(visible);
        }
    }
}

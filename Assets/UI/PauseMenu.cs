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

        // start unpaused by default
        Time.timeScale = 1;
        SetMenuVisible(false);
        isPaused.Value = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
			{
				OnPause();
			} 
            else if (Time.timeScale == 0) 
            {
                OnUnpause();
			}
        }
    }

    public void OnExit()
    {
        gameController.OnQuit();
    }

    public void OnPause()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            SetMenuVisible(true);

            isPaused.Value = true;
        }
    }

    public void OnUnpause()
    {
        if (Time.timeScale != 1) 
        {
            Time.timeScale = 1;
            SetMenuVisible(false);

            isPaused.Value = false;
        }
    }

    private void SetMenuVisible(bool visible)
    {
        foreach (var pauseObject in pauseObjects)
        {
            pauseObject.SetActive(visible);
        }
    }
}

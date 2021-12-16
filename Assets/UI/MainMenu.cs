using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnNewGame()
    {
        SceneManager.LoadScene("RaceScene");
    }

    public void OnQuit() {
        Application.Quit ();
    }
}

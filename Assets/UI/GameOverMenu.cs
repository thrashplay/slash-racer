using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public IntegerValue score;

    public Text finalScoreText;

    void Start()
    {
        finalScoreText.text = "Final Score: " + score.Value;
    }

    public void OnReturnToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}

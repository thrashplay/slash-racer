using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public IntegerValue lives;

    public IntegerValue score;

    public Text livesText;

    public Text scoreText;

    void Start()
    {
        livesText.text = "Lives: " + lives.Value;
        scoreText.text = "Score: " + score.Value;
    }

    void Update()
    {
        livesText.text = "Lives: " + lives.Value;
        scoreText.text = "Score: " + score.Value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject fadingTextPrefab;

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

    // show an announcement in the center of the screen
    public void Announce(string message, float time = 1.0F)
    {
        var text = Instantiate(fadingTextPrefab, transform);
        var script = text.GetComponent<FadingText>();
        script.message = message;
        script.fadeTime = time;
    }
}

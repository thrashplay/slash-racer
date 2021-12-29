using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollisionHandler : MonoBehaviour
{
    public GameConfig gameConfig;

    public IntegerValue score;

    public IntegerValue time;

    private SoundManager _soundManager;

    void Start()
    {
        _soundManager = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _soundManager.PlayItemPickedUp();

            score.Value += gameConfig.StarPointValue;
            if (gameConfig.TimeLimitEnabled)
            {
                time.Value += gameConfig.StarBonusTime;
            }

            Destroy(gameObject);
        }
    }
}

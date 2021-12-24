using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameConfig gameConfig;

    public BooleanValue gameOver;

    public SoundManager soundManager;

    public IntegerValue time;

    private float _elapsed;

    void Start()
    {
        time.Value = gameConfig.InitialTimeLimit;
    }

    void FixedUpdate()
    {
        if (gameOver.Value)
        {
            return;
        }

        _elapsed += Time.fixedDeltaTime;

        if (_elapsed > 1)
        {
            while (_elapsed > 1)
            {
                time.Value--;
                _elapsed -= 1;
            }

            if (time.Value <= 3 && time.Value > 0)
            {
                soundManager.PlayLowTimeWarning();
            }
        }
    }
}

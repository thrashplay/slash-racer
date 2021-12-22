using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameConfig gameConfig;

    public IntegerValue time;

    private float _elapsed;

    void Start()
    {
        time.Value = gameConfig.InitialTimeLimit;
    }

    void FixedUpdate()
    {
        _elapsed += Time.fixedDeltaTime;
        while (_elapsed > 1)
        {
            time.Value--;
            _elapsed -= 1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnController : MonoBehaviour
{
    public SpawnableConfig config;

    public Trigger trigger;

    // if true, the initial delay has passed and this spawner is active
    private bool _active = false;

    private float _nextSpawn;

    private IRacetrack _racetrack;

    // Start is called before the first frame update
    void Start()
    {
        _racetrack = GetComponent<IRacetrack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!config.Enabled || _racetrack.Y <= config.InitialDelay)
        {
            return;
        }

        if (!_active)
        {
            // set our initial spawn and active flags
            _active = true;
            UpdateNextSpawn();
        }

        if (_racetrack.Y >= _nextSpawn)
        {
            UpdateNextSpawn();
            trigger.Emit();
        }
    }

    private void UpdateNextSpawn()
    {
        _nextSpawn = _racetrack.Y + Random.Range(config.FrequencyMin, config.FrequencyMax);
    }
}

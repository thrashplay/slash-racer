using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    public float frequency = 10;

    public SpawnObstacleEvent spawnObstacleEvent;

    private float _lastSpawn;

    private IRacetrack _racetrack;

    // Start is called before the first frame update
    void Start()
    {
        _lastSpawn = 0;
        _racetrack = GetComponent<IRacetrack>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((_racetrack.Y - _lastSpawn) >= frequency)
        {
            _lastSpawn = _racetrack.Y;
            spawnObstacleEvent.Emit();
        }
    }
}

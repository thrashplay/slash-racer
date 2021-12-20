using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    // minimize vertical space between obstacles
    public float frequencyMin = 6;
    // maximize vertical space between obstacles
    public float frequencyMax = 20;

    public SpawnObstacleEvent spawnObstacleEvent;

    private float _nextSpawn;

    private IRacetrack _racetrack;

    // Start is called before the first frame update
    void Start()
    {
        _racetrack = GetComponent<IRacetrack>();
        UpdateNextSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (_racetrack.Y >= _nextSpawn)
        {
            UpdateNextSpawn();
            spawnObstacleEvent.Emit();
        }
    }

    private void UpdateNextSpawn()
    {
        _nextSpawn = _racetrack.Y + Random.Range(frequencyMin, frequencyMax);
    }
}

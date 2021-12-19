using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicObstacleSpawner : MonoBehaviour, ISpawnObstacleListener
{
    public GameObject obstaclePrefab;

    public SpawnObstacleEvent spawnObstacleEvent;

    private IRacetrack _racetrack;

    // Start is called before the first frame update
    void Start()
    {
        _racetrack = GetComponent<IRacetrack>();
        spawnObstacleEvent.AddListener(this);
    }

    public void OnSpawnObstacle()
    {
        var position = new Vector3(Random.Range(_racetrack.Left, _racetrack.Right), _racetrack.Y, 0);
        Instantiate(obstaclePrefab, position, Quaternion.identity);
    }
}

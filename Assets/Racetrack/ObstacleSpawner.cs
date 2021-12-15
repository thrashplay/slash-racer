using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public float frequency = 5;

    public GameObject obstaclePrefab;

    public float maxX = 10;
    public float minX = -10;

    private float _timeAccumulator;

    // Start is called before the first frame update
    void Start()
    {
        _timeAccumulator = 0;    
    }

    // Update is called once per frame
    void Update()
    {
        _timeAccumulator += Time.deltaTime;

        if (_timeAccumulator >= frequency)
        {
            _timeAccumulator = 0;
            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        float top = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0)).y;
        var y = top + 1;

        var position = new Vector3(Random.Range(minX, maxX), y, 0);
        Instantiate(obstaclePrefab, position, Quaternion.identity);
    }
}

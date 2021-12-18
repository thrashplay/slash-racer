using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public float frequency = 10;

    public GameObject obstaclePrefab;

    public float maxX = 10;
    public float minX = -10;

    private float _lastSpawn;

    // Start is called before the first frame update
    void Start()
    {
        _lastSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float top = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0)).y;

        if (top - _lastSpawn >= frequency)
        {
            _lastSpawn = top;
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

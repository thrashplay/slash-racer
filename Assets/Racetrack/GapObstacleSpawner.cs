using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GapObstacleSpawner : MonoBehaviour, ISpawnObstacleListener
{
    public float gapWidth = 2F;

    public GameObject obstaclePrefab;

    public IntegerValue playerSteeringSpeed;

    public SpawnObstacleEvent spawnObstacleEvent;

    // center x coordinate of the last gap
    private float _lastX;

    // y coordinate of the last gap
    private float _lastY;

    private IRacetrack _racetrack;

    // Start is called before the first frame update
    void Start()
    {
        _racetrack = GetComponent<IRacetrack>();
        spawnObstacleEvent.AddListener(this);
    }

    public void OnSpawnObstacle()
    { 
        var y = _racetrack.Y;
        var maxDeltaX = (y - _lastY) * playerSteeringSpeed.Value;
        var minX = Mathf.Max(_racetrack.Left, _lastX - maxDeltaX);
        var maxX = Mathf.Min(_racetrack.Right, _lastX + maxDeltaX);
        var gapLeft = Random.Range(minX, maxX - gapWidth);
        var gapRight = gapLeft + gapWidth;

        if (gapLeft > _racetrack.Left)
        {
            SpawnObstacle(_racetrack.Left, gapLeft, y);
        }

        if (gapRight < _racetrack.Right)
        {
            SpawnObstacle(gapRight, _racetrack.Right, y);
        }
    }

    private void SpawnObstacle(float left, float right, float y)
    {
        var width = right - left;
        var centerX = left + (width / 2);
        var position = new Vector3(centerX, _racetrack.Y, 0);
        var obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity);

        var defaultScale = obstacle.transform.localScale;
        obstacle.transform.localScale = new Vector3(width, defaultScale.y, defaultScale.z);

        _lastX = centerX;
        _lastY = y;
    }
}

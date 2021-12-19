using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GapObstacleSpawner : MonoBehaviour, ISpawnObstacleListener
{
    // reduce the maximum gap distance by this much, reducing the perfect play required to move
    // between the most distant gaps
    public float forgiveness = 0.75F;

    public float gapWidthMax = 1.9F;
    public float gapWidthMin = 0.6F;

    public GameObject obstaclePrefab;

    public IntegerValue playerBaseSpeed;
    
    public Vector2Value playerPosition;

    public IntegerValue playerSteeringSpeed;

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
        var gapWidth = Random.Range(gapWidthMin, gapWidthMax);

        var y = _racetrack.Y;
        var distanceTilNextObstacle = y - playerPosition.Value.y;
        var timeTilNextObstacle = distanceTilNextObstacle / playerBaseSpeed.Value;
        var maxDeltaX = (timeTilNextObstacle * playerSteeringSpeed.Value) - forgiveness;
        var minX = Mathf.Max(_racetrack.Left, playerPosition.Value.x - maxDeltaX);
        var maxX = Mathf.Min(_racetrack.Right, playerPosition.Value.x + maxDeltaX);
        
        Debug.Log("maxDelta:" + maxDeltaX + ", ttno" + timeTilNextObstacle + ", x:" + playerPosition.Value.x + ", minx:" + minX + ", maxx:" + maxX);

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
    }
}

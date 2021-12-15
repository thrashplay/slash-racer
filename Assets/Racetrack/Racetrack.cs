using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racetrack : MonoBehaviour
{
    public float WallSegmentHeight = 0.5F;

    public float WallSegmentWidth = 0.1F;

    public GameObject WallSegmentPrefab;

    private float _currentLeft;

    private float _currentRight;

    private float _targetLeft;

    private float _targetRight;

    // y-coordinate for the highest segment of track that has been created
    private float _trackTop;

    void Start()
    {

        _currentLeft = -2;
        _currentRight = 2;

        // initialize the top of our track to be just outside the bottom edge of the scene
        _trackTop = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
    }

    void Update()
    {
        float top = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0)).y;

        while (_trackTop < top)
        {
            Color color = Random.ColorHSV();

            _trackTop += WallSegmentHeight;
            float y = _trackTop - (WallSegmentHeight / 2);

            CreateWall(_currentLeft, y, color);
            CreateWall(_currentRight, y, color);
        }
    }

    private void CreateWall(float x, float y, Color color)
    {
        GameObject wall = Instantiate(WallSegmentPrefab);
        wall.GetComponent<SpriteRenderer>().color = color;
        wall.transform.position = new Vector3(x, y, 0);
        wall.transform.localScale = new Vector3(WallSegmentWidth, WallSegmentHeight, 1);
    }
}

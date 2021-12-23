using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRacetrack
{
    // y position of the current end of the track
    float Y { get; }

    // x coordinate of the left coordinate of the track, at position Y
    float Left { get; }

    // x coordinate of the right coordinate of the track, at position Y
    float Right { get; }
}

public class Racetrack : MonoBehaviour, IRacetrack
{
    public GameConfig gameConfig;

    public float WallSegmentHeight = 0.5F;

    public GameObject WallSegmentPrefab;

    private float _currentLeft;

    private float _currentRight;

    // y-coordinate for the highest segment of track that has been created
    private float _trackTop;

    void Start()
    {
        _currentLeft = -gameConfig.InitialTrackWidth / 2F;
        _currentRight = gameConfig.InitialTrackWidth / 2F;

        // initialize the top of our track to be just outside the bottom edge of the scene
        _trackTop = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
    }

    void Update()
    {
        float top = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0)).y + 2;

        while (_trackTop < top)
        {
            _trackTop += WallSegmentHeight;
            float y = _trackTop - (WallSegmentHeight / 2);

            CreateWall(_currentLeft, y);
            CreateWall(_currentRight, y);
        }
    }

    public float Y
    {
        get { return _trackTop; }
    }

    public float Left
    {
        get { return _currentLeft; }
    }

    public float Right
    {
        get { return _currentRight; }
    }

    private void CreateWall(float x, float y)
    {
        GameObject wall = Instantiate(WallSegmentPrefab);
        wall.transform.position = new Vector3(x, y, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointerSteeringInputController : MonoBehaviour
{
    // size of the region around the player where they will drive straight
    public float deadZoneSize = 1F;

    public BooleanValue isPaused;

    private IPlayerController _player;

    void Start()
    {
        _player = GetComponent<IPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused.Value)
        {
            return;
        }

        // get the mouse click position, in world units
        var mousePosition = Camera.main.ScreenToWorldPoint(
            Input.mousePosition
        );

        if (Mathf.Abs(mousePosition.x - _player.Position.x) <= (deadZoneSize / 2F))
        {
            _player.SteerStraight();
        }
        else if (mousePosition.x < _player.Position.x)
        {
            _player.SteerLeft();
        } 
        else 
        {
            _player.SteerRight();
        }
    }
}

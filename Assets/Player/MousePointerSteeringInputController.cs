using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointerSteeringInputController : MonoBehaviour
{
    private IPlayerController _player;

    void Start()
    {
        _player = GetComponent<IPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // get the mouse click position, in world units
        var mousePosition = Camera.main.ScreenToWorldPoint(
            Input.mousePosition
        );

        if (mousePosition.x < _player.Position.x)
        {
            _player.SteerLeft();
        } else 
        {
            _player.SteerRight();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointerSteeringInputController : MonoBehaviour
{
    public BooleanValue isPaused;

    private int _heading;

    private IPlayerController _player;

    void Start()
    {
        _player = GetComponent<IPlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isPaused.Value)
        {
            return;
        }

        var mouse = GetMouse();

        if (mouse.y > _player.Position.y)
        {
            var input = (int) Input.GetAxis("Mouse X");
            _heading = _player.SteerTo(_heading += input);
         }
    }

    private Vector2 GetMouse()
    {
        // get the mouse position, in world units
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mousePosition;
    }
}

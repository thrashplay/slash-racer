using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseButtonSteeringInputController : MonoBehaviour
{
    private IPlayerController _player;

    void Start()
    {
        _player = GetComponent<IPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _player.SteerLeft();
        } else if (Input.GetMouseButton(1))
        {
            _player.SteerRight();
        } else 
        {
            _player.SteerStraight();
        }

    }
}

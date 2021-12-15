using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneShiftInputController : MonoBehaviour
{
    public float laneWidth = 0.6F;

    private float _laneTarget;

    private IPlayerController _player;

    void Start()
    {
        _player = GetComponent<IPlayerController>();
        _laneTarget = _player.Position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnLeftPressed();
        }

        if (Input.GetMouseButtonDown(1))
        {
            OnRightPressed();
        }

        if (_player.Direction == SteeringDirection.Left && _player.Position.x <= _laneTarget)
        {
            _player.SteerStraight();
        }

        if (_player.Direction == SteeringDirection.Right && _player.Position.x >= _laneTarget)
        {
            _player.SteerStraight();
        }
    }

    private void OnLeftPressed()
    {
        if (_player.Direction == SteeringDirection.Right || _player.Direction == SteeringDirection.Straight)
        {
            _player.SteerLeft();
            _laneTarget -= laneWidth;
        }
    }

    private void OnRightPressed()
    {
        if (_player.Direction == SteeringDirection.Left || _player.Direction == SteeringDirection.Straight)
        {
            _player.SteerRight();
            _laneTarget += laneWidth;
        }
    }
}

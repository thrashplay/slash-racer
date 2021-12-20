using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyboardInputController : MonoBehaviour
{
    public BooleanValue isPaused;

    public KeyBindings keyBindings;

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
        
        if (IsPressing(keyBindings.SteerLeft))
        {
            _player.SteerLeft();
        } else if (IsPressing(keyBindings.SteerRight))
        {
            _player.SteerRight();
        } else 
        {
            _player.SteerStraight();
        }

        _player.IsAccelerating = IsPressing(keyBindings.Accelerate);
    }

    private bool IsPressing(KeyCode[] keyCodes)
    {
        return keyCodes.Any(keyCode => Input.GetKey(keyCode));
    }
}

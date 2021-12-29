using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/PlayerConfig", order = 1)]
public class PlayerConfig : ScriptableObject
{
    public float Acceleration = 0.5F;

    // player's base speed in meters/second
    public float BaseSpeed = 7;

    // player's maximum speed in meters/second
    public float MaxSpeed = 14;

    // maximum angle, in degrees, that the player can steer away from straight ahead
    public int SteeringLimit = 15;

    // how quickly the player turns when steering
    public int SteeringSpeed = 5;
}

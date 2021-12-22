using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpConfig", menuName = "ScriptableObjects/PowerUpConfig", order = 1)]
public class PowerUpConfig : ScriptableObject
{
    // minimum rotational speed of power-ups, in degrees/second
    public float MinRotation = 0;

    // maximum rotational speed of power-ups, in degrees/second
    public float MaxRotation = 360;

    // minimum horizontal speed of power-ups, in meters/second
    public float MinSpeed = 0;

    // maximum horizontal speed of power-ups, in meters/second
    public float MaxSpeed = 5;
}

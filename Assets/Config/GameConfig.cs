using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig", order = 1)]
public class GameConfig : ScriptableObject
{
    // delay in seconds after the player dies before showing game over
    public float DelayAfterDeath = 2F;

    // starting time before game over
    public int InitialTimeLimit = 30;

    // how wide the track is when the game starts
    public int InitialTrackWidth = 8;

    // how much additional time is granted by a star
    public int StarBonusTime = 3;

    // number of lives the player starts with
    public int StartingLives = 1;

    // if true, speed will be adjusted to keep forward movement constant
    public bool KeepForwardSpeedConstant = true;

    // if true, the player will straighten out automatically when not steering
    public bool SteeringAutoStraighten = true;

    // if true, colliding with a wall will destroy the player
    public bool WallsAreFatal = true;
}

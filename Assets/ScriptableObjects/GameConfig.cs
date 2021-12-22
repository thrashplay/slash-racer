using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig", order = 1)]
public class GameConfig : ScriptableObject
{
    // starting time before game over
    public int InitialTimeLimit = 30;

    // how much additional time is granted by a star
    public int StarBonusTime = 3;

    // number of lives the player starts with
    public int StartingLives = 1;

    // if true, the player will straighten out automatically when not steering
    public bool SteeringAutoStraighten = true;

    // if true, colliding with a wall will destroy the player
    public bool WallsAreFatal = true;
}

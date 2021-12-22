using UnityEngine;

// Interface defining behaviors exposed by the player object
public interface IPlayerController
{
  // sets whether the player is accelerating or not
  bool IsAccelerating { get; set; }

  Vector2 Position { get; }

  // Called when the player should begin steering left
  void SteerLeft();

  // Called when the player should begin steering right
  void SteerRight();

    // Called when the player should begin driving straight ahead
  void SteerStraight();

  // steer the player to the specified angle; returns the actual angle set
  int SteerTo(int angle);
}
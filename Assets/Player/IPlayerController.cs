using UnityEngine;

// Interface defining behaviors exposed by the player object
public interface IPlayerController
{
  // Direction the player is currently heading
  SteeringDirection Direction { get; set; }

  Vector2 Position { get; }

  // Called when the player should begin steering left
  void SteerLeft();

  // Called when the player should begin steering right
  void SteerRight();

    // Called when the player should begin driving straight ahead
  void SteerStraight();
}
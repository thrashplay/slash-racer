using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SteeringDirection
{
    Left,
    Right,
    Straight
}

public class Player : MonoBehaviour, IPlayerController
{
    public float acceleration = 0.25F;

    public IntegerValue BaseDrivingSpeed;

    public float maxSpeed = 24F;

    public PlayerCrashedEvent playerCrashedEvent;

    public Vector2Value position;

    public IntegerValue score;

    public IntegerValue steeringSpeed;

    public SteeringDirection Direction { get; set; }

    private float _currentSpeed;

    private float _rawScore;

    private Rigidbody2D _rigidbody;

    void Start()
    {
        _currentSpeed = BaseDrivingSpeed.Value;
  
        Direction = SteeringDirection.Straight;
        _rigidbody = GetComponent<Rigidbody2D>();
        position.Value = _rigidbody.position;
    }

    void Update()
    {
        var position = transform.position;
        Camera.main.transform.position = new Vector3(0, position.y + 2.5F, -10);
    }

    void FixedUpdate() 
    {
        _currentSpeed = IsAccelerating
            ? Mathf.Min(maxSpeed, _currentSpeed + acceleration)
            : BaseDrivingSpeed.Value;

        var delta = Time.deltaTime * new Vector2(GetVelocityX(), _currentSpeed); 
        position.Value += delta;
        _rigidbody.MovePosition(position.Value);

        _rawScore += IsAccelerating ? 8 : 1;
        var increment = Mathf.FloorToInt(_rawScore / 25);
        _rawScore -= 25 * increment;
        score.Value += increment;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Obstacle"))
        {
            // destroy any obstacles the player crashes into
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                Destroy(collision.gameObject);
            }

            // handle the crash            
            OnCrashed();
        }
    }

    private float GetVelocityX()
    {
        return Direction switch
        {
            SteeringDirection.Left => -steeringSpeed.Value,
            SteeringDirection.Right => steeringSpeed.Value,
            _ => 0,
        };
    }

    private void OnCrashed()
    {
        playerCrashedEvent.Emit(this);
        Destroy(gameObject);
    }

    //
    // IPlayerController
    //

    public bool IsAccelerating { get; set; }

    public Vector2 Position
    {
        get
        {
            return transform.position;
        }
    }

    public void SteerLeft()
    {
        if (Direction != SteeringDirection.Left)
        {
            Direction = SteeringDirection.Left;
            transform.rotation = Quaternion.identity;
            transform.Rotate(Vector3.back, -22);
        }
    }

    public void SteerRight()
    {
        if (Direction != SteeringDirection.Right)
        {
            Direction = SteeringDirection.Right;
            transform.rotation = Quaternion.identity;
            transform.Rotate(Vector3.back, 22);
        }
    }

    public void SteerStraight()
    {
        if (Direction != SteeringDirection.Straight)
        {
            Direction = SteeringDirection.Straight;
            transform.rotation = Quaternion.identity;
        }
    }
}

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
    public IntegerValue BaseDrivingSpeed;

    public PlayerCrashedEvent playerCrashedEvent;

    public Vector2Value position;

    public IntegerValue score;

    public IntegerValue steeringSpeed;

    public SteeringDirection Direction { get; set; }

    private Rigidbody2D _rigidbody;

    void Start()
    {
        Direction = SteeringDirection.Straight;
        _rigidbody = GetComponent<Rigidbody2D>();
        position.Value = _rigidbody.position;
    }

    void Update()
    {
        var position = transform.position;
        Camera.main.transform.position = new Vector3(0, position.y + 2.5F, -10);
    
        score.Value = (int) position.y;
    }

    void FixedUpdate() 
    {
        var delta = Time.deltaTime * new Vector2(GetVelocityX(), BaseDrivingSpeed.Value); 
        position.Value += delta;
        _rigidbody.MovePosition(position.Value);
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

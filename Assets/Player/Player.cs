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
    public float DrivingSpeed = 13;

    public PlayerCrashedEvent playerCrashedEvent;

    public IntegerValue score;

    public float steeringSpeed = 1;

    public SteeringDirection Direction { get; set; }

    private Vector2 _position;

    private Rigidbody2D _rigidbody;

    void Start()
    {
        Direction = SteeringDirection.Straight;
        _rigidbody = GetComponent<Rigidbody2D>();
        _position = _rigidbody.position;
    }

    void Update()
    {
        var position = transform.position;
        Camera.main.transform.position = new Vector3(0, position.y + 2.5F, -10);
    
        score.Value = (int) position.y;
    }

    void FixedUpdate() 
    {
        var delta = Time.deltaTime * new Vector2(GetVelocityX(), DrivingSpeed); 
        _position += delta;
        _rigidbody.MovePosition(_position);
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
            SteeringDirection.Left => -steeringSpeed,
            SteeringDirection.Right => steeringSpeed,
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

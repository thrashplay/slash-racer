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
    private static readonly int FIXED_UPDATES_PER_SECOND = 50;

    public PlayerConfig config;

    public PlayerCrashedEvent playerCrashedEvent;

    public Vector2Value position;

    public IntegerValue score;

    public SteeringDirection Direction { get; set; }

    private int _currentRotation = 0;

    private float _currentSpeed;

    private float _rawScore;

    private Rigidbody2D _rigidbody;

    void Start()
    {
        _currentSpeed = config.BaseSpeed;
  
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
        UpdateRotation();
        UpdateVelocity();
        UpdateScore();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _currentRotation = 0;
        } 
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // destroy any obstacles the player crashes into
            Destroy(collision.gameObject);

            // handle the crash            
            OnCrashed();
        }
    }

    private void OnCrashed()
    {
        playerCrashedEvent.Emit(this);
        Destroy(gameObject);
    }

    private void UpdateRotation()
    {
        switch (Direction)
        {
            case SteeringDirection.Left:
                _currentRotation -= config.SteeringSpeed;
                break;

            case SteeringDirection.Right:
                _currentRotation += config.SteeringSpeed;
                break;

            default:
                SteerTowardsStraight();
                break;
        }

        if (_currentRotation < -config.SteeringLimit)
        {
            _currentRotation = -config.SteeringLimit;
        }
        else if (_currentRotation > config.SteeringLimit)
        {
            _currentRotation = config.SteeringLimit;
        }

        _rigidbody.transform.rotation = Quaternion.identity;
        _rigidbody.transform.Rotate(Vector3.back, _currentRotation);
    }

    private void SteerTowardsStraight()
    {
        if (_currentRotation < 0)
        {
            _currentRotation = Mathf.Min(0, _currentRotation + config.SteeringSpeed);
        }
        else if (_currentRotation > 0)
        {
            _currentRotation = Mathf.Max(0, _currentRotation - config.SteeringSpeed);
        }
    }

    private void UpdateVelocity()
    {
        _currentSpeed = IsAccelerating
            ? Mathf.Min(config.MaxSpeed, _currentSpeed + config.Acceleration)
            : config.BaseSpeed;

        var rotation = _rigidbody.rotation * Mathf.Deg2Rad;
        var x = -Mathf.Sin(rotation) * _currentSpeed;
        var y = Mathf.Cos(rotation) * _currentSpeed;

        _rigidbody.velocity = FIXED_UPDATES_PER_SECOND * Time.deltaTime * new Vector2(x, y);
    }

    private void UpdateScore()
    {
        _rawScore += IsAccelerating ? 8 : 1;
        var increment = Mathf.FloorToInt(_rawScore / 25);
        _rawScore -= 25 * increment;
        score.Value += increment;
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
        Direction = SteeringDirection.Left;
    }

    public void SteerRight()
    {
        Direction = SteeringDirection.Right;
    }

    public void SteerStraight()
    {
        Direction = SteeringDirection.Straight;
    }
}

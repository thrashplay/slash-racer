using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayerController
{
    private static readonly int FIXED_UPDATES_PER_SECOND = 50;

    public PlayerConfig config;

    public GameConfig gameConfig;

    public PlayerCrashedEvent playerCrashedEvent;

    public Vector2Value position;

    public IntegerValue score;

    private int _currentRotation = 0;

    private float _currentSpeed;

    private float _rawScore;

    private Rigidbody2D _rigidbody;

    private SoundManager _soundManager;

    void Start()
    {
        _currentSpeed = config.BaseSpeed;
  
        _rigidbody = GetComponent<Rigidbody2D>();
        position.Value = _rigidbody.position;

        _soundManager = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
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
        if 
        (
            collision.gameObject.CompareTag("Obstacle") || 
            (gameConfig.WallsAreFatal && collision.gameObject.CompareTag("Wall"))
        )
        {
            playerCrashedEvent.Emit(this);
        } 
    }

    // adjusts the player speed to keep forward movement constant, if enabled
    private float CalculateAdjustedSpeed(float baseSpeed, float rotation)
    {
        if (!gameConfig.KeepForwardSpeedConstant)
        {
            return baseSpeed;
        }

        return baseSpeed / Mathf.Cos(rotation);
    }

    private void UpdateRotation()
    {
        _rigidbody.transform.rotation = Quaternion.identity;
        _rigidbody.transform.Rotate(Vector3.back, _currentRotation);
    }

    private void UpdateVelocity()
    {
        _currentSpeed = IsAccelerating
            ? Mathf.Min(config.MaxSpeed, _currentSpeed + config.Acceleration)
            : config.BaseSpeed;

        _soundManager.PlayEngine(IsAccelerating ? EngineSound.High : EngineSound.Low);

        var rotation = _rigidbody.rotation * Mathf.Deg2Rad;
        var adjustedSpeed = CalculateAdjustedSpeed(_currentSpeed, rotation);

        var x = -Mathf.Sin(rotation) * adjustedSpeed;
        var y = Mathf.Cos(rotation) * adjustedSpeed;

        _rigidbody.velocity = FIXED_UPDATES_PER_SECOND * Time.deltaTime * new Vector2(x, y);
    }

    private void UpdateScore()
    {
        if (gameConfig.ScoreFromMovementEnabled)
        {
            _rawScore += IsAccelerating ? 8 : 1;
            var increment = Mathf.FloorToInt(_rawScore / 25);
            _rawScore -= 25 * increment;
            score.Value += increment;
        }
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
        SteerTo(_currentRotation - config.SteeringSpeed);
    }

    public void SteerRight()
    {
        SteerTo(_currentRotation + config.SteeringSpeed);
    }

    public void SteerStraight()
    {
        if (gameConfig.SteeringAutoStraighten)
        {
            if (_currentRotation < 0)
            {
                SteerTo(Mathf.Min(0, _currentRotation + config.SteeringSpeed));
            }
            else if (_currentRotation > 0)
            {
                SteerTo(Mathf.Max(0, _currentRotation - config.SteeringSpeed));
            }
        }
    }

    public int SteerTo(int angle)
    {
        _currentRotation = angle;
        
        if (_currentRotation < -config.SteeringLimit)
        {
            _currentRotation = -config.SteeringLimit;
        }
        else if (_currentRotation > config.SteeringLimit)
        {
            _currentRotation = config.SteeringLimit;
        }

        return _currentRotation;
    }
}

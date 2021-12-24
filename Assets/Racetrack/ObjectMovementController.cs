using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left,
    Right
}

public class ObjectMovementController : MonoBehaviour
{
    // minimum time, in seconds, between collisions with a wall
    private static readonly float COLLISION_COOLDOWN = 0.1F;

    public PowerUpConfig config;

    // game time in seconds before which collisions are ignored
    private float _ignoreCollisionsUntil = 0;

    private Direction _direction;

    private Rigidbody2D  _rigidbody;

    private int _rotationSpeed;

    private float _speed;

    void Start()
    {
        _direction = Random.Range(0, 2) == 0 ? Direction.Left : Direction.Right;
        _rotationSpeed = (int) Random.Range(config.MinRotation, config.MaxRotation);
        _speed = Random.Range(config.MinSpeed, config.MaxSpeed);

        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = GetVelocity();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.realtimeSinceStartup < _ignoreCollisionsUntil)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            _direction = _direction == Direction.Left ? Direction.Right : Direction.Left;
            _rigidbody.velocity = GetVelocity();

            _ignoreCollisionsUntil = Time.realtimeSinceStartup + COLLISION_COOLDOWN;
        }
    }

    private Vector2 GetVelocity()
    {
        var sign = _direction == Direction.Left ? -1 : 1;
        return new Vector2(_speed * sign, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public abstract class Movement : MonoBehaviour
{
    [SerializeField] private Transform _feetPosition;
    public float CurrentSpeed = 0f;

    private float _maxSpeed = 15f;
    private float _speed = 1.5f;
    private float _runSpeed = 4.0f;
    private float _jumpForce = 3.5f;

    private bool _touchingGround = true;
    private float _jumpCastRadius = 0.2f;
    private float _jumpCastOffset = 0.1f;
    private float _jumpCastDistance = 0.05f;

    private Rigidbody _rigidbody;

    public float Speed
    {
        get { return _speed; }
        protected set
        {
            if (value < 0) _speed = 0;
            else if (value > _maxSpeed) _speed = _maxSpeed;
            else _speed = value;
        }
    }

    public float RunSpeed => _runSpeed;

    protected void MovePosition(Vector3 velocity)
    {
        if (_touchingGround)
        {
            _rigidbody.velocity = new Vector3(velocity.x, _rigidbody.velocity.y, velocity.z);
        }
    }

    protected void Jump()
    {
        if (_touchingGround)
        {
            _rigidbody.AddForce(new Vector3(0f, _jumpForce, 0f), ForceMode.Impulse);
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
        CurrentSpeed = _rigidbody.velocity.magnitude;

        _touchingGround = IsTouchingGround();
    }

    private bool IsTouchingGround()
    {
        float offset = _jumpCastRadius + _jumpCastOffset;
        float maxDistance = _jumpCastRadius + _jumpCastOffset + _jumpCastDistance;

        if (Physics.SphereCast(_feetPosition.position + Vector3.up * offset, _jumpCastRadius, Vector3.down, out RaycastHit hit, maxDistance))
        {
            return true;
        }

        return false;
    }
}

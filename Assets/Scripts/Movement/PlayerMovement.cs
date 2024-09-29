using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Movement
{
    private Vector2 _moveDir;
    private bool _isRunning = false;
    private bool _isJumping = false;

    override protected void Update()
    {
        base.Update();
        Move();
        Jump();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            return;
        }
        else if (context.performed)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            _moveDir = direction;
        }
        else if (context.canceled)
        {
            _moveDir = Vector2.zero;
            MovePosition(Vector3.zero);
        }
    }

    public void OnRunInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            return;
        }
        else if (context.performed)
        {
            float value = context.ReadValue<float>();
            _isRunning = value >= 0.5f;
        }
        else if (context.canceled)
        {
            _isRunning = false;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            return;
        }
        else if (context.performed)
        {
            float value = context.ReadValue<float>();
            _isJumping = value >= 0.5f;
        }
        else if (context.canceled)
        {
            return;
        }
    }

    private void Move()
    {
        if (_moveDir == Vector2.zero)
        {
            return;
        }

        float speedMultiplier = (_isRunning ? RunSpeed : Speed);
        MovePosition(new Vector3(_moveDir.x * speedMultiplier, 0f, _moveDir.y * speedMultiplier));
    }

    private new void Jump()
    {
        if (!_isJumping)
        {
            return;
        }

        base.Jump();

        _isJumping = false;
    }
}

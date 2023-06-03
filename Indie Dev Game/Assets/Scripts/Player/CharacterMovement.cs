using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [Header("General Movement")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _groundedDistance = 2f;

    [Header("Jumping")]
    [SerializeField] private float _jumpSpeed = 7f;
    [SerializeField] private int _totalJumps = 2;

    [Header("Dashing")]
    [SerializeField] private int _totalDashes = 1;
    [SerializeField] private float _startDashTime = 0.1f;
    [SerializeField] private float _dashDistance = 2f;
    [SerializeField] private float _invulnerableTime = 0.5f;

    [Header("References")]
    [SerializeField] private Rigidbody2D _rb2d;

    private InputActions _inputActions;
    private float _movementInput;

    private float _dashTime;
    private int _dashesLeft;
    private int _jumpsLeft;

    private void Awake()
    {
        if (_rb2d != null) 
            { _rb2d = GetComponent<Rigidbody2D>(); }

        _inputActions = new InputActions();
    }

    private void Start()
    {
        SetUpInput();

        InitVariables();
    }

    private void SetUpInput()
    {
        _inputActions.Enable();

        _inputActions.Player.Jump.performed += _ => Jump();
        _inputActions.Player.Dash.performed += _ => Dash();
    }

    private void InitVariables()
    {
        _dashTime = _startDashTime;
        _dashesLeft = _totalDashes;
        _jumpSpeed = _totalJumps;
    }

    private void Update()
    {
        Movement();

        if (IsGrounded())
        {

        }
    }

    private void Movement()
    {
        _movementInput = _inputActions.Player.Movement.ReadValue<float>();
        Vector2 currentPosition = transform.position;
        currentPosition.x += _movementInput * _moveSpeed * Time.deltaTime;
        transform.position = currentPosition;
    }

    private bool IsGrounded()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down * _groundedDistance))
        {
            return true;
        } else
        {
            return false;
        }
    }

    private void Jump()
    {

    }

    private void Dash()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Transform))]

public class Hunter : MonoBehaviour, IHunter
{

    [InspectorName("Gun")]
    [SerializeField]
    private Gun _gun;

    [SerializeField]
    [InspectorName("Speed")]
    private float _walkSpeed;

    private HorizontalState _horizontalState;
    private VerticalState _verticalState;
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private float _horizontalWalkTime, _horizontalWalkCooldown;
    private float _verticalWalkTime, _verticalWalkCooldown;

    private void Awake()
    {
        _horizontalState = HorizontalState.Idle;
        _verticalState = VerticalState.Idle;
        _horizontalWalkTime = _verticalWalkTime = 2;
        _horizontalWalkCooldown = _verticalWalkCooldown = 0.2f;
    }

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var vector = Vector2.zero;

        switch (_horizontalState)
        {
            case HorizontalState.Left:
                vector += Vector2.left;
                break;
            case HorizontalState.Right:
                vector += Vector2.right;
                break;
        }

        switch (_verticalState)
        {
            case VerticalState.Down:
                vector += Vector2.down;
                break;
            case VerticalState.Up:
                vector += Vector2.up;
                break;
        }

        _rigidbody.velocity = vector * _walkSpeed * Time.fixedDeltaTime;
        _horizontalWalkTime -= Time.deltaTime;
        _verticalWalkTime -= Time.deltaTime;

        if (_horizontalWalkTime <= 0)
            IdleHorizontal();

        if (_verticalWalkTime <= 0)
            IdleVertical();

    }

    public void MoveRight()
    {
        if (_horizontalState != HorizontalState.Right)
        {
            _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
            _horizontalState = HorizontalState.Right;
        }
        _horizontalWalkTime = _horizontalWalkCooldown;
    }

    public void MoveLeft()
    {
        if (_horizontalState != HorizontalState.Left)
        {
            _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
            _horizontalState = HorizontalState.Left;
        }
        _horizontalWalkTime = _horizontalWalkCooldown;
    }

    public void MoveUp()
    {
        if (_verticalState != VerticalState.Up)
        {
            _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
            _verticalState = VerticalState.Up;
        }
        _verticalWalkTime = _verticalWalkCooldown;
    }

    public void MoveDown()
    {
        if (_verticalState != VerticalState.Down)
        {
            _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
            _verticalState = VerticalState.Down;
        }
        _verticalWalkTime = _verticalWalkCooldown;
    }

    private void IdleHorizontal()
    {
        _horizontalState = HorizontalState.Idle;
    }

    private void IdleVertical()
    {
        _verticalState = VerticalState.Idle;
    }

    public void Shoot()
    {
        _gun.Shoot();
    }
}

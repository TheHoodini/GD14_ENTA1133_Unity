using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
   public enum Direction
   {
       North,
       East,
       South,
       West
    }

    private Dictionary<Direction, int> _rotationByDirection = new()
    {
        { Direction.North, 0 },
        { Direction.East, 90 },
        { Direction.South, 180 },
        { Direction.West, 270 }
    };
    private Direction _facingDirection;
    private bool _isRotating = false;

    [SerializeField] private float rotationTime = 0.5f;
    private float _rotationTimer = 0f;
    private Quaternion _previusRotation;

    public void Setup()
    {
        Direction[] directions = { Direction.North, Direction.East, Direction.South, Direction.West };

        _facingDirection = directions[Random.Range(0, directions.Length)];

        SetFacingDirection();
    }

    private void SetFacingDirection()
    {
        Vector3 facing = transform.rotation.eulerAngles;
        facing.y = _rotationByDirection[_facingDirection];
        transform.rotation = Quaternion.Euler(facing);
    }

    private void TurnLeft()
    {
        switch (_facingDirection)
        {
            case Direction.North:
                _facingDirection = Direction.West;
                break;
            case Direction.East:
                _facingDirection = Direction.North;
                break;
            case Direction.South:
                _facingDirection = Direction.East;
                break;
            case Direction.West:
                _facingDirection = Direction.South;
                break;
        }
        StartRotating();
    }

    private void TurnRight()
    {
        switch (_facingDirection)
        {
            case Direction.North:
                _facingDirection = Direction.East;
                break;
            case Direction.East:
                _facingDirection = Direction.South;
                break;
            case Direction.South:
                _facingDirection = Direction.West;
                break;
            case Direction.West:
                _facingDirection = Direction.North;
                break;
        }
        StartRotating();
    }

    public Vector2 Move;
    private void MoveInput(Vector2 newMoveDirection)
    {
        Move = newMoveDirection;
        Debug.Log($"Move Input: {Move.x}{Move.y}");

        bool rotateLeft = Move.x < 0;
        bool rotateRight = Move.x > 0;

        if (rotateLeft && !rotateRight && !_isRotating)
        {
            TurnLeft();
        }
        else if (rotateRight && !rotateLeft && !_isRotating)
        {
            TurnRight();
        }
    }
    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    private void StartRotating()
    {
        _previusRotation = transform.rotation;
        _isRotating = true;
    }
    private void Update()
    {
        if (_isRotating)
        {
            Quaternion currentRotation = Quaternion.Slerp(
                _previusRotation, 
                Quaternion.Euler(new Vector3(0, _rotationByDirection[_facingDirection])), 
                _rotationTimer / rotationTime);

            transform.rotation = currentRotation;

            _rotationTimer += Time.deltaTime;

            if (_rotationTimer > rotationTime)
            {
                _isRotating = false;
                _rotationTimer = 0f;
                SetFacingDirection();
            }
        }
    }
}

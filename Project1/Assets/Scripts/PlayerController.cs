using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // ------------------------------------- ROTATION -------------------------------------
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


    // ------------------------------------- MOVEMENT -------------------------------------
    private MapManager _map;
    private int _roomX;
    private int _roomZ;

    [SerializeField] private float moveTime = 0.4f;
    private bool _isMoving = false;
    private float _moveTimer = 0f;
    private Vector3 _startMovePos;
    private Vector3 _targetMovePos;

    private float bumpDistance = 0.3f;
    private bool _isBumping = false;
    
    private RoomBase _currentRoom;
    public RoomBase CurrentRoom => _currentRoom;

    // ------------------------------------- UI ----------------------------------------
    private UIManager uiManager;

    // ------------------------------------- ROOMS -------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        RoomBase room = other.GetComponent<RoomBase>();
        if (room != null)
        {
            _currentRoom = room;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        RoomBase room = other.GetComponent<RoomBase>();
        if (room != null && room == _currentRoom)
        {
            _currentRoom = null;
        }
    }
    // -------------------------------------------------------------------------------------
    public void SetStartingRoom(RoomBase room, MapManager map)
    {
        _map = map;
        _roomX = room.X;
        _roomZ = room.Z;

        transform.position = new Vector3(room.transform.position.x, 0, room.transform.position.z);
    }
    public void Setup(UIManager useUIManager)
    {
        uiManager = useUIManager;

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
    private void MoveForward()
    {
        if (_isMoving || _isRotating || _isBumping) return;

        int targetX = _roomX;
        int targetZ = _roomZ;

        switch (_facingDirection)
        {
            case Direction.North: targetZ += 1; break;
            case Direction.East: targetX += 1; break;
            case Direction.South: targetZ -= 1; break;
            case Direction.West: targetX -= 1; break;
        }

        // null room = bump
        if (targetX < 0 || targetX >= _map.MapSize ||
            targetZ < 0 || targetZ >= _map.MapSize ||
            _map.MapRooms[targetX, targetZ] == null)
        {
            StartCoroutine(WallBump());
            return;
        }

        // move to next rooom
        _isMoving = true;
        _moveTimer = 0f;

        _startMovePos = transform.position;
        _targetMovePos = new Vector3(
            targetX * _map.RoomSize,
            0,
            targetZ * _map.RoomSize
        );

        _roomX = targetX;
        _roomZ = targetZ;
    }

    private System.Collections.IEnumerator WallBump()
    {
        _isBumping = true;

        Vector3 start = transform.position;
        Vector3 end = start + transform.forward * bumpDistance;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 4f;
            transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 4f;
            transform.position = Vector3.Lerp(end, start, t);
            yield return null;
        }

        _isBumping = false;
    }

    public Vector2 Move;
    private void MoveInput(Vector2 newMoveDirection)
    {
        Move = newMoveDirection;
        // Debug.Log($"Move Input: {Move.x}{Move.y}");

        bool rotateLeft = Move.x < 0;
        bool rotateRight = Move.x > 0;

        bool moveForward = Move.y > 0;

        bool _notDoingAnyMovement = !_isRotating && !_isMoving && !_isBumping;
        if (rotateLeft && _notDoingAnyMovement)
        {
            TurnLeft();
        }
        else if (rotateRight && _notDoingAnyMovement)
        {
            TurnRight();
        }
        else if (moveForward && _notDoingAnyMovement)
        {
            MoveForward();
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

    public void OnInteract(InputValue value)
    {
        bool interactPressed = value.Get<float>() > 0;

        if (interactPressed && _currentRoom != null)
        {
            string roomDesc = _currentRoom.OnRoomSearch();
            uiManager.UpdateRoomDescription(roomDesc);
        }
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

        if (_isMoving)
        {
            _moveTimer += Time.deltaTime;
            float t = _moveTimer / moveTime;

            transform.position = Vector3.Lerp(_startMovePos, _targetMovePos, t);

            if (t >= 1f)
                _isMoving = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerPlatformmerStateMachine : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private CircleCollider2D _collider;

    PlayerBaseState _currentState;
    public PlayerBaseState CurrentState {
        get => _currentState;
        set => _currentState = value;
    }
    PlayerStateFactory _states;

    private bool _isJumpPressed;
    public bool IsJumpPressed => _isJumpPressed;

    [Header("Movement")]
    [SerializeField] private Vector2 _movementInput;
    public Vector2 MovementInput => _movementInput;
    private bool _isMovement;
    public bool IsMovement => _isMovement;

    private bool _isRunPressed;
    public bool IsRunning => _isRunPressed;

    private Vector2 _horizontalVelocity;
    private Vector2 _horizontalVelocitySmooth;
    public Vector2 HorizontalVelocity {
        get => _horizontalVelocity;
        set => _horizontalVelocity = value;
    }
    private Vector2 _verticalVelocity;
    public Vector2 VerticalVelocity {
        get => _verticalVelocity;
        set => _verticalVelocity = value;
    }
    private Vector2 _movementDirection;
    private Vector2 _newPosition;
    private Vector2 _newPositionPure;

    [SerializeField] private float _walkSpeed;
    public float WalkSpeed => _walkSpeed;

    [SerializeField] private float _runMultiplier;
    public float RunMultiplier => _runMultiplier;

    [Header("Jump")]
    [SerializeField] private float _jumpSpeed;
    public float JumpSpeed => _jumpSpeed;

    [SerializeField] private float _gravity;
    public float Gravity => _gravity;

    [SerializeField] private float _hoverSpeed;
    public float HoverSpeed => _hoverSpeed;
    [SerializeField] private float _hoverGravityMultiplier;
    public float HoverGravityMultiplier => _hoverGravityMultiplier;
    [SerializeField] private float _fallGravityMultiplier;
    public float FallGravityMultiplier => _fallGravityMultiplier;

    private float _dt;
    public float Dt => _dt;

    [Header("Collision")]
    public int collisionPointCount;
    public float skinWidth;

    private RaycastHit2D[] hit;

    [SerializeField] private bool _isGrounded;
    public bool IsGrounded => _isGrounded;
    private Vector2 _groundNormal;
    public Vector2 GroundNormal => _groundNormal;

    public float maxSlopeAngle;

    void Awake()
    {
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();
        hit = new RaycastHit2D[8];
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        _movementInput = Input.GetAxisRaw("Horizontal") * Vector2.right;
        _isMovement = (_movementInput != Vector2.zero);
        _isRunPressed = Input.GetKey(KeyCode.LeftShift);
        _isJumpPressed = Input.GetKey(KeyCode.Space);
        _currentState.UpdateStates();
    }

    void FixedUpdate()
    {
        _dt = Time.fixedDeltaTime;
        _currentState.FixedUpdateStates();
        
        _horizontalVelocitySmooth = Mathf.Lerp(_horizontalVelocity.magnitude, _horizontalVelocitySmooth.magnitude, Mathf.Pow(2f, -_dt * 30f)) * _horizontalVelocity.normalized;
        _movementDirection = (_horizontalVelocitySmooth + _verticalVelocity) * Time.fixedDeltaTime;
        collisionPointCount = _rigidbody.Cast(_movementDirection.normalized, hit, _movementDirection.magnitude * _dt * 2);
        _newPosition = _newPositionPure = _rigidbody.position + _movementDirection;
        int j = 0;
        Vector2 normalBefore = Vector2.up;
        for (int i = 0; i < collisionPointCount; i++) {
            var h = hit[i];
            if (h.collider.isTrigger) {
                continue;
            }
            if (Vector2.Dot(h.normal, _movementDirection) > 0) continue;
            // the collision point is below the skin
            Vector2 a = h.point + h.normal * (_collider.radius - skinWidth);
            Vector2 b = _newPosition - a;
            Vector2 d = ProjectOnPlane(b, h.normal);
            

            if (Vector2.Angle(Vector2.up, h.normal) <= maxSlopeAngle) {
                j++;
                _isGrounded = true;
                _groundNormal = h.normal;
            } else if (_isGrounded){
                d = Vector2.Dot(b, _groundNormal) * _groundNormal;
            }

            if (h.normal.y < 0) {
                _verticalVelocity += Vector2.up * (-5);
                d += h.normal * _dt;
            }

            _newPosition = a + d;
        }
        if (j == 0 || _verticalVelocity.y > 0) {
            _isGrounded = false;
            _groundNormal = Vector2.up;
        }
        
        
        _rigidbody.MovePosition(_newPosition);
    }

    public Vector2 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)
    {
        float num = Vector2.Dot(planeNormal, planeNormal);
        if (num < Mathf.Epsilon)
        {
            return vector;
        }

        float num2 = Vector2.Dot(vector, planeNormal);
        return new Vector2(vector.x - planeNormal.x * num2 / num, vector.y - planeNormal.y * num2 / num);
    }
}

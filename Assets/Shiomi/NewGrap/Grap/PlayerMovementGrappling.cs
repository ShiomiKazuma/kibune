using UnityEngine;
public class PlayerMovementGrappling : MonoBehaviour
{
    // true = paused
    bool _pauseOverride = false;

    //移動スピード
    float _moveSpeed;
    public float _walkSpeed;
    public float _sprintSpeed;
    public float _swingSpeed;
    public float _groundDrag;

    //ジャンプ
    public float _jumpForce;
    public float _jumpCooldown;
    public float _airMultiplier;
    bool readyToJump;

    //しゃがみ処理
    public float _crouchSpeed;
    public float _crouchYScale;
    float _startYScale;

    //入力コード
    public KeyCode _jumpKey = KeyCode.Space;
    public KeyCode _sprintKey = KeyCode.LeftShift;
    public KeyCode _crouchKey = KeyCode.LeftControl;

    //接地判定
    public float _playerHeight;
    public LayerMask _layerMask;
    bool _isGrounded;
    public bool IsGrounded => _isGrounded;

    //傾斜判定
    public float _maxSlopeAngle;
    RaycastHit slopeHit;
    bool exitingSlope;

    //カメラ
    public PlayerCam _cam;
    public float _grappleFov = 95f;

    public Transform _orientation;
    float _horizontalInput;
    float _verticalInput;
    Vector3 _moveDirection;
    Rigidbody _rb;
    public MovementState _state;

    PauseManager _pMan;

    public enum MovementState
    {
        freeze,
        grappling,
        swinging,
        walking,
        sprinting,
        crouching,
        air
    }

    public bool _freeze;
    public bool _activeGrapple;
    public bool _swinging;

    void MyInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        //ジャンプインプット
        if (Input.GetKey(_jumpKey) && readyToJump && _isGrounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), _jumpCooldown);
        }
    }

    void StateHandler()
    {
        //Freeze
        if (_freeze)
        {
            _state = MovementState.freeze;
            _moveSpeed = 0;
            _rb.velocity = Vector3.zero;
        }
        //Grappling
        else if (_activeGrapple)
        {
            _state = MovementState.grappling;
            _moveSpeed = _sprintSpeed;
        }
        //Swinging
        else if (_swinging)
        {
            _state = MovementState.swinging;
            _moveSpeed = _swingSpeed;
        }
        //Sprinting
        else if (_isGrounded && Input.GetKey(_sprintKey))
        {
            _state = MovementState.sprinting;
            _moveSpeed = _sprintSpeed;
        }
        //Walking
        else if (_isGrounded)
        {
            _state = MovementState.walking;
            _moveSpeed = _walkSpeed;
        }
        //Air
        else
        {
            _state = MovementState.air;
        }
    }

    void MovePlayer()
    {
        if (_activeGrapple) return;
        if (_swinging) return;
        //移動方向
        _moveDirection = _orientation.forward * _verticalInput + _orientation.right * _horizontalInput;

        //傾斜
        if (OnSlope() && !exitingSlope)
        {
            _rb.AddForce(GetSlopeMoveDirection() * _moveSpeed * 20f, ForceMode.Force);
            if (_rb.velocity.y > 0)
                _rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }
        //地面
        else if (_isGrounded)
            _rb.AddForce(_moveDirection.normalized * _moveSpeed * 10f, ForceMode.Force);
        //空中
        else if (!_isGrounded)
            _rb.AddForce(_moveDirection.normalized * _moveSpeed * 10f * _airMultiplier, ForceMode.Force);
        //傾斜の間、重力を無くす
        _rb.useGravity = !OnSlope();
    }

    void SpeedControl()
    {
        if (_activeGrapple) return;
        //傾斜でのスピードを制限する
        if (OnSlope() && !exitingSlope)
        {
            if (_rb.velocity.magnitude > _moveSpeed)
                _rb.velocity = _rb.velocity.normalized * _moveSpeed;
        }
        //地面と空中でのスピード制限
        else
        {
            Vector3 flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
            if (flatVel.magnitude > _moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * _moveSpeed;
                _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
            }
        }
    }

    void Jump()
    {
        exitingSlope = true;
        //yのvelocityをリセットする
        _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
        _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    private bool enableMovementOnNextTouch;
    private Vector3 velocityToSet;
    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        _activeGrapple = true;

        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);

        Invoke(nameof(ResetRestrictions), 3f);
    }
    private void SetVelocity()
    {
        enableMovementOnNextTouch = true;
        _rb.velocity = velocityToSet;

        _cam.DoFov(_grappleFov);
    }

    public void ResetRestrictions()
    {
        _activeGrapple = false;
        _cam.DoFov(85f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enableMovementOnNextTouch)
        {
            enableMovementOnNextTouch = false;
            ResetRestrictions();

            GetComponent<Grappling>().StopGrapple();
        }
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, _playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < _maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(_moveDirection, slopeHit.normal).normalized;
    }

    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
            + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        if (displacementY < 0f)
        {
            velocityY = Vector3.zero;
            velocityXZ = velocityXZ * 2.0f;
        }

        return velocityXZ + velocityY;
    }

    #region Text & Debugging

    //public TextMeshProUGUI text_speed;
    //public TextMeshProUGUI text_mode;
    //private void TextStuff()
    //{
    //    Vector3 flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

    //    if (OnSlope())
    //        text_speed.SetText("Speed: " + Round(_rb.velocity.magnitude, 1) + " / " + Round(_moveSpeed, 1));

    //    else
    //        text_speed.SetText("Speed: " + Round(flatVel.magnitude, 1) + " / " + Round(_moveSpeed, 1));

    //    text_mode.SetText(_state.ToString());
    //}

    //public static float Round(float value, int digits)
    //{
    //    float mult = Mathf.Pow(10.0f, (float)digits);
    //    return Mathf.Round(value * mult) / mult;
    //}

    #endregion

    void OnPaused()
    {
        _pauseOverride = true;
    }

    void OnEndPaused()
    {
        _pauseOverride = false;
    }

    private void Awake()
    {
        _pMan = GameObject.FindObjectOfType<PauseManager>();
    }

    private void OnEnable()
    {
        _pMan.BeginPause += OnPaused;
        _pMan.EndPause += OnEndPaused;
    }

    private void OnDisable()
    {
        _pMan.BeginPause -= OnPaused;
        _pMan.EndPause -= OnEndPaused;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        readyToJump = true;

        _startYScale = transform.localScale.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_pauseOverride) return;

        //向きを設定
        this.transform.rotation = _orientation.rotation;
        //接地判定
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _layerMask);
        MyInput();
        SpeedControl();
        StateHandler();
        //抵抗を設定
        if (_isGrounded && !_activeGrapple)
            _rb.drag = _groundDrag;
        else
            _rb.drag = 0;
        MovePlayer();
        //TextStuff();
    }

}

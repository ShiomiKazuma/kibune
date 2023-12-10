using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(CharacterController))]
public class CharacterMove : MonoBehaviour
{
    const float _normalFov = 60f;
    const float _hookshotFov = 100f;
    [SerializeField] float _mouseSensitivity = 1f;
    [SerializeField] float _moveSpeed = 10f;
    [SerializeField] float _jumpPower = 20f;
    [SerializeField] float _gravityDownForce = -60f;
    [SerializeField] Transform _hitPointTransform;
    [SerializeField] Transform _hookshotTransform;
    [SerializeField] ParticleSystem _hookparticleSystem;
    [SerializeField] GameObject _aim;
    [SerializeField] float _grapDis = 30f;
    Image _aimImage;
    CharacterController _characterController;
    float _cameraVerticalAngle;
    float _characterVelocityY;
    Camera _playerCamera;
    State _state;
    Vector3 _hookShotPos;
    Vector3 _characterVecMomentum;
    float _hockshotSize;
    CameraFov _cameraFov;
    enum State
    {
        Normal,
        HookshotFlying,
        HookshotThrown,

    }
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController> ();
        _playerCamera = transform.Find("Camera").GetComponent<Camera>();
        _cameraFov = _playerCamera.GetComponent<CameraFov> ();
        Cursor.lockState = CursorLockMode.Locked;
        _state = State.Normal;
        _aimImage = _aim.GetComponent<Image>();
        _hookshotTransform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch(_state)
        {
            case State.Normal:
                HandleCharacterLook();
                HandleCaracterMovement();
                HandleHookshotStart();
                break;
            case State.HookshotThrown:
                HookshotThrow();
                HandleCharacterLook();
                HandleCaracterMovement();
                break;
            case State.HookshotFlying:
                HandleCharacterLook();
                HandHookshotMovement();
                break;
        }

        //エイムカーソルを変化させる
        RaycastHit hitAim;
        if (Physics.Raycast(this.transform.position, _playerCamera.transform.forward, out hitAim, _grapDis))
        {
            _aimImage.color = Color.black;
        }
        else
        {
            _aimImage.color = Color.red;
        }

    }

    void HandleCharacterLook()
    {
        //マウスの移動
        float lookX = Input.GetAxisRaw("Mouse X");
        float lookY = Input.GetAxisRaw("Mouse Y");
        //横方向のカメラの調整
        transform.Rotate(new Vector3(0f, lookX * _mouseSensitivity, 0f), Space.Self);
        //縦方向のカメラの調整
        _cameraVerticalAngle -= lookY * _mouseSensitivity;
        _cameraVerticalAngle = Mathf.Clamp(_cameraVerticalAngle, -89f, 89f);
        _playerCamera.transform.localEulerAngles = new Vector3(_cameraVerticalAngle, 0, 0);
    }

    void HandleCaracterMovement()
    {
        //方向の入力
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 characterVelocity = transform.right * moveX * _moveSpeed + transform.forward * moveZ * _moveSpeed;

        if(_characterController.isGrounded)
        {
            _characterVelocityY = 0f;

            if(InputJump())
            {
                _characterVelocityY = _jumpPower;
            }
        }

        _characterVelocityY += _gravityDownForce * Time.deltaTime;
        characterVelocity.y = _characterVelocityY;
        characterVelocity += _characterVecMomentum;
        _characterController.Move(characterVelocity * Time.deltaTime);

        if(_characterVecMomentum.magnitude >= 0f)
        {
            float momentumDrag = 3f;
            _characterVecMomentum -= _characterVecMomentum * momentumDrag * Time.deltaTime;
            if(_characterVecMomentum.magnitude < .0f)
            {
                _characterVecMomentum = Vector3.zero;
            }
        }
    }
    
    void ResetGravity()
    {
        _characterVelocityY = 0f;
    }
    //グラップリング始めの処理
    void HandleHookshotStart()
    {
        if(InputDownHookshot())
        {
            if(Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out RaycastHit raycastHit, _grapDis))
            {
                //ヒットした場合の処理
                _hitPointTransform.forward = _playerCamera.transform.forward; //フックポイントを正面にする
                _hitPointTransform.position = raycastHit.point; //フックショットを移動させる
                _hookShotPos = raycastHit.point;
                _hockshotSize = 0f;
                _hookshotTransform.gameObject.SetActive(true);
                _hookshotTransform.localScale = Vector3.zero;
                _state = State.HookshotThrown;
            }
        }
    }

    void HookshotThrow()
    {
        _hookshotTransform.LookAt(_hookShotPos);

        float hockshotThrowSpeed = 70f;
        _hockshotSize += hockshotThrowSpeed * Time.deltaTime;
        _hookshotTransform.localScale = new Vector3(1, 1, _hockshotSize);

        if(_hockshotSize >= Vector3.Distance(transform.position, _hookShotPos))
        {
            _state = State.HookshotFlying;
            _cameraFov.SetCameraFov(_hookshotFov);
            _hookparticleSystem.Play();
        }
    }

    void HandHookshotMovement()
    {
        _hookshotTransform.LookAt(_hookShotPos);
        Vector3 hookDir = (_hookShotPos - transform.position).normalized;
        float hookshotSpeedMax = 40f;
        float hookshotSpeedMin = 10f;
        float hookshotSpeed = Mathf.Clamp(Vector3.Distance(transform.position, _hookShotPos), hookshotSpeedMin, hookshotSpeedMax);
        float hookshotMultiplier = 2f; //フックショットの加速度
        _characterController.Move(hookDir * hookshotSpeed * hookshotMultiplier * Time.deltaTime);

        if(Vector3.Distance(transform.position, _hookShotPos) < 1f)
        {
            StopHookshot();
        }

        if(InputDownHookshot())
        {
            StopHookshot();
        }

        if(InputJump())
        {
            //ジャンプキャンセル
            float momentumExtraSpeed = 7f;
            _characterVecMomentum = hookDir * hookshotSpeed * momentumExtraSpeed;
            _characterVecMomentum += Vector3.up * _jumpPower;
            StopHookshot();
        }
    }

    void StopHookshot()
    {
        //キャンセル
        _state = State.Normal;
        ResetGravity();
        _hookshotTransform.gameObject.SetActive(false);
        _cameraFov.SetCameraFov(_normalFov);
        _hookparticleSystem.Stop();
    }
    bool InputDownHookshot()
    {
        return Input.GetMouseButtonDown(0);
    }

    bool InputJump()
    {
        return Input.GetButtonDown("Jump");
    }
}

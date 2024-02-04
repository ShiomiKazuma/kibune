using UnityEngine;
using DG.Tweening;

public class PlayerCam : MonoBehaviour
{
    public float _sensX;
    public float _sensY;
    public Transform _orientation;
    float _xRotation;
    float _yRotation;

    bool _pauseOverride;
    PauseManager _pMan;

    public Vector3 Forward => this.gameObject.transform.forward;

    public void DoFov(float endValue)
    {
        GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
    }

    public void DoTilt(float zTilt)
    {
        transform.DOLocalRotate(new Vector3(0, 0, zTilt), 0.25f);
    }

    void OnPaused()
    {
        _pauseOverride = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnEndPaused()
    {
        _pauseOverride = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Awake()
    {
        _pMan = GameObject.FindFirstObjectByType<PauseManager>();
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

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (_pauseOverride) { return; }

        //マウスのインプット
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * _sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * _sensY;

        _yRotation += mouseX;
        _xRotation -= mouseY;

        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        //カメラのrotateを変更する
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        _orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
    }
}

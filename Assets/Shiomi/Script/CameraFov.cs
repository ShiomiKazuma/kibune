using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFov : MonoBehaviour
{
    Camera _playerCamera;
    float _targetFov;
    float _fov;

    private void Awake()
    {
        _playerCamera = GetComponent<Camera>();
        _targetFov = _playerCamera.fieldOfView;
        _fov = _targetFov;
    }

    // Update is called once per frame
    void Update()
    {
        float fovSpeed = 4f;
        _fov = Mathf.Lerp(_fov, _targetFov, Time.deltaTime * fovSpeed);
        _playerCamera.fieldOfView = _fov;
    }

    public void SetCameraFov(float targetFov)
    {
        this._targetFov = targetFov;
    }
}

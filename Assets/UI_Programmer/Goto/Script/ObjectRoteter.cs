using UnityEngine;

public class UIObjRotator : MonoBehaviour
{
    /// <summary>ターゲットのオブジェクト</summary>
    [SerializeField] GameObject _targetObject;
    /// <summary>回転のスピード</summary>
    [SerializeField] Vector2 _rotationSpeed = new Vector2(0.1f, 0.2f);
    /// <summary>動きの反転のフラグ</summary>
    [SerializeField] bool _reverse;

    Camera _mainCamera;
    Vector2 _lastMousePosition;
    /// <summary>初期角度</summary>
    Quaternion _initialRotation;

    void Start()
    {
        _mainCamera = Camera.main;
        _initialRotation = _targetObject.transform.rotation;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // 左クリックを押している間マウスドラッグで回転
        if (Input.GetMouseButtonDown(0))
        {
            _lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            if (!_reverse)
            {
                var x = (Input.mousePosition.y - _lastMousePosition.y);
                var y = (_lastMousePosition.x - Input.mousePosition.x);

                var newAngle = Vector3.zero;
                newAngle.x = x * _rotationSpeed.x;
                newAngle.y = y * _rotationSpeed.y;

                _targetObject.transform.Rotate(newAngle);
                _lastMousePosition = Input.mousePosition;
            }
            else
            {
                var x = (_lastMousePosition.y - Input.mousePosition.y);
                var y = (Input.mousePosition.x - _lastMousePosition.x);

                var newAngle = Vector3.zero;
                newAngle.x = x * _rotationSpeed.x;
                newAngle.y = y * _rotationSpeed.y;

                _targetObject.transform.Rotate(newAngle);
                _lastMousePosition = Input.mousePosition;
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            // 右クリックで角度のリセット
            _targetObject.transform.rotation = _initialRotation;
        }
    }
}
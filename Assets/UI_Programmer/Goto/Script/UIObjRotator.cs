using UnityEngine;

public class UIObjRotator : MonoBehaviour
{
    /// <summary>回転させるオブジェクト</summary>
    [SerializeField, Header("回転させるオブジェクト")] GameObject _targetObject;
    /// <summary>回転のスピード</summary>
    [SerializeField, Header("回転のスピード\nX：縦方向　Y：横方向")] Vector2 _rotationSpeed = new Vector2(0.1f, 0.2f);
    /// <summary>動きの反転のフラグ</summary>
    [SerializeField, Header("動きの反転のフラグ")] bool _reverse;

    Camera _mainCamera;
    Vector2 _lastMousePosition;
    /// <summary>初期角度</summary>
    Quaternion _initialRotation;
    bool _canRotation = false;

    void Start()
    {
        _mainCamera = Camera.main;
        _initialRotation = _targetObject.transform.rotation;
    }

    private void Update()
    {
        if (_canRotation)
        {
            if (Input.GetMouseButtonDown(1))
            {
                // 右クリックで角度のリセット
                _targetObject.transform.rotation = _initialRotation;
                Debug.Log("reset");
            }
            // 左クリックを押している間マウスドラッグで回転
            else if (Input.GetMouseButtonDown(0))
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
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _canRotation = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _canRotation = false;
    }
}

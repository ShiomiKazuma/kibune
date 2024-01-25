using UnityEngine;

public class UIObjRotator : MonoBehaviour
{
    /// <summary>��]������I�u�W�F�N�g</summary>
    [SerializeField, Header("��]������I�u�W�F�N�g")] GameObject _targetObject;
    /// <summary>��]�̃X�s�[�h</summary>
    [SerializeField, Header("��]�̃X�s�[�h\nX�F�c�����@Y�F������")] Vector2 _rotationSpeed = new Vector2(0.1f, 0.2f);
    /// <summary>�����̔��]�̃t���O</summary>
    [SerializeField, Header("�����̔��]�̃t���O")] bool _reverse;

    Camera _mainCamera;
    Vector2 _lastMousePosition;
    /// <summary>�����p�x</summary>
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
                // �E�N���b�N�Ŋp�x�̃��Z�b�g
                _targetObject.transform.rotation = _initialRotation;
                Debug.Log("reset");
            }
            // ���N���b�N�������Ă���ԃ}�E�X�h���b�O�ŉ�]
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

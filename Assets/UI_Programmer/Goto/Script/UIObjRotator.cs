using UnityEngine;

public class UIObjRotator : MonoBehaviour
{
    /// <summary>��]������I�u�W�F�N�g</summary>
    [SerializeField, Header("��]������؋��i")] GameObject[] _targetObjects;
    /// <summary>��]�̃X�s�[�h</summary>
    [SerializeField, Header("��]�̃X�s�[�h\nX�F�c�����@Y�F������")] Vector2 _rotationSpeed = new Vector2(0.1f, 0.2f);
    /// <summary>�����̔��]�̃t���O</summary>
    [SerializeField, Header("�����̔��]�̃t���O")] bool _reverse;
    [SerializeField, Header("�`�ʂɎg���J����")]
    Camera _mainCamera;

    GameObject _currentTargetObject;
    Vector2 _lastMousePosition;
    /// <summary>�����p�x</summary>
    Quaternion _initialRotation;
    bool _canRotation = false;

    void Start()
    {
        foreach (var obj in _targetObjects)
        {
            _initialRotation = obj.transform.rotation;
            MeshRenderer[] meshRenderer = obj.GetComponentsInChildren<MeshRenderer>();
            foreach (var item in meshRenderer)
            {
                item.enabled = false; 
            }
        }
    }

    public void SetTarget(int id)
    {
        foreach (var obj in _targetObjects)
        {
            MeshRenderer[] meshRenderer = obj.GetComponentsInChildren<MeshRenderer>();
            foreach (var item in meshRenderer)
            {
                item.enabled = false; 
            }
        }

        _currentTargetObject = _targetObjects[id];
        _currentTargetObject.transform.rotation = _initialRotation;
        MeshRenderer[] targetMeshRenderer = _currentTargetObject.GetComponentsInChildren<MeshRenderer>();
        foreach (var item in targetMeshRenderer)
        {
            item.enabled = true; 
        }
    }

    private void Update()
    {
        if (_canRotation && _currentTargetObject != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                // �E�N���b�N�Ŋp�x�̃��Z�b�g
                _currentTargetObject.transform.rotation = _initialRotation;
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

                    _currentTargetObject.transform.Rotate(newAngle);
                    _lastMousePosition = Input.mousePosition;
                }
                else
                {
                    var x = (_lastMousePosition.y - Input.mousePosition.y);
                    var y = (Input.mousePosition.x - _lastMousePosition.x);

                    var newAngle = Vector3.zero;
                    newAngle.x = x * _rotationSpeed.x;
                    newAngle.y = y * _rotationSpeed.y;

                    _currentTargetObject.transform.Rotate(newAngle);
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

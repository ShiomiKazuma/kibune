using UnityEngine;

public class Instantiate : MonoBehaviour
{
    [SerializeField, Tooltip("��������I�u�W�F�N�g")] GameObject[] _objects = default;
    [SerializeField, Tooltip("�������鐔")] int _max = 10;
    [Tooltip("���b�����ɐ������邩")] float _interval = 1f;
    [Header("�����̃C���^�[�o��")]
    [SerializeField, Tooltip("�C���^�[�o����min")] float _minInterval = 0.4f;
    [SerializeField, Tooltip("�C���^�[�o����max")] float _maxInterval = 2f;
    float _timer = 0f;
    [Tooltip("���݉��I�u�W�F�N�g�𐶐�������")] int _count = 0;
    void Start()
    {
        _timer = 0f;
        _count = 0;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_count < _max)
        {
            if (_timer > _interval)
            {
                //Instantiate(_objects);
                RandomInstantiate();
                _count++;
                _timer = 0f;
                _interval = Random.Range(_minInterval, _maxInterval);
            }
        }
    }

    void RandomInstantiate()
    {
        var index = Random.Range(0, _objects.Length);
        Instantiate(_objects[index], gameObject.transform);
    }
}

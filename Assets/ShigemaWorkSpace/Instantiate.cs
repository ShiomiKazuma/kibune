using UnityEngine;

public class Instantiate : MonoBehaviour
{
    [SerializeField, Tooltip("生成するオブジェクト")] GameObject[] _objects = default;
    [SerializeField, Tooltip("生成する数")] int _max = 10;
    [Tooltip("何秒おきに生成するか")] float _interval = 1f;
    [Header("生成のインターバル")]
    [SerializeField, Tooltip("インターバルのmin")] float _minInterval = 0.4f;
    [SerializeField, Tooltip("インターバルのmax")] float _maxInterval = 2f;
    float _timer = 0f;
    [Tooltip("現在何個オブジェクトを生成したか")] int _count = 0;
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

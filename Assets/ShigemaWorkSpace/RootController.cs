using UnityEngine;
using UnityEngine.Splines;
/// <summary>
/// 特徴：SplineAnimateと異なり開始位置や進む方向などを細かく設定できる
/// </summary>
public class RootController : MonoBehaviour
{
    //[SerializeField, Tooltip("動かす対象")] GameObject _npc;
    [SerializeField, Tooltip("Splineを持つオブジェクト")] SplineContainer _container;
    [SerializeField, Tooltip("Splineとのオフセット")] Vector3 _offsetFromPath;
    [Tooltip("通る経路")] SplinePath _path;
    [SerializeField, Tooltip("0に近いほど始点、1に近いほど終点に位置される")] float _t = 0f;
    [SerializeField, Tooltip("速度")] [Range(0, 1)] float _speed = 0.1f;
    void Start()
    {
        var container1Transform = _container.transform.localToWorldMatrix;
        // 配列にして、何パターンか作ればランダム移動にみえるかも
        _path = new SplinePath(new[]
        {
                new SplineSlice<Spline>(_container.Splines[0], new SplineRange(0, 5), container1Transform), //↑
                new SplineSlice<Spline>(_container.Splines[16], new SplineRange(0, 3), container1Transform), //↑→
                new SplineSlice<Spline>(_container.Splines[14], new SplineRange(1, 2), container1Transform), //→
                new SplineSlice<Spline>(_container.Splines[17], new SplineRange(0, 2), container1Transform), //→↑
                new SplineSlice<Spline>(_container.Splines[2], new SplineRange(5, 2), container1Transform), //↑
                new SplineSlice<Spline>(_container.Splines[18], new SplineRange(0, 2), container1Transform), //↑→
                new SplineSlice<Spline>(_container.Splines[12], new SplineRange(3, 2), container1Transform),
                new SplineSlice<Spline>(_container.Splines[19], new SplineRange(0, 2), container1Transform),
                new SplineSlice<Spline>(_container.Splines[4], new SplineRange(7, 6), container1Transform),
            });
    }

    void Update()
    {
        Vector3 pos = _path.EvaluatePosition(_t);
        transform.position = pos + _offsetFromPath;

        var direction = _path.EvaluateTangent(_t);
        var floatPos = _path.EvaluatePosition(_t);
        transform.LookAt(floatPos + direction);

        _t += _speed * Time.deltaTime;
        if (_t > 1f) _t = 0f;
    }
}

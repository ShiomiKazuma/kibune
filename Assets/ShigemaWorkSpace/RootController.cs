using UnityEngine;
using UnityEngine.Splines;
/// <summary>
/// �����FSplineAnimate�ƈقȂ�J�n�ʒu��i�ޕ����Ȃǂ��ׂ����ݒ�ł���
/// </summary>
public class RootController : MonoBehaviour
{
    //[SerializeField, Tooltip("�������Ώ�")] GameObject _npc;
    [SerializeField, Tooltip("Spline�����I�u�W�F�N�g")] SplineContainer _container;
    [SerializeField, Tooltip("Spline�Ƃ̃I�t�Z�b�g")] Vector3 _offsetFromPath;
    [Tooltip("�ʂ�o�H")] SplinePath _path;
    [SerializeField, Tooltip("0�ɋ߂��قǎn�_�A1�ɋ߂��قǏI�_�Ɉʒu�����")] float _t = 0f;
    [SerializeField, Tooltip("���x")] [Range(0, 1)] float _speed = 0.1f;
    void Start()
    {
        var container1Transform = _container.transform.localToWorldMatrix;
        // �z��ɂ��āA���p�^�[�������΃����_���ړ��ɂ݂��邩��
        _path = new SplinePath(new[]
        {
                new SplineSlice<Spline>(_container.Splines[0], new SplineRange(0, 5), container1Transform), //��
                new SplineSlice<Spline>(_container.Splines[16], new SplineRange(0, 3), container1Transform), //����
                new SplineSlice<Spline>(_container.Splines[14], new SplineRange(1, 2), container1Transform), //��
                new SplineSlice<Spline>(_container.Splines[17], new SplineRange(0, 2), container1Transform), //����
                new SplineSlice<Spline>(_container.Splines[2], new SplineRange(5, 2), container1Transform), //��
                new SplineSlice<Spline>(_container.Splines[18], new SplineRange(0, 2), container1Transform), //����
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

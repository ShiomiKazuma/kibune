// �쐬�ҁA�S���F����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLib.Tweening;
using UnityEditor;
/// <summary> SNS�̃|�b�v�A�b�v�̂悤�ȃC�x���g�̔��΂����Ԍo�߂Ń����_���ɂ��� </summary>
#region README
// ���̃R���|�[�l���g�͗L���̊Ԏ��ԃJ�E���g������̂ŁA���Ăق����Ȃ��ꍇ�ɂ͖��������邱��
#endregion
[RequireComponent(typeof(UITween))]
public class SNSPopUpEvent : MonoBehaviour
{
    [SerializeField,
        Header("SNS�̃|�b�v�A�b�v�ʒm������Ԋu")]
    float _eventPopInterval;

    UITween _uiTween;

    float _elapsedTime;

    bool _isCountingTimeForDestoy;

    public void OnPopUpEventEnd()
    {
        _isCountingTimeForDestoy = true;
    }

    public void CallStartTween()
    {
        _uiTween.StartTween();
    }

    private void Start()
    {
        _uiTween = GetComponent<UITween>();
    }

    private void FixedUpdate()
    {
        if (_elapsedTime > _eventPopInterval)
        {
            CallStartTween();
        }
        else
        {
            _elapsedTime += Time.deltaTime;
        }
        if (_isCountingTimeForDestoy)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime > _eventPopInterval * 2)
                Destroy(gameObject);
        }
    }
}
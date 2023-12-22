// �Ǘ��� ����
using System;
using UnityEngine;
using SLib;
/*
* ����̃C���X�^���X�̃v���p�e�B�l�v�[���N���X
* �̃v���p�e�B���Ď�����N���X
*/
/// <summary> ���W�X�^�ƃf�[�^�̃y�A����Ȃ�f�[�^�\���ŁA
/// ���W�X�^���ɉ������f�[�^�����߂Ă����f�[�^�x�[�X </summary>
public class PropertyInfoHandler : MonoBehaviour
{
    DataDictionary<string, object> _dataMap = new();
    public DataDictionary<string, object> DataMap => _dataMap;
    public event Action OnPropResisted = () => {Debug.Log("�v���p�e�B�����W�X�g���ꂽ"); };
    public event Action OnPropUnResisted = () => { Debug.Log("�v���p�e�B���A�����W�X�g���ꂽ"); };
    /// <summary> ���W�X�^���ƃf�[�^�̓o�^ </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="resistName"></param>
    /// <param name="value"></param>
    public void Resist<T>(string resistName, T value)
    {
        _dataMap.Add(resistName, value);
        if (OnPropResisted != null) { OnPropResisted(); }
        else { throw new Exception("Assing Some Function!"); }
    }
    /// <summary> ���W�X�^�����w�肵�āA ���̃��W�X�^���ɂ������o�^�����f�[�^�x�[�X�������</summary>
    /// <param name="resistedName"></param>
    public void UnResist(string resistedName)
    {
        _dataMap.Remove(resistedName, _dataMap[resistedName]);
        if (OnPropUnResisted != null) { OnPropUnResisted(); }
        else { throw new Exception("Assing Some Function!"); }
    }
    /// <summary> �w�肵�����W�X�^���ɑΉ������f�[�^�y�A���f�[�^�x�[�X�ɑ��݂��邩�������� </summary>
    /// <param name="resistName"></param>
    /// <returns></returns>
    public bool GetExist(string resistName)
    {
        return _dataMap[resistName] != null;
    }
}
// �Ǘ��� ����
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/// <summary> �v���p�e�B���C���t���̗��p���̃N���X���p������ׂ��C���^�[�t�F�C�X </summary>
public interface IPropInfoUser
{
    PropertyInfoHandlerLinker PropInfoHandlerLinker { get; set; }
    PropertyInfoHandler PropInfoHandler { get; set; }
    List<string> ResisterNameList { get; set; }
}
/// <summary> �v���p�e�B���C���t�����p���̊��N���X�B�@�\�𗘗p���邽�߂ɂ�����p������ </summary>
[RequireComponent(typeof(PropertyInfoHandler))]
public abstract class PropInfoUser : MonoBehaviour
{
    protected abstract void SetUpPropInfoUser();
}
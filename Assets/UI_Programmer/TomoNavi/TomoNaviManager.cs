using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// �쐬 ����
public class TomoNaviManager : MonoBehaviour
{
    [SerializeField, Header("�|�b�v������F�i�r�̃I�u�W�F�N�g�������ɃA�^�b�`")] List<GameObject> _objects;

    public void PopNavi(int index)
    {
        GameObject go = GameObject.Instantiate(_objects[index]);
        go.transform.parent = transform;
        go.transform.localPosition = Vector3.zero;
    }
}

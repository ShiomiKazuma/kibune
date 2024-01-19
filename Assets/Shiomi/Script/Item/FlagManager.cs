using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    [SerializeField, Header("�A�C�e����\������t���O")] public bool[] IsItemFlag;
    [SerializeField, Header("�A�C�e���{�b�N�X�̃Q�[���I�u�W�F�N�g")] GameObject[] _items;

    private void Start()
    {
        //�A�C�e���t���O�̏�����
        for(int i = 0; i < _items.Length; i++)
        {
            _items[i].SetActive(IsItemFlag[i]);
        }
    }
    /// <summary>
    /// �A�C�e���t���O��true
    /// </summary>
    /// <param name="Id"> �A�C�e��ID </param>
    void AddItem(int Id)
    {
        IsItemFlag[Id] = true;
        _items[Id].SetActive(true);  
    }

    /// <summary>
    /// �A�C�e���̃I���I�t���X�V����
    /// </summary>
    public void UpdateItem()
    {
        //�A�C�e���t���O�̍X�V
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i].SetActive(IsItemFlag[i]);
        }
    }
}

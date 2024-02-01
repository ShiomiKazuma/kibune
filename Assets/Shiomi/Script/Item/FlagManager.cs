using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    [SerializeField, Header("�A�C�e����\������t���O")]
    List<bool> IsItemActive;
    [SerializeField, Header("�A�C�e���{�b�N�X�̃Q�[���I�u�W�F�N�g")]
    List<GameObject> _items;

    public List<bool> Progress => IsItemActive;

    /// <summary> �ێ�����Ă���i�����㏑�� </summary>
    /// <param name="progress"></param>
    public void OverwriteProgress(List<bool> progress)
    {
        IsItemActive.Clear();

        foreach (var item in progress)
        {
            IsItemActive.Add(item);
        }

        UpdateActiveItem();
    }

    /// <summary>
    /// �C���x���g���֗L���ȃA�C�e����ǉ��i�C���f�b�N�X�w��j
    /// </summary>
    /// <param name="Index"> �A�C�e��ID </param>
    public void AddActiveItem(int Index)
    {
        IsItemActive[Index] = true;
        _items[Index].SetActive(true);

        UpdateActiveItem();
    }

    /// <summary>
    /// �A�C�e���̃I���I�t���X�V����
    /// </summary>
    void UpdateActiveItem()
    {
        //�A�C�e���t���O�̍X�V
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].SetActive(IsItemActive[i]);
        }
    }

    private void Start()
    {
        //�A�C�e���t���O�̏�����
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].SetActive(IsItemActive[i]);
        }
    }

}

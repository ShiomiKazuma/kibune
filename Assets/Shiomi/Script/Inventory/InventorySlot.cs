using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shiomi
{
    public class InventorySlot : MonoBehaviour
    {
        //[Header("�A�C�e���̂̃A�C�R����\��������Image")] public Image _icon;
        //Item _item;
        //[Header("�폜�{�^��")] public Button _removeButton;
        [SerializeField, Header("�A�C�e���̐�����\��������TextBox")] Text _textBox;
        [SerializeField, Header("�A�C�e���̐�����")] string _text;
        [SerializeField, Header("�A�C�e����ID")] int _itemId;
        //3d�I�u�W�F�N�g�ƃe�L�X�g�̕\��
        public void SetFlavourText()
        {
            _textBox.text = _text;
        }

        public void ToFront()
        {
            //�q�G�����L�[��̈�ԉ��Ɉړ����āA�O�ʂɕ\�������
            transform.SetAsLastSibling();
        }

        //public void AddActiveItem(Item newItem)
        //{
        //    _item = newItem;

        //    _icon.sprite = _item._icon;
        //    _icon.enabled = true;
        //    _removeButton.interactable = true;
        //}

        //public void ClearSlot()
        //{
        //    _item = null;
        //    _icon.sprite = null;
        //    _icon.enabled = false;
        //    _removeButton.interactable = false;
        //}

        //public void RemoveItem()
        //{
        //    Inventory._instance.Remove(_item);
        //}

        //public void UseItem()
        //{
        //    if(_item != null)
        //    {
        //        _item.Use();
        //    }
        //}


    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image _icon;
    Item _item;
    public Button _removeButton;
    [SerializeField] Text _textBox;

    public void AddItem(Item newItem)
    {
        _item = newItem;

        _icon.sprite = _item._icon;
        _icon.enabled = true;
        _removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        _item = null;
        _icon.sprite = null;
        _icon.enabled = false;
        _removeButton.interactable = false;
    }

    public void RemoveItem()
    {
        Inventory._instance.Remove(_item);
    }

    public void UseItem()
    {
        if(_item != null)
        {
            _item.Use();
        }
    }

    //3dオブジェクトとテキストの表示
    public void Hyouzi()
    {
        _textBox.text = _item._setumeibunn;
    }

    public void Hierarchy()
    {
        //ヒエラルキー上の一番下に移動して、前面に表示される
        transform.SetAsLastSibling();
    }
}

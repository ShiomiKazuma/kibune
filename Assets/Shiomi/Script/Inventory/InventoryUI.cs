using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform _itemParent;
    Inventory _inventory;
    InventorySlot[] _inventorySlots;
    public GameObject _inventoryUI;
    // Start is called before the first frame update
    void Start()
    {
        _inventory = Inventory._instance;
        _inventory.onItemChangedCallback += UpdateUI;

        _inventorySlots = _itemParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            _inventoryUI.SetActive(!_inventoryUI.activeSelf);
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            if(i < _inventory._itemList.Count)
            {
                _inventorySlots[i].AddItem(_inventory._itemList[i]);
            }
            else
            {
                _inventorySlots[i].ClearSlot();
            }
        }
    }
}

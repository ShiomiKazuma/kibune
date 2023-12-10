using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiInventory : MonoBehaviour
{
    Inventory _inventory;
    Transform _itemSlotContainer;
    Transform _itemSlotTemplate;
    [SerializeField] float _itemSlotCellSize = 30f;

    private void Awake()
    {
        _itemSlotContainer = transform.Find("itemSlotContainer");
        _itemSlotTemplate = _itemSlotContainer.Find("itemSlotTemplate");
    }
    public void SetInventory(Inventory inventory)
    {
        this._inventory = inventory;
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        int x = 0;
        int y = 0;
        
        //foreach(Item item in _inventory.GetItemsList())
        //{
        //    RectTransform itemSlotRectTransform = Instantiate(_itemSlotTemplate, _itemSlotContainer).GetComponent<RectTransform>();
        //    itemSlotRectTransform.gameObject.SetActive(true);
        //    itemSlotRectTransform.anchoredPosition = new Vector2(x * _itemSlotCellSize, y * _itemSlotCellSize);
        //    Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
        //    //image.sprite = item.GetSprite();
        //    x++;
        //    if(x > 4)
        //    {
        //        x = 0;
        //        y++;
        //    }
        //}
    }
}

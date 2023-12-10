using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory _instance;

    private void Awake()
    {
        if(_instance != null)
        {
            return;
        }
        _instance = this; 
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public int space = 20;

    public List<Item> _itemList = new List<Item>();

    public bool Add(Item item)
    {
        if(!item._isDefaultItem)
        {
            if(_itemList.Count >= space)
            {
                return false;
            }
            _itemList.Add(item);

            if(onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
        }

        return true;
    }

    public void Remove(Item item)
    {
        _itemList.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}

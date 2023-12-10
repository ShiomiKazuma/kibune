using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string _name = "New Item";
    public Sprite _icon = null;
    public string _setumeibunn;
    public bool _isDefaultItem;

    public virtual void Use()
    {
        //ƒAƒCƒeƒ€‚ÌŒø‰Ê
        Debug.Log("Using" + _name);
    }
}

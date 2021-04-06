using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    
    public Sprite icon = null;
    public bool isDefaultItem = false;
    /*public Item(save_Item new_item)
    {
        name = new_item.name;
        icon = new_item.icon;
        isDefaultItem = new_item.isDefaultItem;
    }*/
}
/*
[System.Serializable]
public class save_Item
{
    new public string name = "New Item";
    [System.NonSerialized]
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public save_Item(Item old_item)
    {
        name = old_item.name;
        icon = old_item.icon;
        isDefaultItem = old_item.isDefaultItem;
    }
}*/

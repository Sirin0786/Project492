using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class InventoryUI : MonoBehaviour
{

    public Transform itemsParent;
    Inventory_Code inventory;
    //InventorySlot[] slots;

    void Start()
    {
        inventory = Inventory_Code.instance;
        //inventory.OnItemChangedCallback += UpdateUI;

        //Debug.Log(Inventory_Code.instance.slots.ToString());
        inventory.slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update ()
    {
        for (int i = 0;i< inventory.slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                inventory.slots[i].AddItem(inventory.items[i]);
            }else
            {
                inventory.slots[i].ClearSlot();
            }
        }

    }

}

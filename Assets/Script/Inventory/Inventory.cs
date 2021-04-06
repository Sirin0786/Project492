using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Inventory : MonoBehaviour
{
    public static bool Invent = false;
    
    public GameObject InventoryUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (Invent)
            {
                CloseInvent();
            }
            else
            {
                OpenInvent();
            }
        }
    }

    public void CloseInvent()
    {
        InventoryUI.SetActive(false);
        Time.timeScale = 1f;
        Invent = false;
    }

    void OpenInvent()
    {
        InventoryUI.SetActive(true);
        Time.timeScale = 0f;
        Invent = true;
    }
}

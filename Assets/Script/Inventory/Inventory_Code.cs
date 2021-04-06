using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Inventory_Code : MonoBehaviour
{
    #region  Singleton
    public static Inventory_Code instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance");
            //Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion

    public delegate void OnItemChanged();

    public OnItemChanged OnItemChangedCallback;


    public int space = 8;
    public List<Item> items = new List<Item>();
    public InventorySlot[] slots;
    public Dictionary <string, Item> dic = new Dictionary<string, Item>();

    public bool check_flash = false;
    public bool check_rabbit = false;
    public bool check_myDiary = false;
    public bool check_momDiary = false;

    public bool check_end = false;
    public bool check_kill = false;

    public bool check_unlock01 = false;

    public bool End1 = false;
    public bool End2 = false;
    public bool End3 = false;
    public bool End4 = false;

    
    public bool Add (Item item)
    {
        if(!item.isDefaultItem)
        {
            if(items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }
            items.Add(item);

            if(OnItemChangedCallback != null)
                OnItemChangedCallback.Invoke();
        }
        return true;
    }

    public void Remove (Item item)
    {
        items.Remove(item);

        if(OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
    }

    

}

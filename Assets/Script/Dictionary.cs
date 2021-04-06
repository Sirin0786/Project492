using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dictionary : MonoBehaviour
{
    public Item[] i;
 
    public void Start()
    {
        foreach(Item A in i)
        {
            if(!Inventory_Code.instance.dic.ContainsKey(A.name))
            {
                Inventory_Code.instance.dic.Add(A.name,A);
            }
        }
    }
  
}

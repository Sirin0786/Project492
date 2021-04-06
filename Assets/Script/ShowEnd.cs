using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEnd : MonoBehaviour
{
    public GameObject E1;
    public GameObject E2;
    public GameObject E3;
    public GameObject E4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Inventory_Code.instance.End1){
            E1.SetActive(true);
        }
        if(Inventory_Code.instance.End2){
            E2.SetActive(true);
        }
        if(Inventory_Code.instance.End3){
            E3.SetActive(true);
        }
        if(Inventory_Code.instance.End4){
            E4.SetActive(true);
        }
    }
}

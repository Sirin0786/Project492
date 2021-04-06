using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Debug.Log("GameIsSave");
        PlayerPrefs.SetInt ("Level", SceneManager.GetActiveScene ().buildIndex);
        List<string> sitems = new List<string>();
        foreach(Item A in Inventory_Code.instance.items)
        {
            sitems.Add(A.name);
        }
        SaveData data = new SaveData();
        /*if(SceneManager.GetActiveScene().name == "Ending"){
            data.End2 = Inventory_Code.instance.End2;
            data.End3 = Inventory_Code.instance.End3;
            data.End4 = Inventory_Code.instance.End4;
        }else if(SceneManager.GetActiveScene().name == "RoomSophia" && Inventory_Code.instance.check_end){
            data.End1 = Inventory_Code.instance.End1;
        }*/
        data.items = sitems;
        data.check_end = Inventory_Code.instance.check_end;
        data.check_flash = Inventory_Code.instance.check_flash;
        data.check_kill = Inventory_Code.instance.check_kill;
        data.check_momDiary = Inventory_Code.instance.check_momDiary;
        data.check_myDiary = Inventory_Code.instance.check_myDiary;
        data.check_rabbit = Inventory_Code.instance.check_rabbit;
        data.check_unlock01 = Inventory_Code.instance.check_unlock01;
        
        data.End2 = Inventory_Code.instance.End2;
        data.End3 = Inventory_Code.instance.End3;
        data.End4 = Inventory_Code.instance.End4;
        data.End1 = Inventory_Code.instance.End1;

        SerializationManager.Save("test_save", data);
    }
}

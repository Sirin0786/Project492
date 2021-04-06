using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject UIend;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        SaveData data = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/savestest_save.save");
        Inventory_Code.instance.End1 = data.End1;
        Inventory_Code.instance.End2 = data.End2;
        Inventory_Code.instance.End3 = data.End3;
        Inventory_Code.instance.End4 = data.End4;
    }

    public void StartScene()
    {
        audioSource.Play(0);
        SceneManager.LoadScene("StartGame");
        SaveData data = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/savestest_save.save");
        List<Item> items = new List<Item>();
        Inventory_Code.instance.items = items;
        Inventory_Code.instance.check_end = false;
        Inventory_Code.instance.check_flash = false;
        Inventory_Code.instance.check_kill = false;
        Inventory_Code.instance.check_momDiary = false;
        Inventory_Code.instance.check_myDiary = false;
        Inventory_Code.instance.check_rabbit = false;
        Inventory_Code.instance.check_unlock01 = false;
        Inventory_Code.instance.End1 = data.End1;
        Inventory_Code.instance.End2 = data.End2;
        Inventory_Code.instance.End3 = data.End3;
        Inventory_Code.instance.End4 = data.End4;
    }
    
    public void Ending()
    {
        audioSource.Play();
        UIend.SetActive(true);
    }

    public void Close()
    {
        audioSource.Play();
        UIend.SetActive(false);
    }

    public void LoadScene()
    {
        audioSource.Play(0);
        SceneManager.LoadScene ( PlayerPrefs.GetInt("Level") );
        SaveData data = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/savestest_save.save");
        List<Item> items = new List<Item>();
        foreach(string A in data.items)
        {
            items.Add(Inventory_Code.instance.dic[A]);
        }
        Inventory_Code.instance.items = items;
        Inventory_Code.instance.check_end = data.check_end;
        Inventory_Code.instance.check_flash = data.check_flash;
        Inventory_Code.instance.check_kill = data.check_kill;
        Inventory_Code.instance.check_momDiary = data.check_momDiary;
        Inventory_Code.instance.check_myDiary = data.check_myDiary;
        Inventory_Code.instance.check_rabbit = data.check_rabbit;
        Inventory_Code.instance.check_unlock01 = data.check_unlock01;
    }

    

    //เวลาออกจากเกม
    public void ExitGame()
    {
        audioSource.Play();
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                //Resume();
            }
            else
            {
                Pause();
            }
        }   
    }

    public void Resume()
    {
        audioSource.Play(0);
        pauseMenuUI.SetActive(false);
        /*PlayerPrefs.SetInt ("Level", SceneManager.GetActiveScene ().buildIndex);
        List<string> sitems = new List<string>();
        foreach(Item A in Inventory_Code.instance.items)
        {
            sitems.Add(A.name);
        }
        SaveData data = new SaveData();
        data.items = sitems;
        SerializationManager.Save("test_save", data);*/
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        audioSource.Play(0);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    
    public void QuitGame()
    {
        audioSource.Play(0);
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}

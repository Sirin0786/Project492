using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class ChoiceChoose : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject choiceUI;

    PlayableDirector choice1;
    PlayableDirector choice2;
    PlayableDirector choice3;
    PlayableDirector choice4;
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "RoomSophia"){
            choice1 = GameObject.Find("open").GetComponent<PlayableDirector>();
            choice2 = GameObject.Find("not_open").GetComponent<PlayableDirector>();
        }else if(SceneManager.GetActiveScene().name == "03"){
            choice3 = GameObject.Find("Fight").GetComponent<PlayableDirector>();
            choice4 = GameObject.Find("Escape").GetComponent<PlayableDirector>();
        }
        
    }

    void OnEnable()
    {
        Pause();
    }

    

    void Pause()
    {
        choiceUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void chose1()
    {
        choiceUI.SetActive(false);
        choice1.Play();
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void chose2()
    {
        choiceUI.SetActive(false);
        choice2.Play();
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
     public void chose3()
    {
        choiceUI.SetActive(false);
        choice3.Play();
        Inventory_Code.instance.check_kill = true;
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void chose4()
    {
        choiceUI.SetActive(false);
        choice4.Play();
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
}

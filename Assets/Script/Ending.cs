using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;

public class Ending : MonoBehaviour
{
    PlayableDirector end2;
    PlayableDirector end3;
    PlayableDirector end4;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if(Inventory_Code.instance.check_kill && Inventory_Code.instance.check_momDiary && Inventory_Code.instance.check_rabbit){
            end3 = GameObject.Find("end3").GetComponent<PlayableDirector>();
            Inventory_Code.instance.End3 = true;
            end3.Play();
        }else if(Inventory_Code.instance.check_kill){
            end2 = GameObject.Find("end2").GetComponent<PlayableDirector>();
            Inventory_Code.instance.End2 = true;
            end2.Play();
        }else if(!Inventory_Code.instance.check_kill){
            end4 = GameObject.Find("end4").GetComponent<PlayableDirector>();
            Inventory_Code.instance.End4 = true;
            end4.Play();
        }
    }


}

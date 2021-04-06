using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class IfHaveDiary : MonoBehaviour
{
    PlayableDirector have;
    PlayableDirector not_have;
    void Start()
    {
        have = GameObject.Find("Knowmom").GetComponent<PlayableDirector>();
        not_have = GameObject.Find("not_Know").GetComponent<PlayableDirector>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Inventory_Code.instance.check_momDiary && Inventory_Code.instance.check_rabbit){
            have.Play();
        }else{
            not_have.Play();
        }


    }
}

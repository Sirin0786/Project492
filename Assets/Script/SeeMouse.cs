using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeMouse : MonoBehaviour
{
    public GameObject Sophia;
    GameObject CamPas;
    public GameObject XUI;

    // Start is called before the first frame update
    void Start()
    {
        //Sophia = GameObject.Find("Sophia");
        CamPas = GameObject.Find("CameraPass");
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        XUI.SetActive(true);
        if(Input.GetKeyDown(KeyCode.X)){
            Sophia.SetActive(true);
            CamPas.SetActive(false);
            XUI.SetActive(false);
        }
    }
}

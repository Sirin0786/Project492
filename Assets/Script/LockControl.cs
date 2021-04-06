using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockControl : MonoBehaviour
{
    private int[] result, correctCombination;
    GameObject Padlock;
    GameObject CamPass;
    GameObject Sophia;
    public GameObject XUI;
    private bool isOpened;
    private void Start()
    {
        Padlock = GameObject.Find("padlock"); 
        Sophia = GameObject.Find("Sophia");
        result = new int[]{0,0,0,0};
        correctCombination = new int[] {2,3,0,8};
        isOpened = false;
        Rotate.Rotated += CheckResults;
        XUI.SetActive(false);
    }

    private void CheckResults(string wheelName, int number)
    {
        CamPass = GameObject.Find("CameraPass");
        switch (wheelName)
        {
            case "WheelOne":
                result[0] = number;
                break;

            case "WheelTwo":
                result[1] = number;
                break;

            case "WheelThree":
                result[2] = number;
                break;

            case "WheelFour":
                result[3] = number;
                break;
        }

        if (result[0] == correctCombination[0] && result[1] == correctCombination[1]
            && result[2] == correctCombination[2] && result[3] == correctCombination[3] && !isOpened)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
            Destroy(Padlock);
            Destroy(CamPass);
            XUI.SetActive(false);
            Sophia.SetActive(true);
            Inventory_Code.instance.check_unlock01 = true;
            isOpened = true;
        }
    }

    private void OnDestroy()
    {
        Rotate.Rotated -= CheckResults;
    }
}

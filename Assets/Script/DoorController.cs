using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorController : MonoBehaviour
{
    Animator Door;
    private bool _isInside = false;
    private bool _isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        Door = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_isInside)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _isOpen = !_isOpen;
                Door.SetBool("open",_isOpen);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _isInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _isInside = false;
        }
    }

    
}

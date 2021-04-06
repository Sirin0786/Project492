using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Animator am;
    private AudioSource foot;

    Vector3 velocity;
    bool isGrounded;
    int t=0;

    private void Start()
    {
        //รับค่ามาจาก animator controller
        am = GetComponent<Animator>();
        foot = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        //เช็คว่าอยู่ติดพื้นหรือไม่
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //ควบคุมตัวละคร
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //แรงโน้มถ่วง
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //ให้แสดงอนิเมชั่นเมื่อเดิน
        bool walking = x != 0f || z != 0f;
        am.SetBool("isWalking",walking);
        if(walking){
            t++;
            if(t >= 40 && foot.isPlaying == false)
            {
                foot.Play();
                t=0;
            }
        }
        
    }
}

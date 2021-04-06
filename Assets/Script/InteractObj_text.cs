using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class InteractObj_text : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] float Distance = 4.0f;
    GameObject PickupMessage;

    private float RayDistance;
    private bool CanSeePickup = false;

    Text interact_text;
    Text expand_item;

    public AudioSource A;

    PlayableDirector DoWork;
    PlayableDirector Washing;
    PlayableDirector End1;
    PlayableDirector End2;
    PlayableDirector End2_Die;

    GameObject coffee;

    bool check_report = false;
    bool check_postit = false;
    bool finish_work = false;
    bool washing = false;
    bool eat_coffee = false;
    bool sleep = false;
    GameObject PressI;
    bool X = false;
    private float time;


    void Start()
    {
        time = 3.0f;
        PressI = GameObject.Find("I");
        PressI.gameObject.SetActive(false);
        RayDistance = Distance;
        interact_text = GameObject.Find ("message").GetComponent<Text>();
        expand_item = GameObject.Find ("Expand").GetComponent<Text>();
        expand_item.gameObject.SetActive(false);
        PickupMessage = GameObject.Find("Interact");
        PickupMessage.gameObject.SetActive(false);
        coffee = GameObject.Find("Coffee");
        DoWork = GameObject.Find("DoWork").GetComponent<PlayableDirector>();
        Washing = GameObject.Find("Washing").GetComponent<PlayableDirector>();
        End1 = GameObject.Find("End1").GetComponent<PlayableDirector>();
        End2 = GameObject.Find("End2").GetComponent<PlayableDirector>();
        End2_Die = GameObject.Find("End2_Die").GetComponent<PlayableDirector>();
    }

    void Update()
    {
        if(X){
            time -= Time.deltaTime;
            if(time <= 0){
                PressI.SetActive(false);
                X = false;
            }
        }
        if(Physics.Raycast(transform.position, transform.forward, out hit, RayDistance)){
            if(hit.transform.tag == "Door"){
                CanSeePickup = true;
                interact_text.text = "เปิด/ปิด";
                if(hit.transform.name == "Lock_Demo" && Input.GetKeyDown(KeyCode.E)){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "ตอนนี้ยังไม่ใช่เวลาซักผ้า";
                }
                else if(hit.transform.name == "Exit" && Input.GetKeyDown(KeyCode.E)){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "ไม่จำเป็นต้องออกไปข้างนอกตอนนี้";
                }
                
                if(hit.transform.name == "End2_Door" && Input.GetKeyDown(KeyCode.E) && sleep){
                    End2_Die.Play();
                }
                else if(Input.GetKeyDown(KeyCode.E) && sleep && hit.transform.name != "Door2"){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "ต้องไปดูก่อนว่าปิดทีวีหรือยัง";
                }

            }
            else if(hit.transform.tag == "Coffee"){
                CanSeePickup = true;
                interact_text.text = "ดื่ม";
                if (Input.GetKeyDown(KeyCode.E) && !eat_coffee)
                {
                    if(finish_work){
                        expand_item.gameObject.SetActive(true);
                        expand_item.text = "ทำงานเสร็จแล้ว ไม่จำเป็นต้องกินกาแฟแล้ว";
                    }else{
                        eat_coffee = true;
                        Destroy(coffee);
                        expand_item.gameObject.SetActive(true);
                        expand_item.text = "กลับไปทำงานต่อดีกว่า";
                    }
                }
                else if(Input.GetKeyDown(KeyCode.E) && eat_coffee){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "กาแฟหมดแล้ว";
                }
            }
            else if(hit.transform.tag == "Item"){
                CanSeePickup = true;
                interact_text.text = "เก็บ";
                if (Input.GetKeyDown(KeyCode.E))
                {
                   Debug.Log(hit.transform.gameObject.GetComponent<Pickup>().item.name);
                   if(hit.transform.gameObject.GetComponent<Pickup>().item.name == "Report")
                   {
                       X = true;
                       PressI.SetActive(true);
                       check_report = true;
                   }
                   if(hit.transform.gameObject.GetComponent<Pickup>().item.name == "PostIt")
                   {
                       check_postit = true;
                   }

                   bool wasPickedUp = Inventory_Code.instance.Add(hit.transform.gameObject.GetComponent<Pickup>().item);
                   if (wasPickedUp)
                    Destroy(hit.transform.gameObject);
                }
            }
            else if(hit.transform.name == "Mac"){
                CanSeePickup = true;
                interact_text.text = "ทำงาน";
                if (Input.GetKeyDown(KeyCode.E) && !finish_work)
                {
                    if(!check_postit){
                        expand_item.gameObject.SetActive(true);
                        expand_item.text = "ลืมรหัสผ่านเข้าคอมไปเลยแฮะ ต้องไปหารหัสก่อน";
                    }
                    else if(!check_report){
                        expand_item.gameObject.SetActive(true);
                        expand_item.text = "ต้องไปเอาเอกสารข้อมูลของคดีที่รวบรวมไว้มาด้วย";
                    }
                    else{
                        DoWork.Play();
                        finish_work = true;
                    }
                }
                else if(Input.GetKeyDown(KeyCode.E) && finish_work){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "ทำงานเสร็จแล้ว";
                }
            }
            else if(hit.transform.name == "Wash"){
                CanSeePickup = true;
                interact_text.text = "ล้างหน้า";
                if (Input.GetKeyDown(KeyCode.E) && !washing)
                {
                    if(!finish_work){
                        expand_item.gameObject.SetActive(true);
                        expand_item.text = "ไปทำงานให้เสร็จก่อนดีกว่า";
                    }
                    else{
                        Washing.Play();
                        washing = true;
                    }
                    
                }
                else if(Input.GetKeyDown(KeyCode.E) && washing){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "ล้างหน้าไปแล้ว";
                }
            }
            else if(hit.transform.tag == "Bed"){
                CanSeePickup = true;
                interact_text.text = "นอน";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if(!finish_work){
                        expand_item.gameObject.SetActive(true);
                        expand_item.text = "ยังทำงานไม่เสร็จเลย";
                    }
                    else{
                        if(!eat_coffee){
                            End1.Play();
                            sleep = true;
                        }
                        else{
                            End2.Play();
                            A.Play();
                            sleep = true;
                        }

                    Inventory_Code.instance.items = new List<Item>();
                    }
                }
            }
            else if(hit.transform.tag == "Bear"){
                CanSeePickup = true;
                interact_text.text = "ตรวจสอบ";
                if(Input.GetKeyDown(KeyCode.E)){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "ตุ๊กตาที่เพื่อนร่วมงานให้เป็นของขวัญ";
                }
            }
            else
            {
                CanSeePickup = false;
                expand_item.gameObject.SetActive(false);
            }
            
        }

        if(CanSeePickup == true){
            PickupMessage.gameObject.SetActive(true);
            RayDistance = 1000f;
        }
        if(CanSeePickup == false){
            PickupMessage.gameObject.SetActive(false);
            RayDistance = Distance;
        }
    }
}

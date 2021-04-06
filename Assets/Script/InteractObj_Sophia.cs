using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class InteractObj_Sophia : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] float Distance = 4.0f;
    GameObject PickupMessage;
    GameObject PressF;
    GameObject Flashlight;
    GameObject Sophia;
    GameObject CamPas;
    
    private float RayDistance;
    private bool CanSeePickup = false;

    Animator Door;
    private bool _isOpen = false;
    private bool _openlight = false;

    PlayableDirector StartGame;
    PlayableDirector Neighbor_come;
    PlayableDirector ToRedDoor;
    PlayableDirector GoRedDoor;
    PlayableDirector RedInfo;
    PlayableDirector Sleep01;
    PlayableDirector Sleep02;
    PlayableDirector Founded;
    PlayableDirector end1;
    PlayableDirector know;
    PlayableDirector read;
    PlayableDirector back;
    
    Text interact_text;
    Text expand_item;
    
    bool check_flashlight = false;
    bool check_sleep1 = false;
    bool check_key01 = false;
    bool check_doll = false;
    bool know_clue1 = false;
    bool X = false;
    bool Y = false;

    private float time;
    

    void Start()
    {
        RayDistance = Distance;
        interact_text = GameObject.Find("message").GetComponent<Text>();
        expand_item = GameObject.Find("Expand").GetComponent<Text>();
        expand_item.gameObject.SetActive(false);
        PickupMessage = GameObject.Find("Interact");
        PickupMessage.gameObject.SetActive(false);
        if(SceneManager.GetActiveScene().name == "01" || SceneManager.GetActiveScene().name == "02" || SceneManager.GetActiveScene().name == "03"){
            Flashlight = GameObject.Find("Flash");
            Flashlight.gameObject.SetActive(false);
        }
        if(SceneManager.GetActiveScene().name == "RoomSophia"){
            Neighbor_come = GameObject.Find("Food").GetComponent<PlayableDirector>();
            ToRedDoor = GameObject.Find("GotoRed").GetComponent<PlayableDirector>();
            GoRedDoor = GameObject.Find("GotoRed2").GetComponent<PlayableDirector>();
            time = 3.0f;
            if(!Inventory_Code.instance.check_end){
                StartGame = GameObject.Find("Start").GetComponent<PlayableDirector>();
                StartGame.Play();
            }else{
                end1 = GameObject.Find("end1").GetComponent<PlayableDirector>();
                Inventory_Code.instance.End1 = true;
                end1.Play();
            }
        }else if(SceneManager.GetActiveScene().name == "01"){
            time = 3.0f;
            PressF = GameObject.Find("F");
            PressF.gameObject.SetActive(false);
            know = GameObject.Find("Know").GetComponent<PlayableDirector>();
            RedInfo = GameObject.Find("RedDorInfo").GetComponent<PlayableDirector>();
            Sleep01 = GameObject.Find("Sleep").GetComponent<PlayableDirector>();
            Sophia = GameObject.Find("Sophia");
            CamPas = GameObject.Find("CameraPass");
            CamPas.SetActive(false);
        }else if(SceneManager.GetActiveScene().name == "02"){
            Sleep02 = GameObject.Find("Sleep02").GetComponent<PlayableDirector>();
            Founded = GameObject.Find("Found").GetComponent<PlayableDirector>();
        }else if(SceneManager.GetActiveScene().name == "03"){
            back = GameObject.Find("Back").GetComponent<PlayableDirector>();
            Sleep01 = GameObject.Find("Sleep").GetComponent<PlayableDirector>();
            read = GameObject.Find("BirthDay").GetComponent<PlayableDirector>();
        }
        
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "RoomSophia"){
            scece1();
        }else if(SceneManager.GetActiveScene().name == "01"){
            scece2();
        }else if(SceneManager.GetActiveScene().name == "02"){
            scece3();
        }else if(SceneManager.GetActiveScene().name == "03"){
            scece4();
        }

        if(SceneManager.GetActiveScene().name == "01" || SceneManager.GetActiveScene().name == "02" || SceneManager.GetActiveScene().name == "03"){
            if(Input.GetKeyDown(KeyCode.F) && Inventory_Code.instance.check_flash){
                _openlight = !_openlight;
                Flashlight.SetActive(_openlight);
            }
        }
    }


    void scece1(){
        if(X){
            time -= Time.deltaTime;
            if(time <= 0){
                expand_item.gameObject.SetActive(false);
                X = false;
            }
        }
        if(Physics.Raycast(transform.position, transform.forward, out hit, RayDistance)){
            if(hit.transform.tag == "Door"){
                CanSeePickup = true;
                interact_text.text = "เปิด/ปิด";
                if(hit.transform.name == "Exit" && Input.GetKeyDown(KeyCode.E)){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "ไม่จำเป็นต้องออกไปข้างนอกตอนนี้";
                }else if(hit.transform.name == "BedDoor" && Input.GetKeyDown(KeyCode.E) && check_sleep1){
                    if(!check_doll){
                        expand_item.gameObject.SetActive(true);
                        expand_item.text = "หยิบตุ๊กตาไปด้วยหนึ่งตัวดีกว่า";
                    }else{
                        GoRedDoor.Play();
                    }
                }else if(hit.transform.name == "Mom_Door" && Input.GetKeyDown(KeyCode.E)){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "ตั้งแต่วันที่คุณแม่หายไป ห้องคุณแม่ก็ถูกล็อคไว้ตลอดเลย";
                }else if(hit.transform.name == "Kitchen_Door" && Input.GetKeyDown(KeyCode.E) && !Y){
                    Y = true;
                    Neighbor_come.Play();
                }else if(Input.GetKeyDown(KeyCode.E)){
                    Door = hit.transform.gameObject.GetComponent<Animator>();
                    _isOpen = !_isOpen;
                    Door.SetBool("open",_isOpen);
                }
            }
            else if(hit.transform.tag == "Item"){
                CanSeePickup = true;
                interact_text.text = "เก็บ";
                if (Input.GetKeyDown(KeyCode.E)){
                   Debug.Log(hit.transform.gameObject.GetComponent<Pickup>().item.name);
                   bool wasPickedUp = Inventory_Code.instance.Add(hit.transform.gameObject.GetComponent<Pickup>().item);
                   if (wasPickedUp)
                    Destroy(hit.transform.gameObject);
                }
            }
            else if(hit.transform.tag == "Doll"){
                CanSeePickup = true;
                interact_text.text = "หยิบ";
                if (Input.GetKeyDown(KeyCode.E) && !check_doll){
                    Debug.Log(hit.transform.gameObject.GetComponent<Pickup>().item.name);
                    if(hit.transform.gameObject.GetComponent<Pickup>().item.name == "Doll_MomBuy"){
                       Inventory_Code.instance.check_rabbit = true;
                    }
                    bool wasPickedUp = Inventory_Code.instance.Add(hit.transform.gameObject.GetComponent<Pickup>().item);
                    if (wasPickedUp){
                        check_doll = true;
                        Destroy(hit.transform.gameObject);
                    }
                }else if(Input.GetKeyDown(KeyCode.E) && check_doll){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "หยิบตุ๊กตาไปแล้ว";
                }
            }
            else if(hit.transform.tag == "Bed"){
                CanSeePickup = true;
                interact_text.text = "นอน";
                if (hit.transform.name == "Bed1" && Input.GetKeyDown(KeyCode.E) && !check_sleep1){
                    check_sleep1 = true;
                    ToRedDoor.Play();
                }else if(hit.transform.name == "Bed1" && Input.GetKeyDown(KeyCode.E) && check_sleep1){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "นี่ไม่ใช่เวลามานอนแล้ว";
                }
            }
            else if(hit.transform.name == "sandwish1"){
                CanSeePickup = true;
                interact_text.text = "กิน";
                if (Input.GetKeyDown(KeyCode.E)){
                    X = true;
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "ก็อร่อยดีนี่หน่า ทำไมปกติคุณแม่ถึงไม่ให้กินนะ";
                    Destroy(hit.transform.gameObject);
                }
            }
            else if(hit.transform.tag == "Expand"){
                CanSeePickup = true;
                interact_text.text = "ตรวจสอบ";
                if(hit.transform.name == "Dad_Buy" && Input.GetKeyDown(KeyCode.E)){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "ตุ๊กตาที่คุณพ่อซื้อให้เป็นของขวัญวันเกิด";
                }else if(hit.transform.name == "Mom_Buy" && Input.GetKeyDown(KeyCode.E)){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "ตุ๊กตาที่คุณแม่ซื้อให้เป็นของขวัญ";
                }else if(hit.transform.name == "fox" && Input.GetKeyDown(KeyCode.E)){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "คุณจิ้งจอกกำลังแอบอยู่ในฐานทัพลับแหละ";
                }else if(hit.transform.name == "fox" && Input.GetKeyDown(KeyCode.E)){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "คุณจิ้งจอกกำลังแอบอยู่ในฐานทัพลับแหละ";
                }else if(hit.transform.name == "totoro" && Input.GetKeyDown(KeyCode.E)){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "โตโตโร่น่ารักมากๆเลย";
                }else if(hit.transform.name == "Phone" && Input.GetKeyDown(KeyCode.E)){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "คุณแม่เคยบอกว่าอย่ากดเล่น ไม่งั้นจะโดนตีแหละ";
                }else if(hit.transform.name == "Dish" && Input.GetKeyDown(KeyCode.E)){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "เคยถูกคุณแม่ตีเพราะทำจานแตกด้วย เจ็บมากเลย";
                }else if(hit.transform.name == "Toisit" && Input.GetKeyDown(KeyCode.E)){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "เมื่อก่อนกว่าจะนั่งได้ลำบากมากเลย";
                }else if(hit.transform.name == "Pink_Totoro" && Input.GetKeyDown(KeyCode.E)){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "โตโตโร่สีชมพูแหละ";
                }
            }
            else
            {
                CanSeePickup = false;
                if(!X){
                    expand_item.gameObject.SetActive(false);
                }
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

    void scece2(){
        if(X){
            time -= Time.deltaTime;
            if(time <= 0){
                PressF.SetActive(false);
                X = false;
            }
        }
        if(Physics.Raycast(transform.position, transform.forward, out hit, RayDistance)){
            if(hit.transform.tag == "Door"){
                CanSeePickup = true;
                interact_text.text = "เปิด/ปิด";
                if(hit.transform.name == "Exit" && Input.GetKeyDown(KeyCode.E)){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "เปิดไม่ออกแฮะ";
                }else if(hit.transform.name == "Lock_Demo" && Input.GetKeyDown(KeyCode.E) && !check_key01){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "เหมือนจะล็อคนะ ต้องหากุญแจมาเปิด";
                }else if((hit.transform.name == "R" || hit.transform.name == "L" ) && Input.GetKeyDown(KeyCode.E) && !Inventory_Code.instance.check_unlock01){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "เปิดไม่ออก ต้องแก้รหัสก่อน";
                }else if(Input.GetKeyDown(KeyCode.E)){
                    Door = hit.transform.gameObject.GetComponent<Animator>();
                    _isOpen = !_isOpen;
                    Door.SetBool("open",_isOpen);
                }
            }
            else if(hit.transform.tag == "Item"){
                CanSeePickup = true;
                interact_text.text = "เก็บ";
                if (Input.GetKeyDown(KeyCode.E)){
                   Debug.Log(hit.transform.gameObject.GetComponent<Pickup>().item.name);
                   if(hit.transform.gameObject.GetComponent<Pickup>().item.name == "flashlight"){
                       X = true;
                       PressF.SetActive(true);
                       Inventory_Code.instance.check_flash = true;
                   }else if(hit.transform.gameObject.GetComponent<Pickup>().item.name == "Key"){
                       check_key01 = true;
                   }
                   bool wasPickedUp = Inventory_Code.instance.Add(hit.transform.gameObject.GetComponent<Pickup>().item);
                   if (wasPickedUp)
                    Destroy(hit.transform.gameObject);
                }
            }
            else if(hit.transform.tag == "Bed"){
                CanSeePickup = true;
                interact_text.text = "นอน";
                if (Input.GetKeyDown(KeyCode.E)){
                    Sleep01.Play();
                }
            }
            else if(hit.transform.tag == "Expand"){
                CanSeePickup = true;
                interact_text.text = "ตรวจสอบ";
                Debug.Log(hit.transform.name);
                if(hit.transform.name == "Hint" && Input.GetKeyDown(KeyCode.E)){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "ตุ๊กตา ทำไมถึงมีคำนี้เขียนแปะไว้ในตู้ล่ะ";
                }else if(hit.transform.name == "LockBody" && Input.GetKeyDown(KeyCode.E)){
                    Sophia.SetActive(false);
                    CamPas.SetActive(true);
                    CanSeePickup = false;
                }else if(hit.transform.name == "MomClue" && Input.GetKeyDown(KeyCode.E)){
                    if(!know_clue1){
                        know.Play();
                        know_clue1 = true;
                    }else{
                        expand_item.gameObject.SetActive(true);
                        expand_item.text = "ต้องเป็นเรื่องล้อเล่นแน่ๆเลย";
                    }
                }else if(hit.transform.name == "RedClue" && Input.GetKeyDown(KeyCode.E)){
                    RedInfo.Play();
                }else if(hit.transform.name == "corp" && Input.GetKeyDown(KeyCode.E)){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "นะ นี่มัน...ตุ๊กตาหรอ ทำไมพี่เจนถึงได้มีอะไรแบบนี้เก็บไว้ด้วยล่ะ";
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

    void scece3(){
        if(Physics.Raycast(transform.position, transform.forward, out hit, RayDistance)){
            if(hit.transform.tag == "Door"){
                CanSeePickup = true;
                interact_text.text = "เปิด/ปิด";
                if(hit.transform.name == "Exit" && Input.GetKeyDown(KeyCode.E)){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "เปิดไม่ออกแฮะ";
                }else if(Input.GetKeyDown(KeyCode.E)){
                    Door = hit.transform.gameObject.GetComponent<Animator>();
                    _isOpen = !_isOpen;
                    Door.SetBool("open",_isOpen);
                }
            }
            else if(hit.transform.tag == "Item"){
                CanSeePickup = true;
                interact_text.text = "เก็บ";
                if (Input.GetKeyDown(KeyCode.E)){
                   Debug.Log(hit.transform.gameObject.GetComponent<Pickup>().item.name);
                   if(hit.transform.gameObject.GetComponent<Pickup>().item.name == "MomDiary"){
                       Inventory_Code.instance.check_momDiary = true;
                       Founded.Play();
                   }
                   bool wasPickedUp = Inventory_Code.instance.Add(hit.transform.gameObject.GetComponent<Pickup>().item);
                   if (wasPickedUp)
                    Destroy(hit.transform.gameObject);
                }
            }
            else if(hit.transform.tag == "Bed"){
                CanSeePickup = true;
                interact_text.text = "นอน";
                if (Input.GetKeyDown(KeyCode.E)){
                    Sleep02.Play();
                }
            }
            else if(hit.transform.tag == "Expand"){
                CanSeePickup = true;
                interact_text.text = "ตรวจสอบ";
                if(hit.transform.name == "Hint" && Input.GetKeyDown(KeyCode.E)){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "ตุ๊กตา ทำไมถึงมีคำนี้เขียนแปะไว้ในตู้ล่ะ";
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

    void scece4(){
        if(Physics.Raycast(transform.position, transform.forward, out hit, RayDistance)){
            if(hit.transform.tag == "Door"){
                CanSeePickup = true;
                interact_text.text = "เปิด/ปิด";
                if(hit.transform.name == "Exit" && Input.GetKeyDown(KeyCode.E)){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "เปิดไม่ออกแฮะ";
                }else if(hit.transform.name == "ExitRed" && Input.GetKeyDown(KeyCode.E)){
                    back.Play();
                }else if(Input.GetKeyDown(KeyCode.E)){
                    Door = hit.transform.gameObject.GetComponent<Animator>();
                    _isOpen = !_isOpen;
                    Door.SetBool("open",_isOpen);
                }
            }
            else if(hit.transform.tag == "Item"){
                CanSeePickup = true;
                interact_text.text = "เก็บ";
                if (Input.GetKeyDown(KeyCode.E)){
                   Debug.Log(hit.transform.gameObject.GetComponent<Pickup>().item.name);
                   if(hit.transform.gameObject.GetComponent<Pickup>().item.name == "MyDiary"){
                       Inventory_Code.instance.check_myDiary = true;
                   }
                   bool wasPickedUp = Inventory_Code.instance.Add(hit.transform.gameObject.GetComponent<Pickup>().item);
                   if (wasPickedUp)
                    Destroy(hit.transform.gameObject);

                   read.Play();
                }
            }
            else if(hit.transform.tag == "Bed"){
                CanSeePickup = true;
                interact_text.text = "นอน";
                if (Input.GetKeyDown(KeyCode.E)){
                    if(!Inventory_Code.instance.check_myDiary){
                        Inventory_Code.instance.check_end = true;
                        Sleep01.Play();
                    }else{
                        expand_item.gameObject.SetActive(true);
                        expand_item.text = "ได้เวลากลับโลกแห่งความเป็นจริงแล้ว";
                    }
                }
            }else if(hit.transform.tag == "Expand"){
                CanSeePickup = true;
                interact_text.text = "ตรวจสอบ";
                if(hit.transform.name == "Hint" && Input.GetKeyDown(KeyCode.E)){
                    expand_item.gameObject.SetActive(true);
                    expand_item.text = "ตุ๊กตา ทำไมถึงมีคำนี้เขียนแปะไว้ในตู้ล่ะ";
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

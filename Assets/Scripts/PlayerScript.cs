using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    public float sensitivity;
    public InputAction playercontroller;
    public InputAction playerrotation;
    public InputAction playerpickup;
    public InputAction playercancel;
    private CharacterController character;
    public Transform orientation;
    private Vector3 move;
    private Vector2 look;
    private Vector3 movement;
    //A Button
    private bool buttonsouthpress;
    //B Button
    private bool buttoneastpress;

    public Transform groundcheck;
    public LayerMask groundmask;
    private float gravity;
    private float grounddistance;
    private bool grounded;
    private Vector3 velocity;
    private float lookx;
    private float looky;
    private float rotationx;
    private float rotationy;
    public GameObject key;
    private KeyScript k;
    private DoorScript d;
    private float pickuprange;
    public Transform keyholdposition;
    public Transform eyeline;
    private bool gotkey;
    public bool isdead;
    public GameObject pressE;
    public GameObject pressEtoopen;
    public GameObject game;
    private GameController gamecontroller;
    public GameObject enemy;
    //public GameObject winplane;
    private EnemyScript e;
    public bool issafe;
    public GameObject needkey;
    public GameObject pausemenu;
    private AudioSource audiosource;
    public AudioClip walkingsound;
    private bool playingsound;
    public GameObject darkmusic;
    private AudioSource darkmusicaudio;
    
    


    // Start is called before the first frame update
    void Start()
    {
        character = gameObject.GetComponent<CharacterController>();
        gamecontroller = game.GetComponent<GameController>();
        
        audiosource = gameObject.GetComponent<AudioSource>();
        darkmusicaudio = darkmusic.GetComponent<AudioSource>();
        //key = GameObject.FindWithTag("Key");
        //k = key.GetComponent<KeyScript>();
        //puts cursor in center of screen and so you cant see it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gravity = -9.81f;
        grounddistance = 0.2f;
        pickuprange = 5.0f;
        gotkey = false;
        pressE.SetActive(false);
        pressEtoopen.SetActive(false);
        needkey.SetActive(false);
        issafe = false;
        playingsound = false;
        darkmusicaudio.Play();
        isdead = false;
        
    }

    void OnEnable() {
        playercontroller.Enable();
        playerrotation.Enable();
        playerpickup.Enable();
        playercancel.Enable();
    }

    void OnDisable() {
        playercontroller.Disable();
        playerrotation.Disable();
        playerpickup.Disable();
        playercancel.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (gamecontroller.gameover == false && gamecontroller.gamewin == false) {
            move = playercontroller.ReadValue<Vector3>();
            buttonsouthpress = playerpickup.IsPressed();
            Look();
            if (movement.x > 0.0f || movement.z > 0.0f ) {
                if (playingsound == false) {
                    PlaySound(walkingsound);
                    playingsound = true;
                }
            }
            else {
                audiosource.Stop();
                playingsound = false;
            }
        }
        if (gamecontroller.gameover == true || gamecontroller.gamewin == true)
        {
            speed = 0.0f;
            isdead = true;
            buttoneastpress = playercancel.IsPressed();
            audiosource.Stop();
            darkmusicaudio.Stop();
            if (buttoneastpress) {
                SceneManager.LoadScene("Final Level");
                gamecontroller.gameover = false;
            }

            /*
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Game Scene");
                gamecontroller.gameover = false;
                
            }
            */
        }


        grounded = Physics.CheckSphere(groundcheck.position,grounddistance,groundmask);
        if (grounded && velocity.y < 0) {
            velocity.y = -2.0f;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }


        /*
        if (Input.GetKeyDown(KeyCode.E)) {
            StartPickup();

        }
        */
        if (buttonsouthpress) {
            StartPickup();

            if (gotkey == true) {
                OpenDoor();
            }
        }

        //Debug Raycast test
        if (gotkey == false)
        {
            //Debug raycasting test
            Debug.DrawRay(eyeline.transform.position, eyeline.transform.TransformDirection(Vector3.forward), Color.yellow);
            RaycastHit hit;
            if (Physics.Raycast(eyeline.transform.position, eyeline.transform.TransformDirection(Vector3.forward), out hit, pickuprange))
            {
                k = hit.collider.GetComponent<KeyScript>();
                d = hit.collider.GetComponent<DoorScript>();
                if (k != null)
                {
                    
                    pressE.SetActive(true);

                }
                else if (d != null) {
                    
                    needkey.SetActive(true); 
                }
                else
                {
                    pressE.SetActive(false);
                    needkey.SetActive(false);
                }
                

            }
        }
        
        //Debug Raycast test
        if (gotkey == true) {
            //Debug raycasting test
            Debug.DrawRay(eyeline.transform.position, eyeline.transform.TransformDirection(Vector3.forward), Color.yellow);
            RaycastHit hit;
            if (Physics.Raycast(eyeline.transform.position, eyeline.transform.TransformDirection(Vector3.forward), out hit, pickuprange))
            {
                d = hit.collider.GetComponent<DoorScript>();
                if (d != null)
                {
                    
                    pressEtoopen.SetActive(true);

                }
                else
                {
                    pressEtoopen.SetActive(false);
                }

            }
        }
        
        if (issafe == true) {
            gamecontroller.frosting = false;
        } 
        if (issafe == false) {
            gamecontroller.frosting = true;
        }
    }

    void FixedUpdate() {
        movement = (move.z * transform.forward) + (move.x * transform.right);
        movement.y = 0.0f;
        character.Move(movement * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        character.Move(velocity * Time.deltaTime);
    }
    
    void Look() {
        look = playerrotation.ReadValue<Vector2>();

        lookx = look.x * sensitivity * Time.deltaTime;
        looky = look.y * sensitivity * Time.deltaTime;

        rotationx -= looky;
        rotationy += lookx;
        rotationx = Mathf.Clamp(rotationx,-90f,90f);
        transform.rotation = Quaternion.Euler(-rotationx,-rotationy,0.0f);
        orientation.transform.Rotate(Vector3.up*lookx);

    }

    void StartPickup()
    {
        RaycastHit hit;
        if (Physics.Raycast(eyeline.transform.position, eyeline.transform.TransformDirection(Vector3.forward), out hit, pickuprange))
        {

            k = hit.collider.GetComponent<KeyScript>();
            if (k != null)
            {
                PickUp();
            }
        }
    }
   void PickUp() {
        key.transform.SetParent(keyholdposition);
        key.transform.SetPositionAndRotation(keyholdposition.transform.position, Quaternion.Euler(90,0,0));
        k.stopspinning = true;
        gotkey = true;
        gamecontroller.gotkey = true;
        pressE.SetActive(false);
   }

   void OpenDoor() {
        RaycastHit hit;
        if (Physics.Raycast(eyeline.transform.position,eyeline.transform.TransformDirection(Vector3.forward),out hit, pickuprange)) {
            d = hit.collider.GetComponent<DoorScript>();
            if (d != null) {
                d.opendoor = true;
                gamecontroller.dooropened = true;
            }
        }
   }

public void PlaySound(AudioClip clip) {
    audiosource.PlayOneShot(clip);
}

}

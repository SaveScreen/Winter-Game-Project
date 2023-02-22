using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    public float sensitivity;
    public InputAction playercontroller;
    public InputAction playerrotation;
    private CharacterController character;
    public Transform orientation;
    private Vector3 move;
    private Vector2 look;
    private Vector3 movement;
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
    private float pickuprange;
    public Transform keyholdposition;
    public Transform eyeline;
    private bool gotkey;
    public GameObject pressE;
    public GameObject game;
    private GameController gamecontroller;
    public GameObject enemy;


    // Start is called before the first frame update
    void Start()
    {
        character = gameObject.GetComponent<CharacterController>();
        gamecontroller = game.GetComponent<GameController>();
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
        
    }

    void OnEnable() {
        playercontroller.Enable();
        playerrotation.Enable();
    }

    void OnDisable() {
        playercontroller.Disable();
        playerrotation.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (gamecontroller.gameover == false)
        {
            move = playercontroller.ReadValue<Vector3>();
            Look();
        }
        if (gamecontroller.gameover == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                gamecontroller.gameover = false;
            }
        }


        grounded = Physics.CheckSphere(groundcheck.position,grounddistance,groundmask);
        if (grounded && velocity.y < 0) {
            velocity.y = -2.0f;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            StartPickup();

        }

        
        if (gotkey == false)
        {
            //Debug raycasting test
            Debug.DrawRay(eyeline.transform.position, eyeline.transform.TransformDirection(Vector3.forward), Color.yellow);
            RaycastHit hit;
            if (Physics.Raycast(eyeline.transform.position, eyeline.transform.TransformDirection(Vector3.forward), out hit, pickuprange))
            {
                k = hit.collider.GetComponent<KeyScript>();
                if (k != null)
                {
                    Debug.DrawRay(eyeline.transform.position, eyeline.transform.TransformDirection(Vector3.forward), Color.red);
                    pressE.SetActive(true);

                }
                else
                {
                    pressE.SetActive(false);
                }

            }
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
        pressE.SetActive(false);
   }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private Vector2 newlook;
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
 

    // Start is called before the first frame update
    void Start()
    {
        character = gameObject.GetComponent<CharacterController>();
        //puts cursor in center of screen and so you cant see it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gravity = -9.81f;
        grounddistance = 0.2f;
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
        move = playercontroller.ReadValue<Vector3>();
        Look();


        grounded = Physics.CheckSphere(groundcheck.position,grounddistance,groundmask);
        if (grounded && velocity.y < 0) {
            velocity.y = -2.0f;
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

}

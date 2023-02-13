using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    public float sensitivity;
    public InputAction playercontroller;
    private Rigidbody rb;
    public Transform orientation;
    private Vector3 move;
    private Vector3 look;
    private float lookx;
    private float looky;
    private float rotationx;
    private float rotationy;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        //puts cursor in center of screen and so you cant see it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnEnable() {
        playercontroller.Enable();
    }

    void OnDisable() {
        playercontroller.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        move = playercontroller.ReadValue<Vector3>();
        lookx = Input.GetAxis("Mouse X");
        looky = Input.GetAxis("Mouse Y");

        rotationx -= looky;
        rotationy += lookx; 

        look = new Vector3(rotationx * sensitivity, rotationy * sensitivity, 0.0f);
        transform.rotation = Quaternion.Euler(look.x,look.y,0.0f);
        orientation.rotation = Quaternion.Euler(0.0f,looky,0.0f);
        
        
    }

    void FixedUpdate() {
        rb.velocity = new Vector3(move.x * speed, 0.0f, move.z * speed);
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    private PlayerScript p;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.FindWithTag("Player");
        //p = player.GetComponent<PlayerScript>();
    }

    void OnTriggerEnter(Collider other) {
        p = other.gameObject.GetComponent<PlayerScript>();
        if (p != null) {
            p.issafe = true;
        } 
    }

    void OnTriggerExit(Collider other) {
        p = other.gameObject.GetComponent<PlayerScript>();
        if (p != null) {
            p.issafe = false;
        }
    }
}

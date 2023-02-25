using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinScript : MonoBehaviour
{
    public GameObject player;
    public GameObject game;
    private GameController g;
    private PlayerScript p;
    
    // Start is called before the first frame update
    void Start()
    {
        g = game.GetComponent<GameController>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other) {
        p = other.gameObject.GetComponent<PlayerScript>();
        if (p != null) {
            g.gamewin = true;
        }
    }
}

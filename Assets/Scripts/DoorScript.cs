using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private Animator anim;
    public GameObject game;
    private GameController gamecontroller;
    public bool opendoor;


    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        gamecontroller = game.GetComponent<GameController>();
        opendoor = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gamecontroller.gotkey == true) {
            if (opendoor == true) {
                anim.SetBool("Open",true);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    private Transform rotating;
    public float spinspeed;
    public bool stopspinning;
    public GameObject game;
    private GameController g;
    

    // Start is called before the first frame update
    void Start()
    {
        rotating = gameObject.GetComponent<Transform>();
        stopspinning = false;
        g = game.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stopspinning == false) {
            rotating.Rotate(0,spinspeed * Time.deltaTime,0);
        }
        if (g.dooropened == true) {
            Destroy(gameObject);
        }
        
    }
}

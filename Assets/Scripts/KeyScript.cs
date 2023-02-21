using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    private Transform rotating;
    public float spinspeed;
    public bool stopspinning;

    // Start is called before the first frame update
    void Start()
    {
        rotating = gameObject.GetComponent<Transform>();
        stopspinning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopspinning == false) {
            rotating.Rotate(0,spinspeed * Time.deltaTime,0);
        }
        
    }
}

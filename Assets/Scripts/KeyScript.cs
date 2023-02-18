using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    private Transform rotating;
    public float spinspeed;
    public bool timetodie;
    // Start is called before the first frame update
    void Start()
    {
        rotating = gameObject.GetComponent<Transform>();
        timetodie = false;
    }

    // Update is called once per frame
    void Update()
    {
        rotating.Rotate(0,spinspeed * Time.deltaTime,0);
        if (timetodie == true) {
           Debug.Log("Pickedup");
        }
    }
}

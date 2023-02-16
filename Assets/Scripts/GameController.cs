using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject frost;
    public GameObject hurt;
    private CanvasGroup hurtalpha;
    private CanvasGroup frostalpha;
    public GameObject player;
    private PlayerScript p;
    private float hurttimer;
    private float frosttimer;
    private bool frosting;
    private bool hurting;
    private int hurtphase;
    // Start is called before the first frame update
    void Start()
    {
        p = player.GetComponent<PlayerScript>();
        frostalpha = frost.GetComponent<CanvasGroup>();
        hurtalpha = hurt.GetComponent<CanvasGroup>();
        frosttimer = 0.5f;
        hurttimer = 6.0f;
        frosting = false;
        hurting = false;
        hurtphase = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (frosting == false)
        {
            frostalpha.alpha = Mathf.MoveTowards(frostalpha.alpha, 0.0f, frosttimer * Time.deltaTime);
        }
        else if (frosting == true)
        {
            frostalpha.alpha = Mathf.MoveTowards(frostalpha.alpha, 1.0f, frosttimer * Time.deltaTime);
        }
        if (frostalpha.alpha == 0.0f)
        {
            frosting = true;
        }
        if (frostalpha.alpha == 1.0f)
        {
            Debug.Log("OUCH!");
            frosting = false;
            hurting = true;
            
        }

        if (hurting == true)
        {
            if (hurtphase == 0)
            {
                hurtalpha.alpha = Mathf.MoveTowards(hurtalpha.alpha, 0.6f, hurttimer * Time.deltaTime);
            }
            if (hurtphase == 1)
            {
                hurtalpha.alpha = Mathf.MoveTowards(hurtalpha.alpha, 0.0f, hurttimer * Time.deltaTime);
            }

            if (hurtalpha.alpha == 0.6f)
            {
                hurtphase = 1;
            }
            if (hurtalpha.alpha == 0.0f && hurtphase == 1)
            {
                hurtphase = 0;
                hurting = false;
            }

        }
        
        
    }
}

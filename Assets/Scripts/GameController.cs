using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject frost;
    public GameObject frostdeath;
    private CanvasGroup frostdeathalpha;
    private CanvasGroup frostalpha;
    public GameObject player;
    private PlayerScript p;
    //private float hurtspeed;
    public float frostspeed;
    public float defrostspeed;
    public bool frosting;
    //private bool hurting;
    //private int hurtphase;
    public GameObject gameoverscreen;
    public bool gameover;
    public bool deathbyfrost;
    public bool gotkey;
    public bool dooropened;
    // Start is called before the first frame update
    void Start()
    {
        p = player.GetComponent<PlayerScript>();
        frostalpha = frost.GetComponent<CanvasGroup>();
        frostdeathalpha = frostdeath.GetComponent<CanvasGroup>();
        //hurtspeed = 6.0f;
        frosting = true;
        frostalpha.alpha = 0.0f;
        frostdeathalpha.alpha = 0.0f;
        //hurting = false;
        //hurtphase = 0;
        gameover = false;
        gameoverscreen.SetActive(false);
        deathbyfrost = false;
        dooropened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameover == false)
        {
            gameoverscreen.SetActive(false);

            //frosting going down
            if (frosting == false && frostalpha.alpha > 0.0f) {
                frostalpha.alpha = Mathf.MoveTowards(frostalpha.alpha, 0.0f, defrostspeed * Time.deltaTime);
            }
            else if (frosting == false && frostalpha.alpha <= 0.0f) {
                frosting = true;
            }

            //frosting going up
            if (frosting == true && frostalpha.alpha < 1.0f) {
                frostalpha.alpha = Mathf.MoveTowards(frostalpha.alpha, 1.0f, frostspeed * Time.deltaTime);
            } 
            else if (frosting == true && frostalpha.alpha >= 1.0f) {
                gameover = true;
                deathbyfrost = true;
            }

            //************************OLD FROSTING SYSTEM***********************
            /*
            if (frosting == false)
            {
                frostalpha.alpha = Mathf.MoveTowards(frostalpha.alpha, 0.0f, frostspeed * Time.deltaTime);
            }
            else if (frosting == true)
            {
                frostalpha.alpha = Mathf.MoveTowards(frostalpha.alpha, 1.0f, frostspeed * Time.deltaTime);
            }
            if (frostalpha.alpha == 0.0f && frosting == false)
            {
                frosting = true;
            }
            if (frostalpha.alpha == 1.0f && frosting == true)
            {
                frosting = false;
                hurting = true;

            }
            
            if (hurting == true)
            {
                if (hurtphase == 0)
                {
                    hurtalpha.alpha = Mathf.MoveTowards(hurtalpha.alpha, 0.6f, hurtspeed * Time.deltaTime);
                }
                if (hurtphase == 1)
                {
                    hurtalpha.alpha = Mathf.MoveTowards(hurtalpha.alpha, 0.0f, hurtspeed * Time.deltaTime);
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
            */
        }
        
        if (gameover == true)
        {
            gameoverscreen.SetActive(true);
            if (deathbyfrost == true) {
                frostdeathalpha.alpha = Mathf.MoveTowards(frostdeathalpha.alpha, 1.0f, 1.0f * Time.deltaTime);
            }
        }
        
        
    }
}

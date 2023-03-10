using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public GameObject player;
    public GameObject game;
    private GameController gamecontroller;
    private PlayerScript p;
    private NavMeshAgent agent;
    private AudioSource audiosource;
    public AudioClip hurtsound;
    public float speed;
    //private Transform currentpos;
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        gamecontroller = game.GetComponent<GameController>();
        audiosource = gameObject.GetComponent<AudioSource>();
        //currentpos = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gamecontroller.gameover == false)
        {
            agent.destination = player.transform.position;
            Vector3.MoveTowards(transform.position, agent.destination, speed * Time.deltaTime);
        }
       
    }

    
    void OnTriggerEnter(Collider other) {

        p = other.gameObject.GetComponent<PlayerScript>();
        if (p != null) {
            PlaySound(hurtsound);
            gamecontroller.gameover = true;
        }
    }

    void PlaySound(AudioClip clip) {
        audiosource.PlayOneShot(clip);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public GameObject player;
    private PlayerScript p;
    private NavMeshAgent agent;
    public float speed;
    //private Transform currentpos;
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        p = player.GetComponent<PlayerScript>();
        //currentpos = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.transform.position;
        Vector3.MoveTowards(transform.position,agent.destination,speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other) {
        if (player != null) {
            Debug.Log("Ouch!");
        }
    }
}

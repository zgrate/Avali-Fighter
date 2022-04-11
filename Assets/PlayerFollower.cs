using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerFollower : MonoBehaviour
{

    private NavMeshAgent agent;
    public int health = 100;
    public int damage = 50;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.transform.position;
        if(health <= 0)
        {
            player.GetComponent<PlayerScript>().AddKill();
            Destroy(gameObject);
        }
    }

    public void GetShot()
    {
        health -= damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerScript>().Damage();
            Destroy(gameObject);
        }
    }
}

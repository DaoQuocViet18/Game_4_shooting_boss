using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    GameObject player;
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        float distance = (transform.position - player.transform.position).magnitude;
        if (distance >= 30)
            agent.SetDestination(player.transform.position);
        else
        {
            Vector3 targetPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(targetPos);
            agent.SetDestination(transform.position);
        }
    }    
    
}

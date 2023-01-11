using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public ParticleSystem telParticle;
    GameObject player;
    public NavMeshAgent agent;

    public GameObject[] body;

    // Start is called before the first frame update
    void Start()
    {
        telParticle.Play();
        player = GameObject.Find("Player");
        Invoke("appear", 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void appear()
    {
        for (int i = 0; i < body.Length; i++)
        {
            body[i].SetActive(true);
        }
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

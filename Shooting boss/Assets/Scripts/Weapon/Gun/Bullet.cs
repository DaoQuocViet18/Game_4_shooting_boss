using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem par_Bullet;
    ParticleSystem par;
    PlayerCam playerCam;
    void Start()
    {
        playerCam = GameObject.Find("Main Camera").GetComponent<PlayerCam>();
    }


    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        par = Instantiate(par_Bullet, transform.position, transform.rotation);
        par.transform.LookAt(playerCam.orientation);
        gameObject.SetActive(false);
        Invoke("End", 5);
    }

    void End ()
    {
        Destroy(par.gameObject);
        Destroy(gameObject);
    }
}

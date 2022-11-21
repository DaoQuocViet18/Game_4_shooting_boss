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
        if ((transform.position - playerCam.transform.position).magnitude >= 200)
            Destroy(gameObject);

    }

    private void OnCollisionEnter(Collision collision)
    {
        //collision.gameObject.GetComponent<MeshCollider>().enabled = true;

        if (collision.transform.CompareTag("Untagged"))
        {
            par = Instantiate(par_Bullet, transform.position, transform.rotation);
            par.transform.LookAt(playerCam.orientation);

            gameObject.SetActive(false);
            Invoke("End", 2);
        }
    }

    void End()
    {
        Destroy(par.gameObject);
        Destroy(gameObject);
    }
}

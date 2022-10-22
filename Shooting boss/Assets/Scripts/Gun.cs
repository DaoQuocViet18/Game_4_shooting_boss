using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform posShoot;
    public float shootForce;
    public KeyCode shootKey = KeyCode.Mouse0;

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(shootKey))
        {
            GameObject currentBul = Instantiate(bullet, posShoot.position, bullet.transform.rotation);
            Rigidbody projectRb = currentBul.GetComponent<Rigidbody>();

            projectRb.AddForce(posShoot.forward * shootForce, ForceMode.Impulse);

        }
    }
}

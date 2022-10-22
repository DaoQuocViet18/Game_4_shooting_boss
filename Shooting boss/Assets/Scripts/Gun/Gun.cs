using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject bullet;
    public Transform Cam;
    public Transform posShoot;
    public float shootForce;
    public KeyCode shootKey = KeyCode.Mouse0;
    private bool readyToShoot;

    private Animator anim;

    void Start()
    {
        anim = GameObject.Find("Weapon").GetComponent<Animator>();
        readyToShoot = true;
    }


    void Update()
    {
            RaycastHit hit;

        if (Input.GetKeyDown(shootKey) && readyToShoot == true && Physics.Raycast(Cam.position, Cam.forward, out hit))
        {
            Vector3 targetLine = (hit.point - posShoot.position).normalized;
            GameObject currentBul = Instantiate(bullet, posShoot.position, bullet.transform.rotation);
            Rigidbody projectRb = currentBul.GetComponent<Rigidbody>();

            projectRb.AddForce(targetLine * shootForce, ForceMode.Impulse);
            anim.SetBool("Shooting", true);
            readyToShoot = false;
            StartCoroutine(End_Shoot());
        }
        else if (Input.GetKeyDown(shootKey) && readyToShoot == true)
        {
            GameObject currentBul = Instantiate(bullet, posShoot.position, bullet.transform.rotation);
            Rigidbody projectRb = currentBul.GetComponent<Rigidbody>();

            projectRb.AddForce(Cam.forward * shootForce, ForceMode.Impulse);
            anim.SetBool("Shooting", true);
            readyToShoot = false;
            StartCoroutine(End_Shoot());
        }

        IEnumerator End_Shoot ()
        {
            yield return new WaitForSeconds(0.5f);
            anim.SetBool("Shooting", false);
            readyToShoot = true;
        }
    }
}

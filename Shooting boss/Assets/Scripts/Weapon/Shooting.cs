using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject[] bullet;
    public Transform Cam;
    public Transform posShoot;
    public float shootForce;
    public KeyCode shootKey = KeyCode.Mouse0;
    private bool readyToShoot;

    private Animator anim;
    private WeaponSwitching weaponSwitching;

    [Header("Gun")]
    public bool gunIsReady = true;

    [Header("Boomerang")]
    public GameObject boomerang;
    public bool boomerangIsReady = true;

    void Start()
    {
        anim = GameObject.Find("Weapon").GetComponent<Animator>();
        weaponSwitching = GameObject.Find("Weapon").GetComponent<WeaponSwitching>();
        readyToShoot = true;
    }


    void Update()
    {
        RaycastHit hit;

        if (Input.GetKeyDown(shootKey) && readyToShoot == true && Physics.Raycast(Cam.position, Cam.forward, out hit)) //&& gameObject.activeInHierarchy == true)
        {
            if (boomerangIsReady == true && weaponSwitching.selectWeapon == 1)
            {
                Shoot(hit.point);
                Action();
            }
            else if (gunIsReady == true && weaponSwitching.selectWeapon == 0)
            {
                Shoot(hit.point);
                Action();
            }
        }
        else if (Input.GetKeyDown(shootKey) && readyToShoot == true )
        {
            Vector3 target = transform.position + transform.forward * 20;
            if (boomerangIsReady == true && weaponSwitching.selectWeapon == 1)
            {
                Shoot(target);
                Action();
            }
            else if (gunIsReady == true && weaponSwitching.selectWeapon == 0)
            {
                Shoot(target);
                Action();
            }
        }

    }
        void Shoot (Vector3 target)
        {
            
            Vector3 targetLine = (target - posShoot.position).normalized;
            GameObject currentBul = Instantiate(bullet[weaponSwitching.selectWeapon], posShoot.position, bullet[0].transform.rotation);
            Rigidbody projectRb = currentBul.GetComponent<Rigidbody>();

            projectRb.AddForce(targetLine * shootForce, ForceMode.Impulse);
        }

        void Action ()
        {
            if (weaponSwitching.selectWeapon == 0)
                Gun();
            else if (weaponSwitching.selectWeapon == 1)
                Boomerang();
        }

        void Gun ()
        {
            anim.SetBool("Shooting", true);
            readyToShoot = false;
            StartCoroutine(End_Shoot());
        }

        void Boomerang ()
        {
            boomerang.SetActive(false);
            boomerangIsReady = false;
        }

        public  void ResetBoomerang ()
        {
            boomerang.SetActive(true);
            boomerangIsReady = true;
        }

        IEnumerator End_Shoot ()
        {
            yield return new WaitForSeconds(0.5f);
            anim.SetBool("Shooting", false);
            readyToShoot = true;
        }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject[] bullet;
    public Transform Cam;
    public Transform posShoot;
    public float[] shootForce;
    public KeyCode shootKey = KeyCode.Mouse0;

    private Animator anim;
    private WeaponSwitching weaponSwitching;
    private PlayerMovement playerMovement;

    [Header("Weapons")]
    public GameObject[] weapon;
    public bool[] IsReady;

    private void Awake()
    {
        IsReady = new bool[weapon.Length];
        for (int i = 0; i < weapon.Length; i++)
        {
            IsReady[i] = true;
        }
    }

    void Start()
    {
        anim = GameObject.Find("Weapon").GetComponent<Animator>();
        weaponSwitching = GameObject.Find("Weapon").GetComponent<WeaponSwitching>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }


    void Update()
    {
        RaycastHit hit;

        if (playerMovement.running == false && weaponSwitching.selectWeapon != 2 && IsReady[weaponSwitching.selectWeapon] == true)
        {
            if (Input.GetKeyDown(shootKey) && Physics.Raycast(Cam.position, Cam.forward, out hit)) //&& gameObject.activeInHierarchy == true)
            {
                for (int i = 0; i < weapon.Length; i++)
                    if (weaponSwitching.selectWeapon == i && IsReady[i] == true)
                        Shoot(hit.point, shootForce[i]);
            }
            else if (Input.GetKeyDown(shootKey))
            {
                Vector3 target = transform.position + transform.forward * 20;
                for (int i = 0; i < weapon.Length; i++)
                    if (weaponSwitching.selectWeapon == i && IsReady[i] == true)
                        Shoot(target, shootForce[i]);
            }
        }
        else if (weaponSwitching.selectWeapon == 2 && IsReady[2] == true)
        {
            if (Input.GetKeyDown(shootKey))
                StartCoroutine(Sword());
        }   



    }
    void Shoot(Vector3 target, float shootForce)
    {
        Vector3 targetLine = (target - posShoot.position).normalized;
        GameObject currentBul = Instantiate(bullet[weaponSwitching.selectWeapon], posShoot.transform.position, Cam.transform.rotation);
        Rigidbody projectRb = currentBul.GetComponent<Rigidbody>();

        projectRb.AddForce(targetLine * shootForce, ForceMode.Impulse);
        Action();
    }

    void Action()
    {
        if (weaponSwitching.selectWeapon == 0 && IsReady[0] == true)
            StartCoroutine(Gun());
        else if (weaponSwitching.selectWeapon == 1 && IsReady[1] == true)
            Boomerang();
        else if (weaponSwitching.selectWeapon == 2 && IsReady[2] == true)
            StartCoroutine(Sword());
    }

    IEnumerator Gun()
    {
        IsReady[0] = false;
        anim.SetBool("Shooting", true);
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("Shooting", false);
        IsReady[0] = true;
    }

    void Boomerang()
    {
        weapon[1].SetActive(false);
        IsReady[1] = false;
    }
    public void ResetBoomerang()
    {
        weapon[0].SetActive(false);
        weaponSwitching.selectWeapon = 1;

        weapon[1].SetActive(true);
        IsReady[1] = true;
    }


    IEnumerator Sword()
    {
        anim.SetInteger("Sword", 1);
        yield return new WaitForSeconds(1f);
        anim.SetInteger("Sword", 0);
    }    
}

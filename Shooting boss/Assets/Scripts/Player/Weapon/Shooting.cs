using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject[] bullet;
    public Transform Cam;
    public Transform[] posShoot;
    public float[] shootForce;
    public KeyCode shootKey = KeyCode.Mouse0;

    private Animator anim;
    private WeaponSwitching weaponSwitching;
    private PlayerMovement playerMovement;

    [Header("Weapons")]
    public GameObject[] weapon;
    public bool[] IsReady;
    public GameObject[] effect;


    [Header("Bar")]
    public int border_mana = 3;
    HealthMana healthMana;

    [Header("Sound")]
    public AudioClip shootingSound;
    private AudioSource playerAudio;

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
        healthMana = GameObject.Find("Player").GetComponent<HealthMana>();
        playerAudio = GameObject.Find("Player").GetComponent<AudioSource>();
        healthMana.setMaxMana(border_mana);
        effect[0].SetActive(false);
    }


    void Update()
    {
        if (playerMovement.running == false && Input.GetKeyDown(shootKey))
            if ( weaponSwitching.selectWeapon != 2)
            {
                for (int i = 0; i < weapon.Length; i++)
                    if (weaponSwitching.selectWeapon == i && IsReady[i] == true)
                        Shoot(shootForce[i], posShoot[i]);
            }    
            else if (weaponSwitching.selectWeapon == 2 && IsReady[2] == true)
                StartCoroutine(Sword());
    }
    void Shoot(float shootForce, Transform posShoot)
    {
        Vector3 target;
        RaycastHit hit;
        if (Physics.Raycast(Cam.position, Cam.forward, out hit))
            target = hit.point;
        else
            target = transform.position + transform.forward * 20;

        Vector3 targetLine = (target - posShoot.position).normalized;
        GameObject currentBul = Instantiate(bullet[weaponSwitching.selectWeapon], posShoot.transform.position, Cam.transform.rotation);
        Rigidbody projectRb = currentBul.GetComponent<Rigidbody>();

        projectRb.AddForce(targetLine * shootForce, ForceMode.Impulse);
        Action();
    }

    void Action()
    {
        if (weaponSwitching.selectWeapon == 0 && IsReady[0] == true)
        {
            playerAudio.PlayOneShot(shootingSound);
            StartCoroutine(Gun());
        }    
        else if (weaponSwitching.selectWeapon == 1 && IsReady[1] == true)
            Boomerang();
    }

    IEnumerator Gun()
    {
        IsReady[0] = false;
        effect[0].SetActive(true);
        anim.SetBool("Shooting", true);
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("Shooting", false);
        effect[0].SetActive(false);
        IsReady[0] = true;
    }

    void Boomerang()
    {
        weapon[1].SetActive(false);
        IsReady[1] = false;
    }
    public void ResetBoomerang()
    {
        for (int i = 0; i < weapon.Length; i++)
            weapon[i].SetActive(false);
        
        weaponSwitching.selectWeapon = 1;

        weapon[1].SetActive(true);

        healthMana.takeMana();
    }

    IEnumerator Sword()
    {
        effect[1].SetActive(true);
        anim.SetInteger("Sword", 1);
        yield return new WaitForSeconds(1f);
        anim.SetInteger("Sword", 0);
        effect[1].SetActive(false);
    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    GameObject player;
    public GameObject bullet;
    public GameObject[] shooting_left;
    public GameObject[] shooting_right;
    public float shootForce;

    Vector3 targetLine;
    GameObject currentBul;
    Rigidbody projectRb;

    [Header ("Sound")]
    public AudioClip shootingSound;
    [SerializeField] private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("shoot", 2f, 2f);
        player = GameObject.Find("Player");
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void shoot()
    {
        for (int i = 0; i < 4; i++)
        {
            targetLine = (player.transform.position - shooting_left[i].transform.position).normalized;
            currentBul = Instantiate(bullet, shooting_left[i].transform.position, transform.transform.rotation);
            projectRb = currentBul.GetComponent<Rigidbody>();
            projectRb.AddForce(targetLine * shootForce, ForceMode.Impulse);

            targetLine = (player.transform.position - shooting_left[i].transform.position).normalized;
            currentBul = Instantiate(bullet, shooting_right[i].transform.position, transform.transform.rotation);
            projectRb = currentBul.GetComponent<Rigidbody>();
            projectRb.AddForce(targetLine * shootForce, ForceMode.Impulse);

            playerAudio.PlayOneShot(shootingSound);
        }
    }
}

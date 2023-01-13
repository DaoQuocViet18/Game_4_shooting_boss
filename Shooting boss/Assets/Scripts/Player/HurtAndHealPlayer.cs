using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtAndHealPlayer : Hurt
{
    public HealthBar1 healthBar;
    public GameObject End_Screen;
    public float timedeath;

    [Header("Sound")]
    public AudioClip healingSound;
    public AudioClip hurtSound;
    private AudioSource playerAudio;

    void Start()
    {
        playerAudio = GameObject.Find("Player").GetComponent<AudioSource>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {

        if (transform.position.y < -50 && Time.time > timedeath)
        {
            takeDamage(40);
            timedeath = Time.time + 2;
        }
    }

    protected override void Collision(Collision collision)
    {
        if (collision.gameObject.CompareTag("DamageForPlayer"))
        {
            playerAudio.PlayOneShot(hurtSound);
            takeDamage(5);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Healing"))
        {
            playerAudio.PlayOneShot(healingSound);
            healingHealth(other);
        }
    }

    private void healingHealth(Collider other)
    {
        currentHealth += 50;
        healthBar.SetHealth(currentHealth);
        Destroy(other.gameObject);
    }    

    protected override void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            End_Screen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }
}

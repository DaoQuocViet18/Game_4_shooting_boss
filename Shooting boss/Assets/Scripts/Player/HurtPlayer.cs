using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : Hurt
{
    public HealthBar1 healthBar;
    public GameObject End_Screen;
    float timedeath;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {

        if (transform.position.y < -50 && Time.time > timedeath)
        {
            takeDamage(20);
            timedeath = Time.time + 2;
        }
    }

    protected override void Collision(Collision collision)
    {
        if (collision.gameObject.CompareTag("DamageForPlayer"))
        {
            takeDamage(5);
        }
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

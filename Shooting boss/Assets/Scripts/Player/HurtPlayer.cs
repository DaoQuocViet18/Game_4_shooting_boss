using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : Hurt
{

    public HealthBar1 healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
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
            Time.timeScale = 0;
        }
    }
}

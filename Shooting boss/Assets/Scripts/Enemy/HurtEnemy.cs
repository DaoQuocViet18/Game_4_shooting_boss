using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : Hurt
{
    public HealthBar1 healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    protected override void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
            Destroy(gameObject);
    }    
}

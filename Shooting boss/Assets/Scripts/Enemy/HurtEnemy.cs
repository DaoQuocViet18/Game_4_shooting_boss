using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : Hurt
{
    public HealthBar1 healthBar;
    SpawnManage spawnManage;
    void Start()
    {
        spawnManage = GameObject.Find("SpawnManage").GetComponent<SpawnManage>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    protected override void Collision(Collision collision)
    {
        if (collision.gameObject.CompareTag("DamageForEnemy"))
        {
            takeDamage(20);
        }
    }

    protected override void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            spawnManage.active_Spawn(transform);
            Destroy(gameObject);
        }    
    }    
}

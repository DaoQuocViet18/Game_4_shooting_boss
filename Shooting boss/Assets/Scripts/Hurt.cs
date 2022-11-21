using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : MonoBehaviour
{
    public int maxHealth = 100;
    protected int currentHealth;

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DamageForEnemy"))
        {
            takeDamage(20);
        }
        else if (collision.gameObject.CompareTag("DamageForPlayer"))
        {
            takeDamage(5);
        }    
    }

    protected virtual void takeDamage(int damage)
    {
        currentHealth -= damage;
    }
}

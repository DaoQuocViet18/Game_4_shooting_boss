using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hurt : MonoBehaviour
{
    public int maxHealth = 100;
    protected int currentHealth;

    protected void OnCollisionEnter(Collision collision)
    {
        Collision(collision);
    }

    protected abstract void Collision(Collision collision);

    protected virtual void takeDamage(int damage)
    {
        currentHealth -= damage;
    }
}



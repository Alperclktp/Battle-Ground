using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public float health;

    private void Update()
    {
        Die();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        Debug.Log("Take damage: " + health);
    }

    public void Die()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

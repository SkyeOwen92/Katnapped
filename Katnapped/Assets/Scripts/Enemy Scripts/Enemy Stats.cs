using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : MonoBehaviour
{
    public string type;
    public float attack;
    public float health;
    public float defense;
    public float speed;
    public bool isHit = false;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0 )
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

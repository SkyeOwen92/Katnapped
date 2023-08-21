using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Attack(other.gameObject);
        }
    }
    void Attack(GameObject enemy)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float enemyDefense = enemy.GetComponent<Enemy>().defense;
        float attackPower = player.GetComponent<PlayerStats>().attack * (10 / (10 + enemyDefense));
        enemy.GetComponent<Enemy>().TakeDamage(attackPower);
        
    }
}

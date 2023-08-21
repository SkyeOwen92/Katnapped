using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEnimesInRange : MonoBehaviour
{
    private List<Enemy> enemiesInRange = new List<Enemy>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemiesInRange.Add(enemy);
            if(other.GetComponent<Enemy>().isHit == false)
            {
                DoDamage(other.gameObject); 
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemiesInRange.Remove(enemy);
        }
    }
    private void OnDisable()
    {
        enemiesInRange.Clear();
    }
    private void DoDamage(GameObject enemy)
    {
        //get relevent enemy stats
        string e_type = enemy.GetComponent<Enemy>().type;
        float e_def = enemy.GetComponent<Enemy>().defense;
        //get relevant player stats
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        string p_type = player.GetComponent<PlayerStats>().type;
        float p_mana = player.GetComponent<PlayerStats>().mana;

        //calculate damage 
        float typemuliplyer = TypeMultiplier(p_type, e_type);
        float damage = (p_mana * (10 / (10 + e_def))) * typemuliplyer;

        //take the enemy damage 
        enemy.GetComponent<Enemy>().TakeDamage(damage);
    }

    private float TypeMultiplier(string p_type, string e_type)
    {
        if (p_type == "Fire")
        {
            if (e_type == "Fire") return 1;
            else if (e_type == "Wind") return 1.5f;
            else return 0.5f;
        }
        else if (p_type == "Wind")
        {
            if (e_type == "Fire") return 0.5f;
            else if (e_type == "Wind") return 1;
            else return 1.5f;
        }
        else
        {
            if (e_type == "Fire") return 1.5f;
            else if (e_type == "Wind") return 0.5f;
            else return 1;
        }
    }
}


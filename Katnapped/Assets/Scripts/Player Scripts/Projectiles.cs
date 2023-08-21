using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    private bool collided = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Magic" && collision.gameObject.tag != "Player"
            && collided == false)
        {
            collided = true;
            if (collision.gameObject.tag == "Enemy" && !collision.gameObject.GetComponent<Enemy>().isHit)
            {
                collision.gameObject.GetComponent<Enemy>().isHit = true;
                DoDamage(collision.gameObject);
                
            }
        }
    }

    private void DoDamage(GameObject enemy)
    {
        //get relevent enemy stats
        string e_type = enemy.GetComponent<Enemy>().type;
        float e_hp = enemy.GetComponent<Enemy>().health;
        float e_def = enemy.GetComponent<Enemy>().defense;
        //get relevant player stats
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        string p_type = player.GetComponent<PlayerStats>().type;
        float p_mana = player.GetComponent<PlayerStats>().mana;

        //calculate damage 
        float typemuliplyer = TypeMultiplier(p_type, e_type);
        float damage = (p_mana * (10 / (10 + e_def))) * typemuliplyer;  

        //take the enemy damage 
        e_hp = e_hp - damage;
        //send new health value to enemy object
        enemy.GetComponent<Enemy>().health = e_hp;
        if (e_hp <= 0)
        {
            Destroy(enemy.gameObject);
            player.GetComponent<PlayerAttacks>().targets.Remove(enemy);
        }
        
    }

    private float TypeMultiplier(string p_type, string e_type)
    {
        if(p_type == "Fire")
        {
            if (e_type == "Fire") return 1;
            else if (e_type == "Wind") return 1.5f;
            else return 0.5f;
        }
        else if(p_type == "Wind")
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

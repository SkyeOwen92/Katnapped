using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectiles : MonoBehaviour
{
    private string element; 
    float attack;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Enemy parent = this.transform.parent.gameObject.GetComponent<Enemy>();
            PlayerStats player = other.gameObject.transform.parent.GetComponent<PlayerStats>();
            float typeMulti = TypeMultiplier(player.type, parent.type);
            float e_attack = parent.attack;
            attack = e_attack * typeMulti;
            player.health -= attack;
            if(player.health >= 0) 
            {
                player.Die();
            }
            Destroy(gameObject);
        }
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

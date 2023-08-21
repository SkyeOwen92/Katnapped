using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectiles : MonoBehaviour
{
    public string element; 
    float attack;
    bool collided = false; 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && collided == false)
        {
            string pType = collision.gameObject.GetComponent<PlayerStats>().type;
            float typeMulti = TypeMultiplier(pType, element);
            float e_attack = gameObject.GetComponentInParent<Enemy>().attack;
            attack = e_attack * typeMulti;
            float p_hp = collision.gameObject.GetComponent<PlayerStats>().health;
            if(p_hp >= 0) 
            {
                collision.gameObject.GetComponent<PlayerMovement>().Die();
            }    
            collided = true;
            Destroy(gameObject);
        }
        else if ( collided == true )
        {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public string type;
    public float attack;
    public float health;
    public float mana;
    public int MP; 
    public float defense;
    public float speed;
    public bool isHit;
    private Animator animator;
    public void Die()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Death");
        this.GetComponent<Player_Movement>().enabled = false;
        GameObject canvas = GameObject.Find("Canvas");
        int lives = canvas.GetComponent<CanvasController>().lives;
        if(lives > 0 )
        {
            canvas.GetComponent<CanvasController>().RunRespawn();
        }
        else
        {
            canvas.GetComponent<CanvasController>().undoDontDestroy();
            FindObjectOfType<MainMenu>().GameOver(); 
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public GameObject fadein;
    public GameObject fadeout;
    public Transform currentCP; //not working
    public GameObject player;
   
    private static CanvasController instance;
    //UI display 
    //health bar    
    private float healthBarMax;
    public HealthBar healthBar;

    //Gem Collection 
    private int gems;
    public TextMeshProUGUI GemTxt; 
    
    //Lives
    public int lives;
    public List<GameObject> MiniKevs; 
    private void Awake()
    {
        ShowLives(); 
        transform.position = currentCP.position;
        healthBarMax = player.GetComponent<PlayerStats>().health;
        healthBar.SetMaxHealth(healthBarMax); //make health bar
        //creates an instance of this script that will continue to run even when you respawn
        if (instance == null)
        { 
            instance = this;
            DontDestroyOnLoad(instance);
            fadeout.SetActive(false);        
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //not working 
    private void ShowLives()
    {
        if (lives > 0)
        {
            for (int i = 0; i < lives; i++)
            {
                MiniKevs[i].gameObject.SetActive(true);
            }
        }
    }
    private void AddALife()
    {
        MiniKevs[lives].gameObject.SetActive(true);
        gems -= 10; 
    }

    //updates each tie damage is taken 
    public void UpdateHealth(float hp)
    {
        healthBar.SetCurHealth(hp);
    }
    //add health when you pick up a heart 
    public void AddHealth(float hp)
    {
        float currHp = player.GetComponent<PlayerStats>().health ;
        if(currHp < healthBarMax)
        {
            currHp += hp; 
            if(currHp > healthBarMax)
            {
                currHp = healthBarMax;
            }
        }
        healthBar.SetCurHealth(currHp);
    }
    //update gems 
    public void UpdateGems(int value)
    {
        gems += value;
        GemTxt.text = gems.ToString(); 
        if(gems >= 10 && lives < 3)
        {
            AddALife();
        }
    }

    //respwan when dead 
    public void RunRespawn()
    {
        StartCoroutine(Respawn());
    }
    IEnumerator Respawn()
    {
        lives--;
        fadeout.SetActive(true);
        yield return new WaitForSeconds(2);
        fadeout.SetActive(false );
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void undoDontDestroy()
    {
        SceneManager.MoveGameObjectToScene(instance.gameObject, SceneManager.GetActiveScene());
    }
    public void SetCheckPoint(Transform newest)
    {
        currentCP = newest;
    }
}

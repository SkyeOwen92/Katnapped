using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public GameObject fadein;
    public GameObject fadeout;
    public Transform currentCP; //not working
    public GameObject player;
    public int lives;
    private static CanvasController instance;
    private void Awake()
    {

        transform.position = currentCP.position;
        
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Emralds : MonoBehaviour
{
    int value = 1;
    public NavMeshAgent gem;
    public float gemRange;
    public Transform player;
    public bool playerInRange;
    public LayerMask playerLayer;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.Find("Canvas").GetComponent<CanvasController>().UpdateGems(value);
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        transform.Rotate(0, 1, 0, Space.Self);
        playerInRange = Physics.CheckSphere(transform.position, gemRange, playerLayer);
        if (playerInRange)
        {
            moveToPlayer()
;
        }
    }
    private void moveToPlayer()
    {
        gem.SetDestination(player.position);
    }
}

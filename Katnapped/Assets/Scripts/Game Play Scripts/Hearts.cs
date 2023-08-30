using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hearts : MonoBehaviour
{
    float health = 1;
    public NavMeshAgent heart;
    public float heartRange;
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
            GameObject.Find("Canvas").GetComponent<CanvasController>().AddHealth(health);
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        transform.Rotate(0, 1, 0, Space.Self);
        playerInRange = Physics.CheckSphere(transform.position, heartRange, playerLayer);
        if (playerInRange)
        {
            moveToPlayer();
        }
    }
    private void moveToPlayer()
    {
        heart.SetDestination(player.position);
    }
}

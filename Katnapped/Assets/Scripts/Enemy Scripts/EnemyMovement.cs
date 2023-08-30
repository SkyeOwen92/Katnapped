using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //AI for enemies 
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask walkable;
    public LayerMask playerLayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet = false;
    public float walkPointRange;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked = false;
    public GameObject projectile;
    public Transform spawnPoint;

    //chasing
    public float sightRange;
    public float attackRange;
    public bool playerInSightRange = false;
    public bool playerinAttackRange = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        //check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerinAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        //what to do when the player is in range 
        if (!playerInSightRange && !playerinAttackRange) Patroling();
        if (playerInSightRange && !playerinAttackRange) Chase();
        if (playerInSightRange && playerinAttackRange) Attack();
    }
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWP = transform.position - walkPoint;
        
        //walkpoint reached, find new walk point 
        if (distanceToWP.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //calculate some randome point within the range 
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, walkable))
        {
            walkPointSet = true;
        }
    }

    private void Chase()
    {
        agent.SetDestination(player.position);
    }
    private void Attack()
    {
        player.gameObject.GetComponent<PlayerStats>().isHit = false;
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if( !alreadyAttacked )
        {
            GameObject magic = Instantiate(projectile, spawnPoint.transform.position, Quaternion.identity, this.transform) as GameObject;
            Rigidbody rb = magic.GetComponent<Rigidbody>(); 
            //make a lobbed attack
            rb.AddForce(transform.forward * 35, ForceMode.Impulse);
            rb.AddForce(transform.up * 20, ForceMode.Impulse);
            //player takes damage if hit on projectile script. 

            //wait to attack again
            alreadyAttacked = true;
            Destroy(magic, 0.5f);
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

}

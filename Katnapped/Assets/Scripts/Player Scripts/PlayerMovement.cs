using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.Jobs;
using UnityEngine.UI;
using UnityEngine.UIElements;
/* Summary: Controls player movement, incoperated animations. 
 * 
 */
public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    float moveSpeed;
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 15f;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightShift))
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }
        //forwad and backwards movement
        //normalized ensures that the magnituted is 1 
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        animator.SetFloat("Speed", moveSpeed);
        if(movement == Vector3.zero)
        {
            return; // stops if we are not moving
        }
        //**** write out what Quaternion is....
        Quaternion rotation = Quaternion.LookRotation(movement);
        //Get player to move and have it rotate based on where they are looking
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        rb.MoveRotation(rotation);


         
         
    }
}

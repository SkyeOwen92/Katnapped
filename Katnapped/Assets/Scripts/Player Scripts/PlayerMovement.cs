using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.Jobs;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using static UnityEngine.InputSystem.Controls.AxisControl;
/* Summary: Controls player movement, incoperated animations. 
* 
*/
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerInput playerInput;
    private Controls inputActions;
    float moveSpeed = 10f;
    private Animator animator;
    private void Start()
    {  
        rb = GetComponentInChildren<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        inputActions = new Controls();
        inputActions.Player.Enable();
        inputActions.Player.Jump.performed += Jump_performed;
        inputActions.Player.Sprint.started += Sprint_started;
        inputActions.Player.Sprint.canceled += Sprint_canceled;
    }
    private void Update()
    {
        Vector2 getMove = inputActions.Player.Move.ReadValue<Vector2>();
        Vector3 movement = new Vector3(getMove.x, 0, getMove.y);
        //clamp01, gets a value between 0 and 1
        float magnitude = Mathf.Clamp01(movement.magnitude) * moveSpeed;
        animator.SetFloat("speed", magnitude);
        if (movement.sqrMagnitude > 0.1f)
        {
            Vector3 move = movement; // resetting the move here somehow helps the character not snap back into it's spot

            //****write out what Quaternion is .. ..
            Quaternion rotation = Quaternion.LookRotation(move);
            //Get player to move and have it rotate based on where they are looking
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
            transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
        }
    }
    private void Jump_performed(InputAction.CallbackContext obj)
    {
        rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }
    private void Sprint_canceled(InputAction.CallbackContext obj)
    {
        moveSpeed -= 10;
    }

    private void Sprint_started(InputAction.CallbackContext obj)
    {
        moveSpeed += 10;
    }
}

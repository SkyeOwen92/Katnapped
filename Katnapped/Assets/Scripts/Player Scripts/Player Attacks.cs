using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacks : MonoBehaviour
{
    private PlayerInput playerInput;
    private Controls inputActions;
    public List<GameObject> targets;
    [SerializeField] private GetEnimesInRange range;
    private Animator animator;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        inputActions = new Controls();
        animator = GetComponent<Animator>();
        inputActions.Player.Enable();
        inputActions.Player.MagicAttack.performed += MagicAttack_performed;
        inputActions.Player.NormalAttack.performed += NormalAttack_performed;
    }

    private void NormalAttack_performed(InputAction.CallbackContext obj)
    {
        StartCoroutine(Attack());
    }

    private void MagicAttack_performed(InputAction.CallbackContext obj)
    {
        StartCoroutine(doMagic()); // use coroutine so I can set off collider after time
    }
    IEnumerator Attack()
    {
        animator.SetTrigger("attackPressed");
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("attackEnded");
    }
    IEnumerator doMagic()
    {
        range.gameObject.SetActive(true);
        ParticleSystem magicEffect = GetComponentInChildren<ParticleSystem>();
        magicEffect.Play();
        float time = 0;
        while (time <= 20)
        {
            time += Time.deltaTime;
        }
        yield return new WaitForSeconds(2);
        range.gameObject.SetActive(false);
    }
}

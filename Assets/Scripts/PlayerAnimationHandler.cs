using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    public Animator animator;
    public PlayerLocomotionHandler _playerLocomotionHandler;
    public CharacterController _characterController;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerAnimations();
    }

    public void HandlePlayerAnimations()
    {
        if (_playerLocomotionHandler.playerVelocity > 0.1 && _characterController.isGrounded)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        animator.SetBool("isSprinting", _playerLocomotionHandler.isSprinting);
        //animator.SetFloat("speed", _playerLocomotionHandler.playerVelocity);
    }
}

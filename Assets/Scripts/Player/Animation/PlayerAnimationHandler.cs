using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Animator playerAnimator;
    public float speed = 0.0f;
    public bool isCrouched;

    void Update()
    {
        speed = playerMovement.currentSpeed;
        isCrouched = playerMovement.isCrouching;
        playerAnimator.SetFloat("Speed", speed);
        playerAnimator.SetBool("Crouched", isCrouched);
    }
}

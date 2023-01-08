using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraAddOn : MonoBehaviour
{
    public Camera playerCamera;
    public PlayerMovement playerMovement;
    public float playerCameraFOVRiseMultiplier = 1.0f;
    public float playerCameraFOVFallMultiplier = 1.0f;
    public float playerCameraFOV = 60.0f;
    public float playerCameraAdaptiveFOV = 0.0f;
    public float playerCameraAdaptiveFOVMax = 10.0f;

    void Start()
    {
        playerCamera = GetComponent<Camera>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    void Update()
    {
        if (playerCamera && playerMovement)
        {
            SetAdaptiveFOV();
        }
    }

    void SetAdaptiveFOV()
    {
        if (playerMovement.isSprinting && (playerMovement.currentSpeed > playerMovement.moveSpeed))
        {
            playerCameraAdaptiveFOV += (playerCameraAdaptiveFOVMax - playerCameraAdaptiveFOV) * playerCameraFOVRiseMultiplier * Time.deltaTime;
        } else
        {
            playerCameraAdaptiveFOV -= playerCameraAdaptiveFOV * playerCameraFOVFallMultiplier * Time.deltaTime;
        }
        
        playerCameraAdaptiveFOV = Mathf.Clamp(playerCameraAdaptiveFOV, 0.0f, playerCameraAdaptiveFOVMax);

        playerCamera.fieldOfView = playerCameraFOV + playerCameraAdaptiveFOV;
    }
}

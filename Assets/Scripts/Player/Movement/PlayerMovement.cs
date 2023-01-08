using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //--------------------------------------//
    [Header("Outside Refences")]            //
    //--------------------------------------//

    public CharacterController controller;
    [Tooltip("Reference to the playerbody for local vector space and rotations")]
    public Transform playerBody;
    [Tooltip("Refernce to the empty that controlls camera rotation along the X axis")]
    public Transform RotX;
    [Tooltip("A reference to the player camera")]
    public Camera playerCamera;
    [Tooltip("Reference to a PlayerInputHandler object")]
    public PlayerInputHandler inputHandler;
    [Tooltip("Center of the BoxCast for checking groundedness")]
    public Transform sphereCastCenter;

    //--------------------------------------//
    [Header("Movement Speed")]              //
    //--------------------------------------//

    public float moveSpeed = 1.0f;
    public float currentSpeed = 0.0f;

    //--------------------------------------//
    [Header("Player Rotation")]             //
    //--------------------------------------//

    public float lookSpeed = 1.0f;
    [Tooltip("Rotation caused by moving mouse along Y axis")]
    float rotationY = 0.0f;

    //--------------------------------------//
    [Header("Input Axes")]                  //
    //--------------------------------------//

    public float forwardAxis = 0.0f;
    public float rightAxis = 0.0f;
    public float backAxis = 0.0f;
    public float leftAxis = 0.0f;
    bool movedForwardThisFrame = false;
    bool movedRightThisFrame = false;
    bool movedBackThisFrame = false;
    bool movedLeftThisFrame = false;
    bool canMove = true;

    //--------------------------------------//
    [Header("Input Sensitivities")]         //
    //--------------------------------------//

    [Tooltip("Rate at which input axes increase")]
    public float axisSensitivity = 1.0f;
    [Tooltip("Rate at which input axes decrease")]
    public float axisGravity = 1.0f;

    //--------------------------------------//
    [Header("Sprinting")]                   //
    //--------------------------------------//

    [Tooltip("Multiplier to moveSpeed")]
    public float currentSprintSpeed = 1.0f;
    [Tooltip("The max value of currentSprintSpeed")]
    public float sprintSpeedMax = 1.2f;
    [Tooltip("The rate at which currentSprintSpeed rises")]
    public float sprintSpeedRiseRate = 1.0f;
    [Tooltip("The rate at which currentSprintSpeed falls")]
    public float sprintSpeedFallRate = 1.0f;
    public bool isSprinting = false;

    //--------------------------------------//
    [Header("Crouching")]                   //
    //--------------------------------------//

    [Tooltip("Multiplier to moveSpeed")]
    public float crouchSpeed = 1.0f;
    [Tooltip("The amount that the character hitbox should shrink when crouched")]
    public float crouchOffset = -1.0f;
    public bool isCrouching = false;

    //--------------------------------------//
    [Header("Jumping")]                     //
    //--------------------------------------//

    bool isJumping = false;
    public float jumpForce = 5.0f;
    [Tooltip("A multiplier to speed applied when in air from falling or jumping")]
    public float airSpeed = 0.25f;
    [Tooltip("The amount the player is slowed after landing represented as a fraction of 1")]
    public float landSlowAmount = 0.15f;
    // Whether or not writing to the jump variable is enabled
    bool jumpWriteDisabled = false;
    [Tooltip("The time it takes for jump to be disableable again")]
    public float jumpWriteDelayCurrent = 0.1f;
    [Tooltip("The max time it takes for jump to be disableable again")]
    public float jumpWriteDelayMax = 0.1f;
    // A variable representing how much the player should move per second as a result of jumping
    Vector3 jumpPosition = new Vector3(0, 0, 0);

    //--------------------------------------//
    [Header("Ground Checks")]               //
    //--------------------------------------//

    [Tooltip("Whether or not the player is touching the ground")]
    public bool playerGrounded = false;
    [Tooltip("Sixe of the BoxCast which checks for groundedness")]
    public float castRadius = 1.0f;
    [Tooltip("The distance the Sphere is to be cast")]
    public float castDistance;
    [Tooltip("Layermask to be used with the BoxCast for checking groundedness")]
    public LayerMask sphereCastLayerMask;
    [Tooltip("Direction to snap to when landing")]
    public Vector3 snapDirection = Vector3.down;
    // A value that stores the result of (isJumping || currentGroundedTimer < 0) last frame
    bool wasInAir = false;
    [Tooltip("The normal of the surface that the player is standing on")]
    public Vector3 surfaceNormal;

    //--------------------------------------//
    [Header("Camera")]                      //
    //--------------------------------------//

    [Tooltip("The distance from the camera to the center of the player collider")]
    public float cameraHeight = .622f;
    [Tooltip("The height of the player collider")]
    public float controllerHeight = 2.0f;

    //--------------------------------------//
    [Header("Gravity")]                     //
    //--------------------------------------//

    public float gravityMultiplier = 1.0f;
    // The gravity of the player represented in velocity
    Vector3 gravityVelocity = new Vector3(0,0,0);
    // Whether or not gravity is calculated this frame
    bool updateGravity = false;
    // The amount the player should be moved down this frame as a result of gravity
    Vector3 gravityMovePosition = new Vector3(0,0,0);
    [Tooltip("Apex of jump or fall")]
    public float inAirMax = 0.0f;
    [Tooltip("Lowest Y value of jump or fall")]
    public float inAirMin = 0.0f;
    [Tooltip("The movement direction at the point in time of entering the air")]
    Vector3 enterAirTimeMovementDirection = new Vector3(0,0,0);

    //--------------------------------------//
    [Header("Movement Vectors")]            //
    //--------------------------------------//

    [Tooltip("Movement made this frame as a result of player input")]
    public Vector3 movementPosition = new Vector3(0,0,0);
    [Tooltip("A version of movement position that is only updated when grounded and wherein Time.delta is not applied")]
    public Vector3 movementDirection = new Vector3(0,0,0);
    [Tooltip("A vector used to move the player from other classes")]
    public Vector3 arbitraryAccelerationVector = new Vector3(0,0,0);
    [Tooltip("A vector used to move the player from other classes")]
    public Vector3 arbitraryVelocityVector = new Vector3(0,0,0);
    [Tooltip("A vector used to move the player from other classes")]
    public Vector3 finalArbitraryMovement = new Vector3(0,0,0);
    // The combined vector of movementPosition, gravityMovePosition, jumpPosition, finalArbitraryMovement, and airMoveVector
    Vector3 finalMovePosition = new Vector3(0,0,0);

    // Used for basic input setup 
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        inputHandler = this.GetComponentInParent<PlayerInputHandler>();
        if (inputHandler != null)
        {
            InputPackage.InputPackageHandler(new InputPackage(SetIsJumping, 1), inputHandler.OnSpaceDownPackages, true);
            InputPackage.InputPackageHandler(new InputPackage(MoveForward, 1), inputHandler.OnWHoldPackages, true);
            InputPackage.InputPackageHandler(new InputPackage(MoveRight, 1), inputHandler.OnDHoldPackages, true);
            InputPackage.InputPackageHandler(new InputPackage(MoveBack, 1), inputHandler.OnSHoldPackages, true);
            InputPackage.InputPackageHandler(new InputPackage(MoveLeft, 1), inputHandler.OnAHoldPackages, true);
            InputPackage.InputPackageHandler(new InputPackage(Sprint, 1), inputHandler.OnShiftDownPackages, true);
            InputPackage.InputPackageHandler(new InputPackage(UnSprint, 1), inputHandler.OnShiftUpPackages, true);
            InputPackage.InputPackageHandler(new InputPackage(Crouch, 1), inputHandler.OnLeftCtrlDownPackages, true);
            InputPackage.InputPackageHandler(new InputPackage(UnCrouch, 1), inputHandler.OnLeftCtrlUpPackages, true);
        }
    }

    void Update()
    {
        // Update whether or not the player was in air last frame
        wasInAir = !playerGrounded;

        // Calculate whether or not the player is on ground this frame
        SetplayerGrounded();

        // Set certain values depending on if on ground or in air
        if (playerGrounded)
        {
            WhileOnLand();
        } else if (!playerGrounded)
        {
            WhileInAir();
        }

        // Set input axes depending on player input
        SetInputAxes();

        // Update player positions
        UpdateRotation();
        UpdatePosition();

        // Update jump timer
        CountDownJumpTimer();
    }

    void UpdatePosition()
    {
        // Using player input, generate a vector to move to if allowed by canMove
        if (canMove)
        {
            SetMovementVector();
        }

        // Iterate on the loop that handles jump position when isJumping is true
        JumpLoop();

        // Set move vector for gravity
        UpdateGravityMovement();

        // Sets the finalArbitraryMovement vector 
        SetFinalArbitraryMovement();

        // Combine all the movement vectors
        SetFinalMovementVector();

        // Move
        controller.Move(finalMovePosition);

        // Reset position so it can be calculated next frame
        finalMovePosition *= 0;
    }

    void SetMovementVector()
    {
        // Calculate movement position based on current inputs, moveSpeed, and currentSprintSpeed

        {
            // Set movement direction 
            movementPosition = playerBody.forward * (forwardAxis - backAxis) + playerBody.right * (rightAxis - leftAxis);
            if (surfaceNormal != Vector3.zero)
            {
                movementPosition = Vector3.ProjectOnPlane(movementPosition, surfaceNormal);
            }
            movementPosition = Vector3.ClampMagnitude(movementPosition, 1);

            // Apply multipliers to movement direction
            movementPosition *= moveSpeed;
            movementPosition *= currentSprintSpeed;
        }

        {
            // Multipy movement position by airSpeed if not grounded
            if (!playerGrounded)
            {
                movementPosition *= airSpeed;
            }

            // Set movement direction if grounded
            if (playerGrounded && !isJumping)
            {
                movementDirection = movementPosition;
            }
        }

        // Keep record of current speed
        currentSpeed = Vector3.Magnitude(movementPosition);

        // Apply Time.deltaTime such that movementPosition can be ran every frame
        movementPosition *= Time.deltaTime;
    }

    void SetInputAxes()
    {
        // Change axis values depending on player input
        {
            if (movedForwardThisFrame)
            {
                forwardAxis += axisSensitivity * Time.deltaTime;
            }
            else
            {
                forwardAxis -= axisGravity * Time.deltaTime;
            }
            forwardAxis = Mathf.Clamp(forwardAxis, 0, 1);

            if (movedRightThisFrame)
            {
                rightAxis += axisSensitivity * Time.deltaTime;
            }
            else
            {
                rightAxis -= axisGravity * Time.deltaTime;
            }
            rightAxis = Mathf.Clamp(rightAxis, 0, 1);

            if (movedBackThisFrame)
            {
                backAxis += axisSensitivity * Time.deltaTime;
            }
            else
            {
                backAxis -= axisGravity * Time.deltaTime;
            }
            backAxis = Mathf.Clamp(backAxis, 0, 1);

            if (movedLeftThisFrame)
            {
                leftAxis += axisSensitivity * Time.deltaTime;
            }
            else
            {
                leftAxis -= axisGravity * Time.deltaTime;
            }
            leftAxis = Mathf.Clamp(leftAxis, 0, 1);
        }

        // Set sprinting axis
        {
            if (isSprinting &&
                !isCrouching &&
                playerGrounded &&
                (movedForwardThisFrame || movedRightThisFrame || movedBackThisFrame || movedLeftThisFrame))
            {
                currentSprintSpeed += sprintSpeedRiseRate * Time.deltaTime;
            }
            else
            {
                currentSprintSpeed -= sprintSpeedRiseRate * Time.deltaTime;
            }
            currentSprintSpeed = Mathf.Clamp(currentSprintSpeed, 1, sprintSpeedMax);
        }

        // Reset values for next frame
        {
            movedForwardThisFrame = false;
            movedRightThisFrame = false;
            movedBackThisFrame = false;
            movedLeftThisFrame = false;
        }
    }

    public void MoveForward()
    {
        movedForwardThisFrame = true;
    }

    public void MoveRight()
    {
        movedRightThisFrame = true;
    }

    public void MoveBack()
    {
        movedBackThisFrame = true;
    }

    public void MoveLeft()
    {
        movedLeftThisFrame = true;
    }
    
    void UpdateRotation()
    {
        // Rotate around Y axis
        playerBody.Rotate(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        // Rotate around X axis
        rotationY -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotationY = Mathf.Clamp(rotationY, -90.0f, 90.0f);
        RotX.localRotation = Quaternion.Euler(rotationY, 0, 0);
    }

    void JumpLoop()
    {
        if (isJumping)
        {
            jumpPosition = Vector3.up * jumpForce * Time.deltaTime;
        } else
        {
            jumpPosition *= 0;
        }
    }

    void UpdateGravityMovement()
    {
        if (updateGravity)
        {
            gravityVelocity += Physics.gravity * gravityMultiplier * Time.deltaTime;
            gravityMovePosition = gravityVelocity * Time.deltaTime;
        }
    }

    void SetFinalArbitraryMovement()
    {
        arbitraryVelocityVector += arbitraryAccelerationVector * Time.deltaTime;
        finalArbitraryMovement += arbitraryVelocityVector * Time.deltaTime;
    }

    void WhileOnLand()
    {
        // Set jumping to false
        if (!jumpWriteDisabled)
        {
            isJumping = false;
        }

        // Disable gravity calcs
        gravityVelocity *= 0;
        gravityMovePosition *= 0;
        updateGravity = false;

        // Set values for fall damage calculation
        inAirMax = playerBody.transform.position.y;
        inAirMin = playerBody.transform.position.y;

        // Reduce the players speed from landing
        if (wasInAir && playerGrounded)
        {
            OnLanded();
        }

        if (playerGrounded && !jumpWriteDisabled)
        {
            controller.Move(snapDirection);
        }
    }

    void WhileInAir()
    {
        // If only just entering air this frame, set movementDirection
        if (!wasInAir && !playerGrounded)
        {
            OnEnteredAirTime();
        }

        // Enable gravity calcs
        updateGravity = true;

        // Keep track of min and max y values of a fall or jump
        inAirMax = inAirMax < playerBody.transform.position.y ? playerBody.transform.position.y : inAirMax;
        inAirMin = inAirMin > playerBody.transform.position.y ? playerBody.transform.position.y : inAirMin;

        // Set surface normal to null so that movement is not disturbed in air
        surfaceNormal = Vector3.zero;
    }

    void OnLanded()
    {
        forwardAxis *= (1 / 1 + inAirMax - inAirMin) * landSlowAmount;
        rightAxis *= (1 / 1 + inAirMax - inAirMin) * landSlowAmount;
        backAxis *= (1 / 1 + inAirMax - inAirMin) * landSlowAmount;
        leftAxis *= (1 / 1 + inAirMax - inAirMin) * landSlowAmount;
        inAirMax = playerBody.transform.position.y;
        inAirMin = playerBody.transform.position.y;

        finalMovePosition += snapDirection;
    }

    void OnEnteredAirTime()
    {
        enterAirTimeMovementDirection = movementDirection;
        enterAirTimeMovementDirection = Vector3.ProjectOnPlane(enterAirTimeMovementDirection, Vector3.up);
    }

    void SetplayerGrounded()
    {
        RaycastHit raycastHit;

        Debug.DrawRay(sphereCastCenter.position, Vector3.down, Color.red, castDistance);
        if (Physics.SphereCast(sphereCastCenter.position, castRadius, Vector3.down, out raycastHit, castDistance, sphereCastLayerMask))
        {
            surfaceNormal = raycastHit.normal;
            playerGrounded = true;
        } else
        {
            playerGrounded = false;
        }
    }

    void SetFinalMovementVector()
    {
        finalMovePosition += movementPosition + gravityMovePosition + jumpPosition + finalArbitraryMovement;

        if (!playerGrounded)
        {
            finalMovePosition += enterAirTimeMovementDirection * Time.deltaTime;
        }
    }

    void CountDownJumpTimer()
    {
        if (jumpWriteDisabled)
        {
            jumpWriteDelayCurrent -= Time.deltaTime;
            if (jumpWriteDelayCurrent < 0)
            {
                jumpWriteDisabled = false;
                jumpWriteDelayCurrent = jumpWriteDelayMax;
            }
        }
    }

    public void SetIsJumping()
    {
        if (playerGrounded)
        {
            isJumping = true;
            jumpWriteDisabled = true;
            enterAirTimeMovementDirection = movementDirection;
            forwardAxis = 0.0f;
            rightAxis = 0.0f;
            backAxis = 0.0f;
            leftAxis = 0.0f;
            inAirMax = playerBody.transform.position.y;
            inAirMin = playerBody.transform.position.y;
        }
    }

    public void Sprint()
    {
        isSprinting = true;
    }

    public void UnSprint()
    {
        isSprinting = false;
    }

    void Crouch()
    {
        RotX.transform.localPosition = (Vector3.up * cameraHeight) + (Vector3.up * crouchOffset * .5f);
        controller.height = controllerHeight + crouchOffset;
        controller.Move(Vector3.up * (crouchOffset / 2));
        isCrouching = true;
    }

    void UnCrouch()
    {
        RotX.transform.localPosition = (Vector3.up * cameraHeight);
        controller.height = controllerHeight;
        controller.Move(Vector3.up * (crouchOffset / 2) * -1);
        isCrouching = false;
    }

    void OnDrawGizmos()
    {
        // Draw ground detection volume
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(sphereCastCenter.position + Vector3.down * castDistance, castRadius);

        // Draw player skin volume
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerBody.position - (Vector3.up * (controller.height / 2 - controller.radius)), controller.radius + controller.skinWidth);
        Gizmos.DrawWireSphere(playerBody.position + (Vector3.up * (controller.height / 2 - controller.radius)), controller.radius + controller.skinWidth);
    }

    ~PlayerMovement()
    {
        if (inputHandler != null)
        {
            InputPackage.InputPackageHandler(new InputPackage(SetIsJumping, 1), inputHandler.OnSpaceDownPackages, false);
            InputPackage.InputPackageHandler(new InputPackage(MoveForward, 1), inputHandler.OnWHoldPackages, false);
            InputPackage.InputPackageHandler(new InputPackage(MoveRight, 1), inputHandler.OnDHoldPackages, false);
            InputPackage.InputPackageHandler(new InputPackage(MoveBack, 1), inputHandler.OnSHoldPackages, false);
            InputPackage.InputPackageHandler(new InputPackage(MoveLeft, 1), inputHandler.OnAHoldPackages, false);
            InputPackage.InputPackageHandler(new InputPackage(Sprint, 1), inputHandler.OnShiftDownPackages, true);
            InputPackage.InputPackageHandler(new InputPackage(UnSprint, 1), inputHandler.OnShiftUpPackages, true);
            InputPackage.InputPackageHandler(new InputPackage(Crouch, 1), inputHandler.OnLeftCtrlDownPackages, false);
            InputPackage.InputPackageHandler(new InputPackage(UnCrouch, 1), inputHandler.OnLeftCtrlUpPackages, false);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Interactable
{
    //--------------------------------------//
    [Header("Outside References")]          //
    //--------------------------------------//

    [Tooltip("An array of references to items that have the possibility of spawning")]
    public GameObject[] randomItemPool;
    [Tooltip("The location that the items spawns at")]
    public Transform spawnLocation;
    [Tooltip("A reference to the animator used to open and close the box etc.")]
    public Animator boxAnimator;
    [Tooltip("A public value to use with SpriteValueMonitor")]
    public PublicFloat publicFloat;
    [Tooltip("A reference to the SpriteValueMonitor used to display interaction progress")]
    public SpriteValueMonitor spriteValueMonitor;
    [Tooltip("A reference to the transform used to set the position of the sprite value monitor")]
    public Transform spriteValueMonitorPositionHandle;

    //--------------------------------------//
    [Header("Box Settings")]                //
    //--------------------------------------//

    [SerializeField]
    [Tooltip("Time it takes to spawn an item")]
    float timeTilSpawn = 5.0f;
    [Tooltip("Time you have to hold down to interact with the object")]
    public ValueAxis interactionProgressAxis;
    [Tooltip("Whether or not the player can interact")]
    bool canInteract = true;
    [SerializeField]
    [Tooltip("The cost to operate the box")]
    int cost = 900;

    void Start()
    {
        interactionProgressAxis = GetComponent<ValueAxis>();
        publicFloat = GetComponent<PublicFloat>();
        spriteValueMonitor.gameObject.transform.position = spriteValueMonitorPositionHandle.position;
    }

    void Update()
    {
        // Spawn and item and subtract points from player if they have interacted with the box long enough
        if (interactionProgressAxis.currentValue_f == interactionProgressAxis.maxValue_f)
        {
            SubtractMoneyFromPlayer();
            RunSpawnItem();

            canInteract = false;
            interactionProgressAxis.currentValue_f = 0.0f;
        }
        publicFloat.publicValue = interactionProgressAxis.currentValue_f;
    }

    public override void EHold()
    {
        if (canInteract)
        {
            // Increase the value of the axis
            interactionProgressAxis.IncreaseAxis();
        }
    }

    public override void Hover(GameObject sentGameObject)
    {
        if (canInteract)
        {
            spriteValueMonitor.OnLook(sentGameObject);
        }
    }

    void SubtractMoneyFromPlayer()
    {
        Debug.Log("Subtracted funds");
    }

    void RunSpawnItem()
    {
        Debug.Log("Box was used");
    }
}

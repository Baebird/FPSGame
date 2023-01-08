using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    //--------------------------------------//
    [Header("Outside References")]          //
    //--------------------------------------//

    [Tooltip("Reference to the player")]
    public GameObject playerGameObject;
    [Tooltip("Reference to the player's camera")]
    public Camera playerCamera;
    [Tooltip("Reference to the InputHandler")]
    public PlayerInputHandler inputHandler;

    //--------------------------------------//
    [Header("Properties")]                  //
    //--------------------------------------//

    [SerializeField]
    [Tooltip("The radius used for the sphere cast for hover events")]
    float hoverEventCastRadius;
    [Tooltip("The max length of the ray used for the hover event raycast")]
    float hoverEventCastLength = 4.0f;
    [Tooltip("The length of the ray used to interact with interactables and items")]
    public float reachDistance = 2.0f;
    [Tooltip("The power at which you throw things when dequipping items")]
    public float throwPower = 5.0f;
    [Tooltip("A reference to the item that the hand equipped")]
    public Item heldItem;

    //--------------------------------------//
    [Header("Raycasting")]                  //
    //--------------------------------------//

    [SerializeField]
    [Tooltip("The mask used when casting rays for interaction")]
    LayerMask layerMask;
    // A variable to save data from succesful raycasts
    RaycastHit raycastHit;

    void Start()
    {
        // Get input handler
        inputHandler = GetComponentInParent<PlayerInputHandler>();

        // Initialize inputs
        if (inputHandler != null)
        {
            InputPackage.InputPackageHandler(new InputPackage(EDown, 1), inputHandler.OnEDownPackages, true);
            InputPackage.InputPackageHandler(new InputPackage(EHold, 1), inputHandler.OnEHoldPackages, true);
            InputPackage.InputPackageHandler(new InputPackage(EUp, 1), inputHandler.OnEUpPackages, true);
            InputPackage.InputPackageHandler(new InputPackage(DequipItem, 1), inputHandler.OnQDownPackages, true);
        }
    }

    void Update()
    {
        Hover();
    }

    void Hover()
    {
        InteractionRays.SphericalHoverRaycast(playerCamera.transform.position, hoverEventCastRadius, playerCamera.transform.forward, out raycastHit, hoverEventCastLength, layerMask, playerGameObject);
    }

    void EDown()
    {
        // Run standard ray for interaction when pressing E Down
        bool successfulInteraction = InteractionRays.EDownInteractionRay(playerCamera.transform.position, playerCamera.transform.forward, out raycastHit, reachDistance, layerMask);

        // If it was succesful, back out
        if (successfulInteraction) return;

        // If no interactable was found, then if an item component was found, equip it
        Item hitItem = raycastHit.collider.gameObject.GetComponent<Item>();
        if ((hitItem != null) && (hitItem.isEquipped == false))
        {
            // Dequip currently held item
            if (heldItem != null)
            {
                DequipItem();
            }

            // Equip new item found
            EquipItem(hitItem);
        }
    }

    void EHold()
    {
        InteractionRays.EHoldInteractionRay(playerCamera.transform.position, playerCamera.transform.forward, out raycastHit, reachDistance, layerMask);
        InteractionRays.EHoldInteractionRay(playerCamera.transform.position, playerCamera.transform.forward, out raycastHit, reachDistance, layerMask, gameObject);
    }

    void EUp()
    {
        InteractionRays.EUpInteractionRay(playerCamera.transform.position, playerCamera.transform.forward, out raycastHit, reachDistance, layerMask);
    }

    public void EquipItem(Item item)
    {
        // Parent to the player
        item.gameObject.transform.SetParent(this.transform);

        // Reset it's rotation and position
        item.gameObject.transform.localPosition = Vector3.zero;
        item.gameObject.transform.localEulerAngles = Vector3.zero;

        // Run it's OnEquip setup function
        item.OnEquip();

        // Set a reference to the currently Equipped item
        heldItem = item;
    }

    public void DequipItem()
    {
        // If a reference to a held item exists, continue
        if (heldItem != null)
        {
            // De-parent item from parent
            heldItem.gameObject.transform.SetParent(null);

            // Run the item's OnDequip function
            heldItem.OnDequip();

            // Try to get this item's rigidbody component
            Rigidbody rb = heldItem.gameObject.GetComponent<Rigidbody>();

            // If found then:
            if (rb != null)
            {
                // Throw the item
                rb.velocity = playerCamera.transform.forward * throwPower;
            }

            // Remove reference to the dequipped item
            heldItem = null;
        }
    }

    ~Hand()
    {
        // Unbind keys to functions
        InputPackage.InputPackageHandler(new InputPackage(EDown, 1), inputHandler.OnEDownPackages, false);
        InputPackage.InputPackageHandler(new InputPackage(EHold, 1), inputHandler.OnEHoldPackages, false);
        InputPackage.InputPackageHandler(new InputPackage(EUp, 1), inputHandler.OnEUpPackages, false);
        InputPackage.InputPackageHandler(new InputPackage(DequipItem, 1), inputHandler.OnQDownPackages, false);
    }
}

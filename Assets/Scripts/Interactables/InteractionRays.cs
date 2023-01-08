using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionRays : MonoBehaviour
{
    static public bool EDownInteractionRay(Vector3 position, Vector3 direction, out RaycastHit raycastHit, float reachDistance, LayerMask layerMask)
    {
        // Variables to save data from the following raycast
        Interactable hitInteractable;

        bool wasSuccesful = Physics.Raycast(position, direction, out raycastHit, reachDistance, layerMask);

        if (wasSuccesful)
        {
            hitInteractable = raycastHit.collider.gameObject.GetComponent<Interactable>();

            if (hitInteractable)
            {
                hitInteractable.EDown();
            }
        }

        return wasSuccesful;
    }
    static public bool EDownInteractionRay(Vector3 position, Vector3 direction, out RaycastHit raycastHit, float reachDistance, LayerMask layerMask, GameObject gameObject)
    {
        // Variables to save data from the following raycast
        Interactable hitInteractable;

        bool wasSuccesful = Physics.Raycast(position, direction, out raycastHit, reachDistance, layerMask);

        if (wasSuccesful)
        {
            hitInteractable = raycastHit.collider.gameObject.GetComponent<Interactable>();

            if (hitInteractable)
            {
                hitInteractable.EDown(gameObject);
            }
        }

        return wasSuccesful;
    }
    static public bool EHoldInteractionRay(Vector3 position, Vector3 direction, out RaycastHit raycastHit, float reachDistance, LayerMask layerMask)
    {
        // Variables to save data from the following raycast
        Interactable hitInteractable;

        bool wasSuccesful = Physics.Raycast(position, direction, out raycastHit, reachDistance, layerMask);

        if (wasSuccesful)
        {
            hitInteractable = raycastHit.collider.gameObject.GetComponent<Interactable>();

            if (hitInteractable)
            {
                hitInteractable.EHold();
            }
        }

        return wasSuccesful;
    }
    static public bool EHoldInteractionRay(Vector3 position, Vector3 direction, out RaycastHit raycastHit, float reachDistance, LayerMask layerMask, GameObject gameObject)
    {
        // Variables to save data from the following raycast
        Interactable hitInteractable;

        bool wasSuccesful = Physics.Raycast(position, direction, out raycastHit, reachDistance, layerMask);

        if (wasSuccesful)
        {
            hitInteractable = raycastHit.collider.gameObject.GetComponent<Interactable>();

            if (hitInteractable)
            {
                hitInteractable.EHold(gameObject);
            }
        }

        return wasSuccesful;
    }
    static public bool EUpInteractionRay(Vector3 position, Vector3 direction, out RaycastHit raycastHit, float reachDistance, LayerMask layerMask)
    {
        // Variables to save data from the following raycast
        Interactable hitInteractable;

        bool wasSuccesful = Physics.Raycast(position, direction, out raycastHit, reachDistance, layerMask);

        if (wasSuccesful)
        {
            hitInteractable = raycastHit.collider.gameObject.GetComponent<Interactable>();

            if (hitInteractable)
            {
                hitInteractable.EUp();
            }
        }

        return wasSuccesful;
    }
    static public bool SphericalHoverRaycast(Vector3 position, float radius, Vector3 direciton, out RaycastHit raycastHit, float reachDistance, LayerMask layerMask)
    {
        // Variables to save data from the following raycast
        Interactable hitInteractable;

        bool wasSuccesful = Physics.SphereCast(position, radius, direciton, out raycastHit, reachDistance, layerMask);

        if (wasSuccesful)
        {
            hitInteractable = raycastHit.collider.gameObject.GetComponent<Interactable>();

            if (hitInteractable)
            {
                hitInteractable.Hover();
            }
        }

        return wasSuccesful;
    }
    static public bool SphericalHoverRaycast(Vector3 position, float radius, Vector3 direciton, out RaycastHit raycastHit, float reachDistance, LayerMask layerMask, GameObject gameObject)
    {
        // Variables to save data from the following raycast
        Interactable hitInteractable;

        bool wasSuccesful = Physics.SphereCast(position, radius, direciton, out raycastHit, reachDistance, layerMask);

        if (wasSuccesful)
        {
            hitInteractable = raycastHit.collider.gameObject.GetComponent<Interactable>();

            if (hitInteractable)
            {
                hitInteractable.Hover(gameObject);
            }
        }

        return wasSuccesful;
    }
}

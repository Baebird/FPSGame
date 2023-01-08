using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public Collider forceField;

    public Vector3 forceVector;

    public Transform StartVector;

    public Transform EndVector;

    public float fanStrength;

    PlayerMovement playerMovement;

    void Start()
    {
        forceVector = (EndVector.position - StartVector.position).normalized * fanStrength;
    }

    void OnTriggerStay(Collider collider)
    {
        playerMovement = collider.gameObject.GetComponent<PlayerMovement>();

        if (playerMovement != null)
        {
            if (playerMovement.arbitraryVelocityVector.magnitude < 5)
            {
                playerMovement.arbitraryAccelerationVector = forceVector;
            } else
            {
                playerMovement.arbitraryAccelerationVector *= 0;
            }
        }

        Debug.Log("Player Found");
    }

    void OnTriggerExit(Collider collider)
    {
        playerMovement.arbitraryAccelerationVector *= 0;
        playerMovement.arbitraryVelocityVector *= 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public Vector3 bulletStartingPosition;
    public Vector3 bulletDirection;
    Vector3 lastFramePosition;

    public float lifetime;
    public float bulletVelocity;
    public float bulletGravity = 0.05f;
    public float bulletDamage = 1.0f;

    float deathElevation = -100.0f;

    void Start()
    {
        bulletStartingPosition = transform.position;
    }

    void Update()
    {
        Move();
        CheckPath();

        CheckForDeathY();
        lifetime += Time.deltaTime;
    }

    void Move()
    {
        lastFramePosition = transform.position;
        transform.position = bulletDirection * bulletVelocity * lifetime
            - Vector3.up * bulletGravity * (Mathf.Pow(lifetime, 2) * 0.5f)
            + bulletStartingPosition;
    }

    void CheckPath()
    {
        RaycastHit hitCollider;
        if (Physics.Raycast(lastFramePosition, Vector3.Normalize(transform.position - lastFramePosition), out hitCollider, Vector3.Magnitude(transform.position - lastFramePosition)))
        {
            Damager damager = hitCollider.collider.GetComponent<Damager>();
            if (damager)
            {
                damager.DealDamage(bulletDamage);
                Destroy(gameObject);
            }
        }
    }

    void CheckForDeathY()
    {
        if (transform.position.y <= deathElevation)
        {
            Destroy(gameObject);
        }
    }
}

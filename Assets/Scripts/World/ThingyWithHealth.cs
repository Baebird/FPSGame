using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingyWithHealth : MonoBehaviour
{
    public float health;
    void Update()
    {
        if (health < 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Oops me die!");
        Destroy(gameObject);
    }
}

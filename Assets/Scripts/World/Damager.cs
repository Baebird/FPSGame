using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public ThingyWithHealth damagePointer;

    public void DealDamage(float damage)
    {
        damagePointer.health -= damage;
    }

    void Update()
    {
        if (damagePointer.health < 0)
        {
            Destroy(gameObject);
        }
    }
}

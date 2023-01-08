using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isEquipped { get; private set; }
    public virtual void OnEquip()
    {
        isEquipped = true;
    }
    public virtual void OnDequip()
    {
        isEquipped = false;
    }

    ~Item()
    {
        OnDequip();
    }
}

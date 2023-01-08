using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // E
    virtual public void EDown() {}
    virtual public void EDown(GameObject gameObject) {}
    virtual public void EHold() {}
    virtual public void EHold(GameObject gameObject) {}
    virtual public void EUp() {}
    virtual public void EUp(GameObject gameObject) {}

    virtual public void Hover() { }
    virtual public void Hover(GameObject gameObject) { }
}

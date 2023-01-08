using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositionRB : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 movePoint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        movePoint += Vector3.up * 1.0f * Time.deltaTime;
        rb.Move(movePoint, Quaternion.identity);
    }
}

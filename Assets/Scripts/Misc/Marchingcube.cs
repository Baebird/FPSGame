using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marchingcube : MonoBehaviour
{
    public Transform tr;

    public float marchCountdown = 1.0f;

    public float randomValue;

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        marchCountdown -= Time.deltaTime;

        if (marchCountdown < 0)
        {
            marchCountdown += 1.0f;
            randomValue = Random.Range(1, 6);
            
            switch ((int)randomValue)
            {
                case 1:
                    tr.position = tr.position + Vector3.up;
                    break;
                case 2:
                    tr.position = tr.position + Vector3.right;
                    break;
                case 3:
                    tr.position = tr.position + Vector3.forward;
                    break;
                case 4:
                    tr.position = tr.position - Vector3.up;
                    break;
                case 5:
                    tr.position = tr.position - Vector3.right;
                    break;
                case 6:
                    tr.position = tr.position - Vector3.forward;
                    break;
            }
        }
    }
}

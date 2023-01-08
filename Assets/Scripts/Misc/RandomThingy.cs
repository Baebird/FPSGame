using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomThingy : Interactable
{
    public PublicFloat publicFloat;
    public ValueAxis interactionProgress;
    public SpriteValueMonitor monitor;

    void Update()
    {
        if(CheckForSucess())
        {
            Grow();
            interactionProgress.currentValue_f = 0;
        }
        publicFloat.publicValue = interactionProgress.currentValue_f;
    }

    public override void EHold()
    {
        interactionProgress.IncreaseAxis();
    }

    public override void Hover(GameObject gameObject)
    {
        monitor.OnLook(gameObject);
    }

    public bool CheckForSucess()
    {
        return interactionProgress.currentValue_f == interactionProgress.maxValue_f;
    }

    public void Grow()
    {
        transform.localScale = new Vector3(transform.localScale.x + 0.5f, transform.localScale.y + 0.5f, transform.localScale.z + 0.5f);
    }
}

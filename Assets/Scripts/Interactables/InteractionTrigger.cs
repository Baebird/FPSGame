using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionTrigger : Interactable
{
    public delegate void VoidFunction();
    public delegate void ParameterizedFunction(GameObject gameObject);

    // E
    public Button b;
    public VoidFunction eDownEvents;
    public ParameterizedFunction eDownEvents_p;
    public VoidFunction eHoldEvents;
    public ParameterizedFunction eHoldEvents_p;
    public VoidFunction eUpEvents;
    public ParameterizedFunction eUpEvents_p;

    // Hover
    public VoidFunction[] hoverEvents;
    public ParameterizedFunction[] hoverEvents_p;

    public override void EDown()
    {
        eDownEvents();
    }

    public override void EDown(GameObject gameObject)
    {
        eDownEvents_p(gameObject);
    }

    public override void EHold()
    {
        eHoldEvents();
    }

    public override void EHold(GameObject gameObject)
    {
        eHoldEvents_p(gameObject);
    }

    public override void EUp()
    {
        eUpEvents();
    }

    public override void EUp(GameObject gameObject)
    {
        eUpEvents_p(gameObject);
    }
}

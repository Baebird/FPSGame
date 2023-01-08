using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueAxis : MonoBehaviour
{
    //--------------------------------------//
    [Header("Value Axis Properties")]       //
    //--------------------------------------//

    [Tooltip("Current axis value")]
    public float currentValue_f = 0.0f;
    [Tooltip("Max axis value")]
    public float maxValue_f = 1.0f;
    [Tooltip("Minimum axis value")]
    public float minValue_f = 0.0f;
    [Tooltip("The rate in u/s at which axis increases when told to")]
    public float riseRate_f = 1.0f;
    [Tooltip("The rate in u/s at which axis decreases when told to")]
    public float fallRate_f = 1.0f;
    [Tooltip("Whether or not the resting state is enabled")]
    public bool restingStateEnabled_f = true;
    [Tooltip("If true the axis' resting state is at the maxValue, if false it's resting state is at the minValue")]
    public bool restingStateSwitch_f = false;
    // A flag set to represent whether or not the value has increased this frame
    bool hasChanged_fl = false;

    //--------------------------------------//
    // Constructors                         //
    //--------------------------------------//

    public ValueAxis() { }

    public ValueAxis(float minValue, float maxValue, bool restingStateEnabled)
    {
        minValue_f = minValue;
        maxValue_f = maxValue;
        restingStateEnabled_f = restingStateEnabled;
    }

    public ValueAxis(float minValue, float maxValue, float riseRate, float fallRate, bool restingStateSwitch, bool restingStateEnabled)
    {
        minValue_f = minValue;
        maxValue_f = maxValue;
        riseRate_f = riseRate;
        fallRate_f = fallRate;
        restingStateSwitch_f = restingStateSwitch;
        restingStateEnabled_f = restingStateEnabled;
    }

    void Update()
    {
        if (restingStateEnabled_f)
        {
            if (!hasChanged_fl)
            {
                if (restingStateSwitch_f)
                {
                    IncreaseAxis();
                } else
                {
                    DecreaseAxis();
                }
            }
        }
        hasChanged_fl = false;
    }

    public void IncreaseAxis()
    {
        currentValue_f += riseRate_f * Time.deltaTime;
        currentValue_f = Mathf.Clamp(currentValue_f, minValue_f, maxValue_f);
        hasChanged_fl = true;
    }

    public void DecreaseAxis()
    {
        currentValue_f -= fallRate_f * Time.deltaTime;
        currentValue_f = Mathf.Clamp(currentValue_f, minValue_f, maxValue_f);
        hasChanged_fl = true;
    }
}

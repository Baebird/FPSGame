using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpriteValueMonitor : MonoBehaviour
{
    //--------------------------------------//
    [Header("Outside References")]          //
    //--------------------------------------//

    // Set in inspector
    [Tooltip("Reference to the tracked value")]
    public PublicFloat referencedValue;

    [Tooltip("Value Axis for opacity")]
    public ValueAxis opacityAxis;

    // Set in code
    [Tooltip("Reference to the TMP object used for text")]
    TextMeshPro textMesh;

    [Tooltip("Reference to player")]
    [HideInInspector]
    public GameObject player;

    [Tooltip("A reference to the renderer")]
    Renderer spvRenderer;

    [Tooltip("A boolean that stores whether or not the material has a _Progress property")]
    bool hasProgress;

    void Start()
    {
        textMesh = GetComponentInChildren<TextMeshPro>();
        spvRenderer = GetComponent<Renderer>();
        hasProgress = spvRenderer.material.HasFloat("_Progress");
    }

    void Update()
    {
        if (hasProgress)
        {
            spvRenderer.material.SetFloat("_Progress", referencedValue.publicValue);
        }

        SetOpacity();
    }

    public void OnLook(GameObject sentGameObject)
    {
        if (opacityAxis)
        {
            opacityAxis.IncreaseAxis();
        }

        Transform cameraTransform = sentGameObject.GetComponentInChildren<Camera>().gameObject.transform;
        if (cameraTransform)
        {
            transform.rotation = cameraTransform.rotation;
        }
    }

    void SetOpacity()
    {
        spvRenderer.material.SetFloat("_Opacity", opacityAxis.currentValue_f);
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, opacityAxis.currentValue_f);
    }
}

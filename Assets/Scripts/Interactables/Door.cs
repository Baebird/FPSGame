using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public Transform doorPivot;
    public AudioSource doorSoundSource;
    public AudioClip[] closeSounds;
    public AudioClip[] openSounds;

    public float openRotation = 90.0f;
    float currentRotation = 0.0f;
    public float openSpeed = 40.0f;
    public float closeRotation = 0.0f;

    public bool isOpen = false;

    public override void EDown()
    {
        isOpen = !isOpen;
    }

    void Update()
    {
        if (isOpen)
        {
            currentRotation += openSpeed * Time.deltaTime;
            if (doorSoundSource != null && openSounds != null)
            {
                PlayRandom(openSounds);
            }
        }
        else
        {
            currentRotation -= openSpeed * Time.deltaTime;
            if (doorSoundSource != null && closeSounds != null)
            {
                PlayRandom(openSounds);
            }
        }
        currentRotation = Mathf.Clamp(currentRotation, closeRotation, openRotation);
        doorPivot.localEulerAngles = new Vector3(0, currentRotation, 0);
    }

    void PlayRandom(AudioClip[] audioClipArray)
    {
        int randomIndex;
        randomIndex = Random.Range(0, audioClipArray.Length);
        if (doorSoundSource != null)
        {
            doorSoundSource.PlayOneShot(audioClipArray[randomIndex]);
        }
    }
}

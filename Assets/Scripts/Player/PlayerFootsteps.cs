using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    AudioSource FootstepsAudioSource;

    [SerializeField]
    AudioClip[] footstepClips;
    CharacterController characterController;

    [HideInInspector]
    public float volumeMin, volumeMax;

    float accumulatedDistance;

    [HideInInspector]
    public float stepDistance;

    void Start()
    {
        FootstepsAudioSource = GetComponent<AudioSource>();
        characterController = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        isFootstepsValid();
    }

    private void isFootstepsValid()
    {
        if (!characterController.isGrounded)
        {
            return;
        }

        if (characterController.velocity.sqrMagnitude > 0)
        {
            accumulatedDistance += Time.deltaTime;

            if (accumulatedDistance > stepDistance)
            {
                FootstepsAudioSource.volume = UnityEngine.Random.Range(volumeMin, volumeMax);
                FootstepsAudioSource.clip = footstepClips[UnityEngine.Random.Range(0, footstepClips.Length)];
                FootstepsAudioSource.Play();

                accumulatedDistance = 0f;
            }
        }
        else
        {
            accumulatedDistance = 0f;
        }
    }
}

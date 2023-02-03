﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Collectible : MonoBehaviour
{
    [Header("Feedback")]
    [SerializeField] AudioClip collectSFX = null;
    [SerializeField] ParticleSystem collectParticle = null;

    [Header("Required References")]
    [SerializeField] Collider triggerToDisable = null;
    [SerializeField] GameObject artToDisable = null;

    AudioSource audioSource = null;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory inventory = other.attachedRigidbody.GetComponent<PlayerInventory>();
        if(inventory != null)
        {
            // we found the inventory!
            inventory.AddCollectible();
            PlayFX();
        }

        // disable relevant components for "Collection"
        triggerToDisable.enabled = false;
        artToDisable.SetActive(false);
    }

    void PlayFX()
    {
        // play gfx
        if(collectParticle != null)
        {
            collectParticle.Play();
        }
        // play sfx
        if(audioSource != null && collectSFX != null)
        {
            audioSource.PlayOneShot(collectSFX, audioSource.volume);
        }
    }
}

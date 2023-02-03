using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float maxSpeed = 20f;
    [SerializeField] float maxTurnSpeed = 150f;

    float moveAmountThisFrame = 0;
    float turnAmountThisFrame = 0;
    bool isMoving = false;
    bool isDead = false;

    [Header("Feedback")]
    [SerializeField] TrailRenderer trail = null;
    [SerializeField] ParticleSystem deathParticles = null;
    [SerializeField] AudioClip deathSFX = null;

    [Header("Required References")]
    [SerializeField] GameObject artToDisableOnDeath = null;

    Rigidbody rigidbodyComponent = null;
    Animator animator = null;
    AudioSource audioSource = null;

    void Start()
    {
        // setup our car defaults
        rigidbodyComponent = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // if player is dead, exit early
        if (isDead)
            return;

        CalculateMoveAmountThisFrame();
        CalculateTurnAmountThisFrame();

        if(animator.runtimeAnimatorController != null)
        {
            SetAnimationState();
        }
        if(trail != null)
        {
            HandleMaxSpeedTrail();
        }
    }

    // physics
    private void FixedUpdate()
    {
        Move();
        // only turn if currently moving
        if (moveAmountThisFrame != 0)
        {
            Turn();
        }
    }

    void CalculateMoveAmountThisFrame()
    {
        moveAmountThisFrame = Input.GetAxis("Vertical") * maxSpeed;
    }

    void CalculateTurnAmountThisFrame()
    {
        turnAmountThisFrame = Input.GetAxis("Horizontal") * maxTurnSpeed;
    }

    void Move()
    {
        // move the car rigidbody, based on move calculation
        Vector3 newMovement = transform.forward * moveAmountThisFrame * Time.deltaTime;
        rigidbodyComponent.MovePosition(rigidbodyComponent.position + newMovement);
    }

    void Turn()
    {
        // turn the car rigidbody, based on turn calculation
        Quaternion newRotation = Quaternion.Euler(0, turnAmountThisFrame * Time.deltaTime, 0);
        rigidbodyComponent.MoveRotation(rigidbodyComponent.rotation * newRotation);
    }

    void SetAnimationState()
    {
        // if we weren't moving before, but now have speed
        if(isMoving == false && moveAmountThisFrame != 0)
        {
            isMoving = true;
            animator.SetBool("isMoving", true);
        }
        // if we were moving before, and now do NOT have speed
        else if(isMoving == true && moveAmountThisFrame == 0)
        {
            isMoving = false;
            animator.SetBool("isMoving", false);
        }
    }

    void HandleMaxSpeedTrail()
    {
        // if max speed, and trail isn't already active, turn it on
        if(moveAmountThisFrame == maxSpeed && trail.emitting == false)
        {
            trail.emitting = true;
        }
        // if not max speed, and trail IS active, turn it off
        if(moveAmountThisFrame != maxSpeed && trail.emitting == true)
        {
            trail.emitting = false;
        }
    }

    public void Die()
    {
        isDead = true;
        DisableDeathObjects();

        if (deathSFX != null)
        {
            audioSource.PlayOneShot(deathSFX, audioSource.volume);
        }
        if (deathParticles != null)
        {
            deathParticles.Play();
        }
    }

    private void DisableDeathObjects()
    {
        artToDisableOnDeath.SetActive(false);

        rigidbodyComponent.detectCollisions = false;
        rigidbodyComponent.velocity = Vector3.zero;
        rigidbodyComponent.constraints = RigidbodyConstraints.FreezeAll;
    }
}

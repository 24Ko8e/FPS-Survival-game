using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;
    PlayerFootsteps playerFootsteps;

    float sprintVolume = 1f;
    float crouchVolume = 0.1f;
    float walkVolumeMin = 0.2f;
    float walkVolumeMax = 0.6f;
    float walkStepDistance = 0.4f;
    float sprintStepDistance = 0.25f;
    float crouchStepDistance = 0.5f;

    Vector3 move_direction;
    public float speed = 5f;
    float gravity = 20f;
    public float jump_force = 10f;
    float vertical_velocity;

    public float sprintSpeed = 10f;
    public float moveSpeed = 5f;
    public float crouchSpeed = 2f;
    Transform lookRoot;
    float standHeight = 1.6f;
    float crouchHeight = 1f;
    bool isCrouching = false;

    [SerializeField] Image staminaBar;
    float stamina = 100f;
    float staminaDepletionMultiplier = 15f;

    private void Awake()
    {
        lookRoot = transform.GetChild(0);
        characterController = GetComponent<CharacterController>();
        playerFootsteps = GetComponentInChildren<PlayerFootsteps>();
    }

    void Start()
    {
        playerFootsteps.volumeMin = walkVolumeMin;
        playerFootsteps.volumeMax = walkVolumeMax;
        playerFootsteps.stepDistance = walkStepDistance;
    }

    void Update()
    {
        MovePlayer();
        Sprint();
        Crouch();
    }

    void MovePlayer()
    {
        move_direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        move_direction = transform.TransformDirection(move_direction);
        move_direction *= speed * Time.deltaTime;

        ApplyGravity();
        characterController.Move(move_direction);
    }

    private void ApplyGravity()
    {
        vertical_velocity -= gravity * Time.deltaTime;
        PlayerJump();

        move_direction.y = vertical_velocity * Time.deltaTime;
    }

    private void PlayerJump()
    {
        if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            vertical_velocity = jump_force;
        }
    }

    void Sprint()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching && stamina > 0f && movement.magnitude > 0)
        {
            speed = sprintSpeed;

            playerFootsteps.stepDistance = sprintStepDistance;
            playerFootsteps.volumeMin = sprintVolume;
            playerFootsteps.volumeMax = sprintVolume;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            speed = moveSpeed;

            playerFootsteps.stepDistance = walkStepDistance;
            playerFootsteps.volumeMin = walkVolumeMin;
            playerFootsteps.volumeMax = walkVolumeMax;
        }
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching && movement.magnitude > 0)
        {
            stamina -= staminaDepletionMultiplier * Time.deltaTime;
            if (stamina <= 0f)
            {
                stamina = 0f;
                speed = moveSpeed;
                playerFootsteps.stepDistance = walkStepDistance;
                playerFootsteps.volumeMin = walkVolumeMin;
                playerFootsteps.volumeMax = walkVolumeMax;
            }
            setPlayerStaminaBar(stamina);
        }
        else if (stamina != 100f)
        {
            stamina += (staminaDepletionMultiplier / 2) * Time.deltaTime;
            setPlayerStaminaBar(stamina);

            if (stamina > 100f)
            {
                stamina = 100f;
            }
        }
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (isCrouching)
            {
                lookRoot.localPosition = new Vector3(0f, standHeight, 0f);
                speed = moveSpeed;

                playerFootsteps.volumeMin = walkVolumeMin;
                playerFootsteps.volumeMax = walkVolumeMax;
                playerFootsteps.stepDistance = walkStepDistance;

                isCrouching = false;
            }
            else
            {
                lookRoot.localPosition = new Vector3(0f, crouchHeight, 0f);
                speed = crouchSpeed;

                playerFootsteps.stepDistance = crouchStepDistance;
                playerFootsteps.volumeMin = crouchVolume;
                playerFootsteps.volumeMax = crouchVolume;

                isCrouching = true;
            }
        }
    }

    void setPlayerStaminaBar(float stamina)
    {
        stamina /= 100f;
        staminaBar.fillAmount = stamina;
    }
}

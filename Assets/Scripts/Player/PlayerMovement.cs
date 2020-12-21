using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;
    Vector3 move_direction;
    public float speed = 5f;
    float gravity = 20f;
    public float jump_force = 10f;
    float vertical_velocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        MovePlayer();
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
}

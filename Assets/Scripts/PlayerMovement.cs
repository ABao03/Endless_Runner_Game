// Author: Andrew Bao
// Date: 2/7/2025

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // The object that controls Player's physics.
    [SerializeField] private Rigidbody2D rb;
    
    // Controls the height of Player's jump.
    [SerializeField] private float jumpForce = 10f;

    [SerializeField] private LayerMask groundLayer;

    // Track if Player is currently in contact with the ground.
    [SerializeField] private Transform feetPos;
    
    // The distance that Player can be from the ground to count as being in contact with it.
    [SerializeField] private float groundDistance = 0.25f;

    // Controls the duration of Player's jump.
    [SerializeField] private float jumpTime = 0.3f;
    
    // Whether or not Player is currently grounded.
    private bool isGrounded = false;

    // Whether or not Player is currently in the air.
    private bool isJumping = false;

    // The length of time Player can keep jumping upwards for.
    private float jumpTimer;

    // Called every frame.
    // Controls Player movement.
    private void Update() {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);

        // Triggered when jump button is pressed.
        // Allows the characster to jump up if currently grounded.
        if (isGrounded && Input.GetButtonDown("Jump")) {
            isJumping = true;
            rb.linearVelocity = Vector2.up * jumpForce;
        }

        // Triggered when jump button is released.
        // Resets the isJumping variable and the jumpTimer.
        if (Input.GetButtonUp("Jump")) {
            isJumping = false;
            jumpTimer = 0;
        }
    }
}

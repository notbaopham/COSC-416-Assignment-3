using System;
using Unity.Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private CinemachineCamera freeLookCamera; // Changing the direction of movement as the camera
    [SerializeField] private float gravity = 1f;

    [SerializeField] private float dashSpeed = 10f;
    private Rigidbody rb;

    private Boolean canDoubleJump;
    private Boolean onGround;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Subscribing MovePlayerto the OnMove event
        inputManager.OnMove.AddListener(MovePlayer);
        inputManager.OnJump.AddListener(Jump);
        inputManager.OnDash.AddListener(Dash);
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Apply custom gravity based on gravityScale
        // Gravity is typically (0, -9.81, 0), but you can modify it by your scale factor
        Vector3 customGravity = new Vector3(0, -9.81f * gravity, 0);
        rb.AddForce(customGravity, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            onGround = true;  // Player is on the ground
        }
    }

    // Called when the player exits a collision
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            onGround = false;  // Player is no longer on the ground
            canDoubleJump = true;
        }
    }

    private void MovePlayer(Vector2 direction)
    {
        // Get the camera's forward and right vectors (ignoring vertical component)
        Vector3 forward = freeLookCamera.transform.forward;
        forward.y = 0f; // Remove vertical component to keep movement horizontal
        forward.Normalize();

        Vector3 right = freeLookCamera.transform.right;
        right.y = 0f; // Remove vertical component to keep movement horizontal
        right.Normalize();

        // Calculate the move direction relative to the camera's orientation
        Vector3 moveDirection = forward * direction.y + right * direction.x;

        // Apply force to the rigidbody for movement
        rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);
    }

    private void Jump(Vector3 direction)
    {
        if (onGround) {
            rb.AddForce(direction * jumpForce, ForceMode.Impulse);
        } else if (canDoubleJump) {
            rb.AddForce(direction * jumpForce, ForceMode.Impulse);
        }
    }

    private void Dash() {
        Vector3 forward = freeLookCamera.transform.forward;
        forward.y = 0f;  // Keep dash direction horizontal
        forward.Normalize();

        // Apply dash force in the direction the camera is facing
        rb.linearVelocity = new Vector3(forward.x * dashSpeed, rb.linearVelocity.y, forward.z * dashSpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

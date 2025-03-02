using System;
using Unity.Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private CinemachineCamera freeLookCamera; // Changing the direction of movement as the camera

    private Rigidbody rb;

    private Boolean onGround;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Subscribing MovePlayerto the OnMove event
        inputManager.OnMove.AddListener(MovePlayer);
        inputManager.OnJump.AddListener(Jump);
        rb = GetComponent<Rigidbody>();
        onGround = false;

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
        rb.AddForce(speed * moveDirection);
    }

    private void Jump(Vector3 jumpDirection)
    {
        if (onGround) {
            rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

}

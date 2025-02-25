using Unity.Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed;
    [SerializeField] private CinemachineCamera freeLookCamera; // Changing the direction of movement as the camera

    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Subscribing MovePlayerto the OnMove event
        inputManager.OnMove.AddListener(MovePlayer);
        rb = GetComponent<Rigidbody>();
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

    // Update is called once per frame
    void Update()
    {
        transform.forward = freeLookCamera.transform.forward;
        transform.rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y,0);
    }
}

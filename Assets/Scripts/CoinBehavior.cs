using UnityEngine;

public class CoinBehavior : MonoBehaviour
{

    private float rotationSpeed = 50f;
    public GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        gameManager.score++;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}

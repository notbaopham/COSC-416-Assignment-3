using System;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour {

    public UnityEvent<Vector2> OnMove = new UnityEvent<Vector2>();
    public UnityEvent<Vector3> OnJump = new UnityEvent<Vector3>();
    public UnityEvent OnResetPressed = new UnityEvent();
    public UnityEvent OnDash = new UnityEvent();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = Vector2.zero;
        Vector3 inputJump = Vector3.zero;
        if (Input.GetKey(KeyCode.A)) {
            input += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D)) {
            input += Vector2.right;
        }
        if (Input.GetKey(KeyCode.S)) {
            input += Vector2.down;
        }
        if (Input.GetKey(KeyCode.W)) {
            input += Vector2.up;
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            inputJump += Vector3.up;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            OnDash?.Invoke();
        }
        OnMove?.Invoke(input);
        OnJump?.Invoke(inputJump);

        if (Input.GetKeyDown(KeyCode.R))
        {
            OnResetPressed?.Invoke();
        }
    }
}
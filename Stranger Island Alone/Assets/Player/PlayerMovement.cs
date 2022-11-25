using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    float vertical;
    float horizontal;

    [Header("Movement")]
    public float moveSpeed = 0.2f;
    public float walkSpeed;
    public float sprintSpeed;

    [Header("Snicking")]
    public float snickingSpeed;
    public float snickingYScale;
    private float startYScale;

    [Header("Jump")]
    public float JumpForce = 1500f;

    public MovementState state;
    public enum MovementState
    {
        walking, sprinting, air, snicking
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startYScale = transform.localScale.y;
    }

    void FixedUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        if (vertical != 0 || horizontal != 0)
            movePlayer();
        SnickingMove();
    }
    
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            if (isGrounded())
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
            }
        stateHandler();
    }
    void stateHandler()
    {
        // Mode - Snicking
        if(isGrounded() && Input.GetKey(KeyCode.LeftControl))
        {
            state = MovementState.snicking;
            moveSpeed = snickingSpeed;
        }
        // Mode - Sprinting
        else if(isGrounded() && Input.GetKey(KeyCode.LeftShift))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        // Mode - Walking
        else if (isGrounded())
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        // Mode - Air
        else
        {
            state = MovementState.air;
        }
    }

    void movePlayer()
    {
        rb.AddForce((transform.forward * vertical + transform.right * horizontal)
                 * moveSpeed * 100f, ForceMode.Force);
        SpeedControl();
    }

    bool isGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.2f))
            return true;
        return false;
    }

    void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limittedVelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limittedVelocity.x, rb.velocity.y, limittedVelocity.y);
        }
    }

    void SnickingMove()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(transform.localScale.x, snickingYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }       
}

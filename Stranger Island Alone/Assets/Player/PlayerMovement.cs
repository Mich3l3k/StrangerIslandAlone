using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    public float sprintBonus;
    public float snickBonus;
    public float jumpHeight;

    float baseSprintBonus;
    float baseSnickBonus;

    private bool groundedPlayer;
    private Rigidbody rb;
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        characterController = gameObject.GetComponent<CharacterController>();

        baseSprintBonus = sprintBonus;
        baseSnickBonus = snickBonus;
    }

    // Update is called once per frame
    void Update()
    {
        /*groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f

        
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * Physics.gravity.y);
        }

        playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);*/
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            sprintBonus = baseSprintBonus;
        else
            sprintBonus = 1;

        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            snickBonus = baseSnickBonus;
        else
            snickBonus = 1;


        if (move != Vector3.zero)
        {
            //rb.velocity = (move * Time.deltaTime * playerSpeed * sprintBonus * snickBonus);
            characterController.Move(move);
        }
    }
}

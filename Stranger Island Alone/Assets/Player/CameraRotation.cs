using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;
using Cursor = UnityEngine.Cursor;

public class CameraRotation : MonoBehaviour
{
    GameObject player;

    //[SerializeField]
    //bool lockedCursor = true;
    
    float cameraVertical;
    float xRotation;
    float yRotation;

    public float mouseSens = 2f;

    // Start is called before the first frame update
    void Start()
    {
        //Find player object
        player = GameObject.FindGameObjectWithTag("Player");

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate camera vertical
        xRotation = Input.GetAxis("Mouse X") * mouseSens;
        yRotation = Input.GetAxis("Mouse Y") * mouseSens;

        cameraVertical -= yRotation;
        cameraVertical = Mathf.Clamp(cameraVertical, -90, 90f);
        transform.localEulerAngles = Vector3.right * cameraVertical;

        // Rotate player horizontal
        player.transform.Rotate(Vector3.up * xRotation);
    }
}

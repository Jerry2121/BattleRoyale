using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpectCameraController : MonoBehaviour {

    [SerializeField]
    float speed = 5f;
    [SerializeField]
    float lookSensitivity = 3f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Utility.WaitForSeconds(0.5f);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleCursor();

        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        Vector3 moveHorizontal = transform.right * xMove;
        Vector3 moveVertical = transform.forward * zMove;
        
        velocity = (moveHorizontal + moveVertical) * speed;

        //Calculate rotation as a 3D vector for turning
        float yRot = Input.GetAxisRaw("Mouse X");
        float xRot = Input.GetAxisRaw("Mouse Y");

        rotation = new Vector3(-xRot, yRot, 0f) * lookSensitivity;
    }

    //Runs every physics iteration
    void FixedUpdate()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
            return;

        PerformMovement();
        PerformRotation();
    }

    //Move based on the velocity variable
    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    //Rotate based on the rotation variable
    void PerformRotation()
    {
        //rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        transform.Rotate(rotation);
        //cancel z rotation
        float z = transform.eulerAngles.z;
        transform.Rotate(0, 0, -z);
    }

    void ToggleCursor()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

}
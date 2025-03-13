using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float crouchSpeed = 3f;
    public float sprint = 7f;
    public float defaultSpeed = 5f;
    public float jumpStrength = 10f;
    public float gravity = -10f;
    public float mouseSensitivity = 2f;

    public bool isCrouching = false;
    public bool isRunning = false;

    //isGrounded
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    private Vector3 velocity;

    public Camera cam;
    public float cameraMovement;

    private CharacterController controller;
    public Rigidbody rb;
    private float xRotation = 0f;
    public Transform playerCamera; // Référence manuelle à la caméra

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Verrouiller le curseur au centre de l'écran
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Move();
        LookAround();
        Sprint();
        Crouch();
        Jump();
        Gravity();
    }

    void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isCrouching == false)
        {
            isRunning = true;
            speed = sprint;
        }
        else if (Input.GetKey(KeyCode.LeftShift) == false && isCrouching == false)
        {
            isRunning = false;
            speed = defaultSpeed;
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, 0f, cam.transform.localPosition.z);
        }
    }

    void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl) && isRunning == false)
        {
            isCrouching = true;
            speed = crouchSpeed;
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, -0.7f, cam.transform.localPosition.z);
        }
        else if (Input.GetKey(KeyCode.LeftControl) == false && isRunning == false)
        {
            isCrouching = false;
            speed = defaultSpeed;
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, 0f, cam.transform.localPosition.z);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpStrength * -1.5f * gravity);
        }
    }

    void Gravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }


    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if(isRunning == true)
        {
            cam.transform.position = cam.transform.position ;
        }
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotation du corps (gauche/droite)
        transform.Rotate(Vector3.up * mouseX);

        // Rotation de la caméra (haut/bas)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Empêche de retourner la tête à 180°

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    
}


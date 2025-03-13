using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int hp = 100;
    public int stamina = 100;
    public int sound = 100;
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float crouchSpeed = 2.5f;
    public float jumpHeight = 1.5f; // Hauteur du saut
    public float mouseSensitivity = 2f;

    private float speed;
    private CharacterController controller;
    private float xRotation = 0f;
    public Transform playerCamera;

    private bool isCrouching = false;
    private float standingHeight = 2f;
    private float crouchingHeight = 1.2f;

    private Vector3 velocity;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    private bool isRunning = false;

    // Head bobbing
    public Camera cam;
    public float bobSpeed = 10f;
    public float bobSprintSpeed = 12f;
    public float bobAmount = 0.2f;
    private float defaultCamHeight;
    private float targetCamHeight;
    private float timer = 0;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = walkSpeed;

        Cursor.lockState = CursorLockMode.Locked;
        targetCamHeight = defaultCamHeight;
    }

    void Update()
    {
        HandleMovement();
        HandleLook();
        HandleSprint();
        HandleCrouch();
        HandleJump();
        ApplyGravity();
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (move.magnitude > 0.1f && isGrounded && !isCrouching)
        {
            // Increment timer smoothly
            timer += Time.deltaTime * (isRunning ? bobSprintSpeed : bobSpeed);

            float bobOffset = Mathf.Sin(timer) * bobAmount;
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, targetCamHeight + bobOffset, cam.transform.localPosition.z);
        }
        else
        {
            // Smoothly reset camera position when stopping movement
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, Mathf.Lerp(cam.transform.localPosition.y, targetCamHeight, Time.deltaTime * 5f), cam.transform.localPosition.z);
        }
    }

    void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void HandleSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            speed = sprintSpeed;
            targetCamHeight = defaultCamHeight + 0.1f; // Slightly raise camera while sprinting
            isRunning = true;
        }
        else if (!isCrouching)
        {
            speed = walkSpeed;
            targetCamHeight = defaultCamHeight; // Reset base camera height
            isRunning = false;
        }
    }

    void HandleCrouch()
    {
        if (Input.GetKey(KeyCode.LeftControl)) // Si on maintient Ctrl
        {
            isCrouching = true;
            controller.height = crouchingHeight;
            speed = crouchSpeed;
        }
        else // Quand on relâche Ctrl
        {
            targetCamHeight = defaultCamHeight; // Reset base camera height
            isCrouching = false;
            controller.height = standingHeight;

            // Vérifier si on sprint après s'être accroupi
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = sprintSpeed;
            }
            else
            {
                speed = walkSpeed;
            }
        }
    }



    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Formule du saut réaliste
        }
    }

    void ApplyGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
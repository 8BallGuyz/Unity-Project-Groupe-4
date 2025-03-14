using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public stats_manager stats;

    public int hp = 100;
    public int maxhp = 100;
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float defaultSprintSpeed;
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

    [Space(10)]
    [Header("STAMINA STATS")]
    // Stamina Manager
    public int stamina = 100;
    public int staminaCap = 100;
    public int staminaMin = 0;
    public float timerStamina = 0;
    public float endStamina = 0.1f;
    public float timerStamina2 = 0;
    public float endStamina2 = 0.1f;
    private bool staminaLose = false;
    private bool staminaBurnout = false;
    private bool staminaRegen = false;
    public float timerBurnout = 0;
    public float endBurnout = 3;

    [Space(10)]
    [Header("SOUND STATS")]
    // Sound Stat Manager
    public float sound = 100;
    public int soundCap = 100;
    public int soundMin = 0;
    public float timerSound = 0;
    public float endSound = 0.2f;
    public float timerSound2 = 0;
    public float endSound2 = 0.2f;
    public float timerSound3 = 0;
    public float endSound3 = 0.2f;
    private bool soundController = false; 

    void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = walkSpeed;
        defaultSprintSpeed = sprintSpeed; // Initialize defaultSprintSpeed
        defaultCamHeight = cam.transform.localPosition.y; // Initialize defaultCamHeight
        Cursor.lockState = CursorLockMode.Locked;
        targetCamHeight = defaultCamHeight;

        stats = FindObjectOfType<stats_manager>();
        stats.SetMaxStaminaUI(staminaCap);
        stats.SetMaxSoundUI(soundCap);
        stats.SetMaxHpUI(maxhp);
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

    void HandleHP() // NOT SET YET
    {
        stats.HpUI(hp);
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        bool moving = (x != 0 || z != 0); // Check if player is moving

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if(moving && !isRunning)
        {

            timerSound3 = timerSound3 + Time.deltaTime;
            if(timerSound3 >= endSound3)
            {
                sound = sound + 1;
                timerSound3 = 0;
            }
        }

        // Fixed head bobbing to work with both walking and sprinting
        if (move.magnitude > 0.1f && isGrounded && !isCrouching)
        {
            // Use the appropriate bob speed based on running state
            float currentBobSpeed = isRunning ? bobSprintSpeed : bobSpeed;

            // Increment timer using the current bob speed
            timer += Time.deltaTime * currentBobSpeed;

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
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        bool moving = (x != 0 || z != 0); // Check if player is moving
        stats.StaminaUI(stamina);
        stats.SoundUI(sound);

        if (Input.GetKey(KeyCode.LeftShift) && isCrouching == false && staminaBurnout == false && stamina > 0 && move.magnitude > 0.1f)
        {
            speed = sprintSpeed;
            targetCamHeight = defaultCamHeight + 0.1f; // Slightly raise camera while sprinting
            isRunning = true;
            staminaLose = true;
            staminaRegen = false;
            stats.StaminaUI(stamina);

        }
        else if (isCrouching == false)
        {
            speed = walkSpeed;
            targetCamHeight = defaultCamHeight; // Reset base camera height
            isRunning = false;
            staminaLose = false;
            staminaRegen = true;
            timerStamina = 0;
        }

        if (stamina == 0)
        {
            staminaLose = false;
            staminaRegen = false;
            staminaBurnout = true;
            timerStamina = 0;
            sprintSpeed = walkSpeed;
        }
        else
        {
            sprintSpeed = defaultSprintSpeed;
        }


        // Stamina Usage
        if (staminaLose == true && staminaBurnout == false && staminaRegen == false)
        {
            timerStamina += Time.deltaTime;
            if (timerStamina >= endStamina && stamina > staminaMin)
            {
                stamina = stamina - 1;
                timerStamina = 0;
            }
        }

        // Stamina Regen
        if (staminaRegen == true && staminaLose == false && staminaBurnout == false)
        {
            timerStamina2 += Time.deltaTime;
            if (timerStamina2 >= endStamina && stamina < staminaCap)
            {
                stamina = stamina + 1;
                timerStamina2 = 0;
            }
        }

        // When the player reaches 0
        if (staminaBurnout == true)
        {
            timerBurnout = timerBurnout + Time.deltaTime;
            if (timerBurnout >= endBurnout)
            {
                staminaBurnout = false;
                staminaRegen = true;
                timerBurnout = 0;
                stamina = stamina + 1;
                speed = walkSpeed;
                isRunning = false;
            }
        }

        if (isRunning && soundController == false) // Sound goes up when running
        {
            timerSound2 += Time.deltaTime;
            if (timerSound2 >= endSound2 )
            {
                sound = Mathf.Min(sound + 1, soundCap);
                timerSound2 = 0;
            }
        }
        else if (!isCrouching && moving && soundController == false) // Sound goes up when walking
        {
            timerSound3 += Time.deltaTime;
            if (timerSound3 >= endSound3)
            {
                sound = Mathf.Min(sound + 1, soundCap);
                timerSound3 = 0;
            }
        }
        else if (!moving && soundController == false) // Sound goes down only when fully idle
        {
            timerSound += Time.deltaTime;
            if (timerSound >= endSound)
            {
                sound = Mathf.Max(sound - 1, soundMin);
                timerSound = 0;
            }
        }
        else if (isCrouching && !moving && soundController == false) // Sound also decreases when crouching and stationary
        {
            timerSound += Time.deltaTime;
            if (timerSound >= endSound)
            {
                sound = Mathf.Max(sound - 1, soundMin);
                timerSound = 0;
            }
        }
        else if (isCrouching && moving) // Sound also decreases when crouching and stationary
        {
            soundController = true;
            endSound = 0.35f;
            timerSound += Time.deltaTime;
            if (timerSound >= endSound && sound > soundMin)
            {
                sound = sound - 1;
                timerSound = 0;
            }
        }
    }

    void HandleCrouch()
    {
        if (Input.GetKey(KeyCode.LeftAlt)) // Si on maintient Alt
        {
            isCrouching = true;
            controller.height = crouchingHeight;
            speed = crouchSpeed;
        }
        else // Quand on relâche Alt
        {
            soundController = false;
            endSound = 0.5f;
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
            sound = sound + 75;
            if(sound > 100)
            {
                sound = 100;
            }
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
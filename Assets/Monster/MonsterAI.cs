using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MonsterAI : MonoBehaviour

{

    private Transform currentWaypoint;


    public bool growing = true;
    public float timerDeath = 0;
    public float endDeath = 3.5f;
    private bool deathDetector = false;

    public AudioClip footstepsSound;  // Son des pattes (à assigner dans l'Inspector)
    private AudioSource footstepsAudioSource;  // Source audio dédiée aux pas
    public float maxFootstepVolume = 1.0f; // Volume max du bruit de pas
    public float minFootstepVolume = 0.2f; // Volume min du bruit de pas
    public float hearingRange = 15f; // Distance à laquelle le bruit atteint son max

    public AudioClip poursuite;
    public AudioClip grow;
    public AudioClip screamerSound;  // Son du screamer (assigner dans l'Inspector)
    public GameObject screamerImage; // Image du screamer (UI à activer)
    public float screamerDuration = 2f; // Temps avant l'arrêt du jeu
    private AudioSource audioSource;

    private CharacterController playerController;
    private PlayerMovement playerMovement;

    public Renderer eyeRenderer;
    public Renderer eyeRenderer2;
    public Material eyesNormal;
    public Material eyesChase;

    public Transform player;
    public Transform head;
    public GameObject segmentPrefab;
    public int segmentCount = 8;
    public float segmentSpacing = 1.5f;
    public float moveSpeed = 6.5f;
    public float patrolRange = 20f;
    public float rotationSpeed = 10f;
    public float chaseDuration = 1f;

    private NavMeshAgent agent;
    private List<Transform> segments = new List<Transform>();
    private bool isChasing = false;
    private float chaseTimer = 0f;
    private bool playerCaptured = false;
    public float time = 0;
    public float end = 3f;

    void Start()
    {

        // Crée un nouvel AudioSource pour le bruit de pattes
        footstepsAudioSource = gameObject.AddComponent<AudioSource>();
        footstepsAudioSource.clip = footstepsSound;
        footstepsAudioSource.loop = true;
        footstepsAudioSource.volume = minFootstepVolume;
        footstepsAudioSource.spatialBlend = 1.0f; // 3D sound
        footstepsAudioSource.Play();


        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.autoBraking = false;

        playerController = player.GetComponent<CharacterController>();
        playerMovement = player.GetComponent<PlayerMovement>();

        InvokeRepeating("SetNewDestination", 0f, 1f);

        Transform lastSegment = transform;
        for (int i = 0; i < segmentCount; i++)
        {
            GameObject segment = Instantiate(segmentPrefab, lastSegment.position - lastSegment.forward * segmentSpacing, Quaternion.identity);
            segments.Add(segment.transform);
            lastSegment = segment.transform;
        }
    }

    void Update()
    {
        if(deathDetector == true) 
        {
            timerDeath = timerDeath + Time.deltaTime;
            if(timerDeath >= endDeath) 
            {
                SceneManager.LoadScene("GameOver");
                Cursor.lockState = CursorLockMode.None;
            }
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float volume = Mathf.Lerp(maxFootstepVolume, minFootstepVolume, distanceToPlayer / hearingRange);
        footstepsAudioSource.volume = Mathf.Clamp(volume, minFootstepVolume, maxFootstepVolume);

        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();

        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("🚨 Bruit détecté ! Le monstre attaque !");
            Debug.Log(playerMovement.sound);
        }


        if (playerMovement.sound == 100)
        {

            growMonster();

            if (hasScreamed == false)
            {
                Debug.Log(playerMovement.sound);
                StartChasing();
            }

        }

        if (isChasing)
        {
            head.LookAt(player.position);
            agent.SetDestination(player.position);

            float distance = Vector3.Distance(transform.position, player.position);

            // if (distance > 40.0f && !playerCaptured)
            // {
                
            // }
            if (distance < 4.0f && !playerCaptured)
            {
                Debug.Log("💀 Le scolopendre a attrapé le joueur !");
                TriggerScreamer();
            }

            chaseTimer -= Time.deltaTime;
            if (chaseTimer <= 0)
            {
                StopChasing();
            }
        }
        else
        {
            head.rotation = Quaternion.Slerp(head.rotation, transform.rotation, Time.deltaTime * rotationSpeed);
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                SetNewDestination();
            }
        }

        MoveSegments();
    }


    void growMonster()
    {

        if (isChasing == false)
        {
            growing = true;
        }

        if (growing == true)
        {
            audioSource.PlayOneShot(grow);
            growing = false;
        }

        Debug.Log(growing);
    }

    void SetNewDestination()
    {
        if (isChasing) return;

        Vector3 randomDirection = transform.forward * patrolRange * 0.5f + Random.insideUnitSphere * patrolRange * 0.5f;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRange, 1))
        {
            agent.SetDestination(hit.position);
        }
    }

    void MoveSegments()
    {
        Vector3 previousPosition = transform.position;
        Quaternion previousRotation = transform.rotation;

        for (int i = 0; i < segments.Count; i++)
        {
            Vector3 tempPos = segments[i].position;
            Quaternion tempRot = segments[i].rotation;

            segments[i].position = Vector3.Lerp(segments[i].position, previousPosition, Time.deltaTime * moveSpeed);
            segments[i].rotation = Quaternion.Slerp(segments[i].rotation, previousRotation, Time.deltaTime * rotationSpeed);

            previousPosition = tempPos;
            previousRotation = tempRot;
        }
    }

    void StartChasing()
    {
        isChasing = true;
        chaseTimer = chaseDuration;
        agent.SetDestination(player.position);

        if (eyeRenderer != null && eyesChase != null)
        {
            eyeRenderer.material = eyesChase;
            eyeRenderer2.material = eyesChase;
        }
    }

    void StopChasing()
    {
        isChasing = false;

        if (eyeRenderer != null && eyesNormal != null)
        {
            eyeRenderer.material = eyesNormal;
            eyeRenderer2.material = eyesNormal;
        }

        SetNewDestination();
    }

    private bool hasScreamed = false;

    void TriggerScreamer()
    {
        if (hasScreamed) return;
        hasScreamed = true;
        playerCaptured = true;

        Debug.Log("🛑 Le scolopendre attrape le joueur !");

        if (screamerImage != null)
        {
            screamerImage.SetActive(true);
        }

        if (screamerSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(screamerSound);
        }

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // ✅ On arrête la chasse AVANT de jouer l’animation du screamer
        StopChasing();

        // ✅ On force la tête du scolopendre à arrêter de regarder le joueur
        head.rotation = Quaternion.identity;

        // Ralentir le scolopendre
        agent.speed = moveSpeed * 0.6f;  // Réduit à 60% de sa vitesse normale

        StartCoroutine(CapturePlayer());
    }


    IEnumerator CapturePlayer()
    {
        float shakeDuration = 3f;  // Durée du tremblement
        float shakeIntensity = 0.3f;  // Intensité du tremblement
        float elapsed = 0f;

        Vector3 cameraOffset = head.forward * 1.5f - head.forward * -0.5f; // Position par défaut de la caméra

        while (hasScreamed)
        {
            // Position de base du joueur
            Vector3 basePosition = head.position + head.forward * 1.5f - head.forward * -0.5f;

            // Définir la position de base de la caméra derrière la tête du scolopendre
            Vector3 targetPosition = head.position + cameraOffset;

            // Raycast pour vérifier si un mur bloque la caméra
            RaycastHit hit;
            if (Physics.Raycast(head.position, cameraOffset.normalized, out hit, cameraOffset.magnitude))
            {
                // Si un obstacle est détecté, placer la caméra juste avant la collision
                targetPosition = hit.point - cameraOffset.normalized * 0.1f;
            }

            // Tremblement de la caméra pendant les 2 premières secondes
            if (elapsed < shakeDuration)
            {
                Vector3 shakeOffset = Random.insideUnitSphere * shakeIntensity;
                player.position = basePosition + shakeOffset;
                elapsed += Time.deltaTime;
            }
            else
            {
                player.position = basePosition;  // Une fois le tremblement terminé, position normale
            }

            // La caméra du joueur regarde en permanence la tête du scolopendre
            player.LookAt(head.position);
            deathDetector = true;

            yield return null;
        }

    }

    void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}

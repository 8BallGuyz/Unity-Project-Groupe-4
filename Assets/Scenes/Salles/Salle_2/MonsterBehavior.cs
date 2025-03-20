using UnityEngine;
using System.Collections; // Nécessaire pour StartCoroutine()
using UnityEngine.UI; // Nécessaire pour afficher l'image du screamer
using UnityEngine.SceneManagement;

public class MonsterBehavior : MonoBehaviour
{
    public Transform player; // Le joueur
    public float speed = 1f; // Vitesse du monstre
    public float detectionRange = 20f; // Distance à laquelle il commence à suivre
    public float attackRange = 2f; // Distance à laquelle il attaque
    public AudioSource monsterAudio;
    public AudioClip[] creepySounds; // Ajoute plusieurs sons effrayants
    public AudioClip teleportSound; // Son spécifique pour la téléportation
    public AudioClip screamerSound; // Son du screamer
    public Image screamerImage; // Référence à l’image du screamer dans l’UI

    private float timeSinceLastSound = 0f;
    private float soundInterval = 5f; // Joue un son toutes les 5 sec environ
    private bool isVisible = false; // Le monstre commence invisible
    private bool hasTeleported = false; // Pour éviter la téléportation en boucle
    private bool hasScreamed = false; // Pour éviter plusieurs screamers

    private PlayerMovement playerMovement;
    private float normalSpeed;
    private float sprintSpeedMultiplier = 10f; 
    private float rushMultiplier = 100f; 
    private float timeStationary = 0f;
    private bool isRushing = false;
    private bool hasRushed = false;

    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        normalSpeed = speed; 

        // Cache le monstre au début
        SetMonsterVisible(false);

                // Cache l’image du screamer au début
        if (screamerImage != null)
            screamerImage.enabled = false;
    }

    void Update()
    {

        // Attendre que la lune tombe avant d'activer le monstre
        if (!ArcDeCercleLune.luneTombee) return;


        float distance = Vector3.Distance(transform.position, player.position);

        // Gestion de l'invisibilité
        if (distance > attackRange * 8)
        {
            if (isVisible)
            {
                isVisible = false;
                SetMonsterVisible(false);
            }
        }
        else
        {
            if (!isVisible)
            {
                isVisible = true;
                SetMonsterVisible(true);
            }
        }

        // Gestion de la téléportation derrière le joueur
        if (distance > detectionRange && !hasTeleported)
        {
            StartCoroutine(TeleportBehindPlayer());
        }
        else if (distance <= detectionRange)
        {
            hasTeleported = false; // Réinitialise pour la prochaine fois que le joueur sort
        }

        // Si le joueur est proche, le monstre commence à traquer
        if (distance < detectionRange && !hasScreamed)
        {
            FollowPlayer();
        }

        // Joue des sons effrayants de temps en temps
        timeSinceLastSound += Time.deltaTime;
        if (timeSinceLastSound >= soundInterval)
        {
            PlayRandomCreepySound();
            timeSinceLastSound = 0f;
        }
    }

    void FollowPlayer()
    {
        if (player == null) return;

        float playerSpeed = playerMovement.controller.velocity.magnitude; 

        if (playerMovement.speed < 9f && !isRushing) 
        {
            speed = normalSpeed * sprintSpeedMultiplier;
            hasRushed = false; 
        }
        else if (!isRushing)
        {
            speed = normalSpeed * 2;
        }

        if (playerMovement.controller.velocity.magnitude == 0)
        {
            timeStationary += Time.deltaTime;

            if (timeStationary >= 2f && !hasRushed)
            {
                speed = normalSpeed * rushMultiplier;
                hasRushed = true;
                isRushing = true;
            }
        }
        else
        {
            timeStationary = 0f; 
        }

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        transform.LookAt(player);
    }

    void SetMonsterVisible(bool visible)
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = visible;
        }
    }

    void PlayRandomCreepySound()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < detectionRange)
        {
            if (monsterAudio != null && creepySounds.Length > 0)
            {
                int index = Random.Range(0, creepySounds.Length);
                monsterAudio.spatialBlend = 1.0f;
                monsterAudio.PlayOneShot(creepySounds[index]);
            }
        }
    }

    IEnumerator TeleportBehindPlayer()
    {
        hasTeleported = true; // Empêche de téléporter en boucle

        yield return new WaitForSeconds(2f); // Attend 2 secondes avant de se téléporter

        Vector3 behindPlayer = player.position - player.forward * 10f; // Position 10 unités derrière le joueur
        transform.position = behindPlayer; // Téléportation
        transform.LookAt(player); // Regarde le joueur

        SetMonsterVisible(true); // Rend le monstre visible

        // Joue un son effrayant si défini
        if (monsterAudio != null && teleportSound != null)
        {
            monsterAudio.PlayOneShot(teleportSound);
        }

        yield return new WaitForSeconds(4f); // Attend encore 4 secondes avant de reprendre ses mouvements
    }


    // Détection du joueur dans le BoxCollider du screamer
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasScreamed)
        {
            StartCoroutine(TriggerScreamer());
        }
    }

    IEnumerator TriggerScreamer()
    {
        hasScreamed = true;

        // Désactiver le mouvement du joueur
        playerMovement.enabled = false;

        // Stopper le monstre
        speed = 0;
        isRushing = false;

        // Jouer le son de screamer
        if (monsterAudio != null && screamerSound != null)
        {
            monsterAudio.PlayOneShot(screamerSound);
        }

        // Afficher l'image du screamer
        if (screamerImage != null)
        {
            screamerImage.enabled = true;
        }

        yield return new WaitForSeconds(4f); // Temps du screamer

        SceneManager.LoadScene("GameOver");
        Cursor.lockState = CursorLockMode.None;

        // // Cacher l'image du screamer
        // if (screamerImage != null)
        // {
        //     screamerImage.enabled = false;
        // }

        // // Réactiver le mouvement du joueur
        // playerMovement.enabled = true;

        // // Le monstre peut recommencer à traquer
        // speed = normalSpeed;
    }
}

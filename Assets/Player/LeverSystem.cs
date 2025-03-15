using UnityEngine;
using UnityEngine.UI;

public class LeverSystem : MonoBehaviour
{
    public Transform playerCamera; // Caméra du joueur
    public float detectionDistance = 3f; // Distance d’interaction
    public LayerMask leverLayer; // Couches détectées
    public GameObject interactionText; // Texte "E pour activer"
    
    private Animator leverAnimator;
    private Transform currentLever;
    private FanSystem currentFan;
    private LeverState currentLeverState; // 🔹 Ajout pour gérer l’état du levier

    void Start()
    {
        interactionText.SetActive(false);
    }

    void Update()
    {
        CheckForLever();

        if (currentLever != null && Input.GetKeyDown(KeyCode.E))
        {
            ActivateLever();
        }
    }

    void CheckForLever()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, detectionDistance, leverLayer))
        {
            if (hit.collider.CompareTag("Lever"))
            {
                // 🔹 Récupère l'état du levier
                currentLeverState = hit.collider.GetComponent<LeverState>();

                // 🔹 Si le levier est déjà activé, on ne peut plus interagir avec lui
                if (currentLeverState != null && currentLeverState.IsActivated)
                {
                    interactionText.SetActive(false);
                    currentLever = null;
                    currentFan = null;
                    return;
                }

                interactionText.SetActive(true);
                currentLever = hit.collider.transform;

                // 🔹 Trouve le ventilateur lié à ce levier
                currentFan = hit.collider.GetComponent<LeverFanLink>()?.linkedFan;
                return;
            }
        }

        // 🔹 Si on ne regarde plus un levier valide, reset
        interactionText.SetActive(false);
        currentLever = null;
        currentFan = null;
    }

    void ActivateLever()
    {
        if (currentLever == null || currentLeverState == null) return;

        // 🔹 Active l'animation du levier
        leverAnimator = currentLever.GetComponent<Animator>();
        if (leverAnimator) leverAnimator.SetTrigger("Activate");

        // 🔹 Active l'animation du ventilateur correspondant
        if (currentFan != null && currentFan.fanAnimator != null)
        {
            currentFan.fanAnimator.SetTrigger("Spin");
        }

        // 🔹 Marque ce levier comme activé UNIQUEMENT pour lui
        currentLeverState.IsActivated = true;

        // 🔹 Désactive le texte après activation
        interactionText.SetActive(false);
        currentLever = null;
        currentFan = null;
    }
}

using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ChaseTrigger : MonoBehaviour
{
    public AudioSource newAmbiance; // Nouvelle musique d'ambiance
    public MonsterAI monsterAI; // Référence du scolopendre
    // public ZoneLightTrigger zoneLightTrigger;
    public Transform spawnPoint; // Point où il apparaît
    public Transform lookPoint; // Point où le joueur doit regarder
    public float cameraTurnSpeed = 2f; // Vitesse de rotation de la caméra
    public float rotationDuration = 1f; // Temps pour tourner la caméra
    public float waitTime = 0.5f; // Pause avant de récupérer les contrôles

    private PlayerMovement player;
    private AudioSource currentAmbiance;
    private bool isChaseActive = false;
    private Collider triggerCollider;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        if (player == null)
        {
            Debug.LogError("❌ ERREUR : Aucun PlayerMovement trouvé dans la scène !");
            return;
        }

        if (player.playerCamera == null)
        {
            Debug.LogError("❌ ERREUR : playerCamera est NULL ! Vérifie l'Inspector.");
            return;
        }

        currentAmbiance = GameObject.Find("Ambiance")?.GetComponent<AudioSource>();
        triggerCollider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isChaseActive)
        {
            if (player == null || player.playerCamera == null || lookPoint == null)
            {
                Debug.LogError("❌ ERREUR : Une variable est NULL, vérifie l'Inspector !");
                return;
            }

            StartCoroutine(ChaseSequence());
        }
    }

    IEnumerator ChaseSequence()
    {

        // zoneLightTrigger.currentZone = "Lab";
        // zoneLightTrigger.StopAllCoroutines();
        // zoneLightTrigger.StartCoroutine(zoneLightTrigger.ChangeLight(zoneLightTrigger.labIntensity, zoneLightTrigger.labColor, zoneLightTrigger.labRange));

        isChaseActive = true;
        triggerCollider.enabled = false;

        if (currentAmbiance) currentAmbiance.Stop();
        if (newAmbiance) newAmbiance.Play();

        player.enabled = false;
        player.GetComponent<Rigidbody>().isKinematic = true;

        Transform camTransform = player.playerCamera;
        if (camTransform == null || lookPoint == null)
        {
            Debug.LogError("❌ ERREUR : camTransform ou lookPoint est NULL !");
            yield break;
        }

        Quaternion initialRotation = camTransform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(lookPoint.position - camTransform.position);

        // 🔄 Rotation vers le lookPoint
        yield return RotateCamera(camTransform, targetRotation, rotationDuration);

        // Lancer la chasse
        if (monsterAI != null && spawnPoint != null)
        {
            monsterAI.GetComponent<NavMeshAgent>().Warp(spawnPoint.position);
            monsterAI.chaseDuration = 6f;
            monsterAI.agent.speed = 6.1f;
            monsterAI.StartChasing();
        }
        else
        {
            Debug.LogError("❌ ERREUR : scolopendre ou spawnPoint est NULL !");
        }

        // ⏸️ Pause pour un effet naturel
        yield return new WaitForSeconds(2f);

        yield return new WaitForSeconds(waitTime);

        // 🔄 Retour à la rotation initiale
        yield return RotateCamera(camTransform, initialRotation, rotationDuration);

        // ✅ Réactivation des contrôles du joueur
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.enabled = true;
        Debug.Log("✅ Contrôles du joueur réactivés !");
    }

    IEnumerator RotateCamera(Transform camTransform, Quaternion targetRotation, float duration)
    {
        Quaternion startRotation = camTransform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration; // Normalisation du temps
            camTransform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            elapsedTime += Time.deltaTime; // ⬅️ Fix : Pas de multiplication par `cameraTurnSpeed`
            yield return null;
        }

        camTransform.rotation = targetRotation; // Correction finale
    }
}

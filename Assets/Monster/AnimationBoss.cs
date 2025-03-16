using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AnimationBossTrigger : MonoBehaviour
{
    public AudioSource newAmbiance;
    public MonsterAI monsterAI;
    public Transform spawnPoint;
    public Transform targetPoint; // Position où le scolopendre doit aller avant de repartir librement
    public Transform lookPointEntrance;
    public Transform lookPointCeiling;
    public GameObject closingGate;
    public float gateDropDuration = 1.5f;
    public float rotationDuration = 1f;

    private PlayerMovement player;
    private AudioSource currentAmbiance;
    private bool isSequenceActive = false;
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
        if (other.CompareTag("Player") && !isSequenceActive)
        {
            StartCoroutine(BossSequence());
        }
    }

    IEnumerator BossSequence()
    {

        isSequenceActive = true;
        triggerCollider.enabled = false;

        if (currentAmbiance) currentAmbiance.Stop();
        if (newAmbiance) newAmbiance.Play();

        player.enabled = false;
        player.GetComponent<Rigidbody>().isKinematic = true;

        Transform camTransform = player.playerCamera;
        Quaternion initialRotation = camTransform.rotation;

        // 🔄 Regarde l'entrée
        Quaternion lookAtEntrance = Quaternion.LookRotation(lookPointEntrance.position - camTransform.position);
        yield return RotateCamera(camTransform, lookAtEntrance, rotationDuration);

        // 🛑 Fermeture de la grille (animation en parallèle)
        StartCoroutine(CloseGate());

        // 🕷️ Apparition et déplacement du scolopendre (lancé en parallèle)
        if (monsterAI != null && spawnPoint != null && targetPoint != null)
        {
            monsterAI.chaseDuration = 1.2f;
            monsterAI.keyObject.SetActive(true);
            monsterAI.GetComponent<NavMeshAgent>().Warp(spawnPoint.position);
            yield return null; // Attendre un frame pour que le NavMesh se mette à jour
            StartCoroutine(MoveMonster(monsterAI, targetPoint.position));
        }
        else
        {
            Debug.LogError("❌ ERREUR : scolopendre, spawnPoint ou targetPoint est NULL !");
        }

        yield return new WaitForSeconds(1f);

        // 🔄 Regarde le plafond
        Quaternion lookAtCeiling = Quaternion.LookRotation(lookPointCeiling.position - camTransform.position);
        yield return RotateCamera(camTransform, lookAtCeiling, rotationDuration);

        yield return new WaitForSeconds(2f);

        // 🔄 Retour à la rotation initiale
        yield return RotateCamera(camTransform, initialRotation, rotationDuration);

        // ✅ Réactivation des contrôles du joueur
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.enabled = true;
        Debug.Log("✅ Contrôles du joueur réactivés !");
    }

    IEnumerator CloseGate()
    {
        Vector3 startPos = closingGate.transform.position;
        Vector3 targetPos = new Vector3(startPos.x, startPos.y - 3, startPos.z);

        float elapsedTime = 0f;
        while (elapsedTime < gateDropDuration)
        {
            closingGate.transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / gateDropDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        closingGate.transform.position = targetPos;
        Debug.Log("🚪 Grille fermée !");
    }

    IEnumerator RotateCamera(Transform camTransform, Quaternion targetRotation, float duration)
    {
        Quaternion startRotation = camTransform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            camTransform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        camTransform.rotation = targetRotation;

    }

    IEnumerator MoveMonster(MonsterAI monster, Vector3 targetPosition)
    {
        NavMeshAgent agent = monster.GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("❌ ERREUR : Le scolopendre n'a pas de NavMeshAgent !");
            yield break;
        }

        agent.SetDestination(targetPosition);

        // Attendre une frame pour que le chemin soit calculé
        yield return new WaitUntil(() => !agent.pathPending);


        // Attente jusqu'à l'arrivée au point cible
        while (!agent.pathPending && agent.remainingDistance > agent.stoppingDistance)
        {
            Debug.Log($"Scolopendre en déplacement : {transform.position}, distance restante : {Vector3.Distance(transform.position, targetPoint.position)}");
            yield return null;
        }

    }
}

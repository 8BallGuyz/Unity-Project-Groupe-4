using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
    public GameObject interactUI; // UI "E pour ouvrir"
    public Animator doorAnimator; // Référence à l'Animator de la porte
    private bool isPlayerNear = false;
    private bool isTransitioning = false;

    private void Start()
    {
        interactUI.SetActive(false); // Cache le message au début
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            interactUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            interactUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && !isTransitioning)
        {
            interactUI.SetActive(false); // Cache le message

            isTransitioning = true;

            // Joue l'animation d'ouverture de porte
            doorAnimator.SetTrigger("Open");

            // Attends la fin de l'animation avant de changer de salle
            StartCoroutine(WaitForDoorToOpen());
        }
    }

    IEnumerator WaitForDoorToOpen()
    {
        yield return new WaitForSeconds(2f); // Ajuste selon la durée de ton animation
        FindObjectOfType<RoomManager>().LoadNextRoom(); // Change de salle
    }
}

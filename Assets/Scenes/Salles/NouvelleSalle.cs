using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
    public GameObject interactUI; // UI "E pour ouvrir"
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

            // Changer de salle via le `RoomManager`
            FindObjectOfType<RoomManager>().LoadNextRoom();
        }
    }
}

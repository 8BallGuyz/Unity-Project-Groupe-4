using UnityEngine;
using UnityEngine.UI;

public class PickupItem : MonoBehaviour
{
    public Sprite icon;
    public GameObject interactUI; // UI "E pour prendre"

    private bool isPlayerNear = false;

    private void Start()
    {
        interactUI.SetActive(false); // Cache l'UI au départ
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            interactUI.SetActive(true); // Afficher "E pour prendre"
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            interactUI.SetActive(false); // Cacher l'UI
        }
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (InventorySystem.instance.AddItem(gameObject, icon))
            {
                interactUI.SetActive(false); // Cacher l'UI après prise
                gameObject.SetActive(false); // Désactiver l'objet
            }
        }
    }
}

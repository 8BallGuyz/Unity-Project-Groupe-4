using UnityEngine;
using UnityEngine.UI;

public class PickupItem : MonoBehaviour
{
    public Sprite icon;

    public GameObject model3D; // 🔹 Modèle 3D à stocker
    public GameObject interactUI; // UI "E pour prendre"

    public static event System.Action OnItemPickedUp;


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
            if (InventorySystem.instance.AddItem(gameObject, icon, model3D))
            {
                interactUI.SetActive(false); // Cacher l'UI après prise
                gameObject.SetActive(false); // Désactiver l'objet

                OnItemPickedUp?.Invoke(); // 🔥 Déclencher l'événement quand l'objet est pris
            }
        }
    }
}

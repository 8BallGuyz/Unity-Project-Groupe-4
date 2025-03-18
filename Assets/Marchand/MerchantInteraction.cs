using UnityEngine;

public class MerchantInteraction : MonoBehaviour
{
    public GameObject merchantUI; // 🔹 Panel de l'UI du marchand (à assigner dans l'Inspector)
    private bool isNearMerchant = false; // 🔹 Pour savoir si le joueur est proche d'un marchand
    private bool isMerchantOpen = false; // 🔹 État de l'inventaire du marchand
    public PlayerMovement playerMovement; // 🔹 Référence au PlayerMovement

    void Start()
    {
        merchantUI.SetActive(false);
    }

    void Update()
    {
        if (isNearMerchant && Input.GetKeyDown(KeyCode.T))
        {
            ToggleMerchantUI();
        }
    }

    void ToggleMerchantUI()
    {
        isMerchantOpen = !isMerchantOpen;
        merchantUI.SetActive(isMerchantOpen);
        Cursor.lockState = isMerchantOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isMerchantOpen;

        // 🔹 Désactiver ou réactiver le mouvement du joueur
        if (playerMovement != null)
        {
            playerMovement.SetInventoryState(isMerchantOpen);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("✅ Marchand détecté !");
        isNearMerchant = true;

    }

    private void OnTriggerExit(Collider other)
    {

        isNearMerchant = false;
        merchantUI.SetActive(false); // 🔹 Ferme l'UI si le joueur s'éloigne
        isMerchantOpen = false;

        // 🔹 Réactive les mouvements du joueur
        if (playerMovement != null)
        {
            playerMovement.SetInventoryState(false);
        }

    }
}

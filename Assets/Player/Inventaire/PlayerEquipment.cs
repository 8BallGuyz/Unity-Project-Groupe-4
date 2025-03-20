using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public Transform handTransform; // 🔹 Emplacement où afficher l'objet (à assigner dans Unity)
    private GameObject currentEquippedItem;
    public void EquipItem(GameObject itemModel)
    {
        if (itemModel == null)
        {
            Debug.Log("❌ Modèle 3D introuvable !");
            return;
        }

        // 🔥 Vérifie si un objet est déjà équipé et le détruit avant d’en ajouter un nouveau
        if (currentEquippedItem == null)
        {
            Debug.Log(currentEquippedItem);
            currentEquippedItem = Instantiate(itemModel, handTransform.position, handTransform.rotation, handTransform); // 🔹 Instancie l'objet
        }


        currentEquippedItem.SetActive(true); // 🔹 Active l'objet s'il était désactivé
        Debug.Log($"🎮 Équipé : {itemModel.name}");
    }

    public bool HasEquippedItem()
    {
        return currentEquippedItem != null; // Vérifie si un objet est équipé
    }

    public void UnequipItem()
    {
        if (currentEquippedItem != null)
        {
            currentEquippedItem.SetActive(false); // 🔹 Active l'objet s'il était désactivé
            Debug.Log("🛑 Objet déséquipé !");
        }
    }

}

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

        if (currentEquippedItem != null)
        {
            Destroy(currentEquippedItem); // 🔹 Supprime l’ancien objet
        }

        currentEquippedItem = Instantiate(itemModel, handTransform.position, handTransform.rotation, handTransform); // 🔹 Instancie l'objet
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
            Destroy(currentEquippedItem); // Supprime l'objet actuellement équipé
            currentEquippedItem = null;
            Debug.Log("🛑 Objet déséquipé !");
        }
    }

}

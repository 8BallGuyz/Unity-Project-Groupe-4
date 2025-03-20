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
        if (currentEquippedItem != null)
        {
            Debug.Log($"🛑 Suppression de l'ancien objet : {currentEquippedItem.name}");
            Destroy(currentEquippedItem);
        }

        // 🔹 Instancie le nouvel objet et l'attache à la main du joueur
        currentEquippedItem = Instantiate(itemModel, handTransform);
        currentEquippedItem.transform.localPosition = Vector3.zero; // 🔹 Le place correctement
        // currentEquippedItem.transform.localRotation = Quaternion.identity;
        currentEquippedItem.SetActive(true);

        Debug.Log($"🎮 Équipé : {currentEquippedItem.name}");
    }


    public bool HasEquippedItem()
    {
        return currentEquippedItem != null; // Vérifie si un objet est équipé
    }
    public void UnequipItem()
    {
        if (currentEquippedItem != null)
        {
            Debug.Log($"🛑 Déséquipement de : {currentEquippedItem.name}");
            Destroy(currentEquippedItem); // 🔥 On le supprime complètement
            currentEquippedItem = null;
        }
    }


}

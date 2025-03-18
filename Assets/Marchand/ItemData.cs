using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string itemName; // 🔹 Nom de l’objet
    public Sprite itemIcon; // 🔹 Icône pour l’UI
    public GameObject itemPrefab; // 🔹 Modèle 3D à placer dans la scène
    public int price; // 🔹 Prix en crédits
}

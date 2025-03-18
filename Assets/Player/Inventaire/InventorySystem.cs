using UnityEngine;
using System.Collections.Generic;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance;

    public List<GameObject> items = new List<GameObject>();
    public List<Sprite> itemIcons = new List<Sprite>();
    public List<GameObject> itemModels = new List<GameObject>(); // 🔹 Stockage des modèles 3D

    public int maxSlots = 9;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public bool AddItem(GameObject item, Sprite icon, GameObject model)
    {
        if (items.Count >= maxSlots)
        {
            Debug.Log("❌ Inventaire plein !");
            return false;
        }

        items.Add(item);
        itemIcons.Add(icon);
        itemModels.Add(model); // 🔹 Ajoute le modèle 3D à la liste
        FindObjectOfType<InventoryUI>()?.RefreshUI();
        Debug.Log($"🆕 Ajouté : {item.name}, Icône : {(icon != null ? icon.name : "Aucune !")}, Modèle : {model.name}");
        return true;
    }
}

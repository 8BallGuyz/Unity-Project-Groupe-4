using UnityEngine;
using System.Collections.Generic;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance;
    public List<Sprite> itemIcons = new List<Sprite>();
    public List<GameObject> itemModels = new List<GameObject>(); // 🔹 Stockage des modèles 3D

    public int maxSlots = 9;

    private void Start()
    {
        if (RoomManager.instance != null)
        {
            itemIcons = new List<Sprite>(RoomManager.instance.itemIcons);
            itemModels = new List<GameObject>(RoomManager.instance.itemModels);
            FindObjectOfType<InventoryUI>()?.RefreshUI();
        }
    }

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public bool AddItem(GameObject item, Sprite icon, GameObject model)
    {
        if (itemModels.Count >= maxSlots)
        {
            Debug.Log("❌ Inventaire plein !");
            return false;
        }

        itemIcons.Add(icon);
        itemModels.Add(model); // 🔹 Ajoute le modèle 3D à la liste
        FindObjectOfType<InventoryUI>()?.RefreshUI();
        Debug.Log($"🆕 Ajouté : {item.name}, Icône : {(icon != null ? icon.name : "Aucune !")}, Modèle : {model.name}");
        return true;
    }
}

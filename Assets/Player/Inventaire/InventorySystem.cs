using UnityEngine;
using System.Collections.Generic;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance;

    public List<GameObject> items = new List<GameObject>();
    public List<Sprite> itemIcons = new List<Sprite>();

    public int maxSlots = 9;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public bool AddItem(GameObject item, Sprite icon)
    {
        if (items.Count >= maxSlots)
        {
            Debug.Log("❌ Inventaire plein !");
            return false;
        }

        items.Add(item);
        itemIcons.Add(icon); // Maintenant on passe bien l'icône
        FindObjectOfType<InventoryUI>()?.RefreshUI();
        Debug.Log($"🆕 Ajouté : {item.name}, Icône : {(icon != null ? icon.name : "Aucune !")}");
        return true;
    }

}

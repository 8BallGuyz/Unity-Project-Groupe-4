using UnityEngine;
using System.Collections.Generic;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance;

    public PlayerEquipment playerEquipment;
    
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

    int equippedIndex = FindObjectOfType<InventoryUI>().GetEquippedItemIndex();

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.B))
        // {
        //     Debug.Log("B pressé");
        //     RemoveItem(equippedIndex); // Supprime l’item du premier slot (à modifier selon ton besoin)
        // }
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
    
    public void RemoveItem(int index)
    {

        playerEquipment = FindObjectOfType<PlayerEquipment>();

        if (InventorySystem.instance == null)
        {
            Debug.LogError("❌ InventorySystem.instance est null !");
            return;
        }

        if (index < 0 || index >= InventorySystem.instance.itemModels.Count)
        {
            Debug.LogError($"❌ Index {index} invalide pour la suppression !");
            return;
        }

        Debug.Log($"🗑️ Suppression de l'objet {InventorySystem.instance.itemModels[index].name} de l'inventaire");

        // Supprime l'item de la liste des modèles et des icônes
        InventorySystem.instance.itemModels.RemoveAt(index);
        InventorySystem.instance.itemIcons.RemoveAt(index);
        Destroy(playerEquipment.currentEquippedItem);
        


        // Met à jour l'inventaire à l'écran
        FindObjectOfType<InventoryUI>()?.RefreshUI();
    }

}

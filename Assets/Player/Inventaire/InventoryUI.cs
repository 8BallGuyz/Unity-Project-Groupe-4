using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Transform slotParent;
    public GameObject slotPrefab;
    public PlayerMovement playerMovement; // 🔹 Référence au PlayerMovement

    private List<GameObject> slots = new List<GameObject>();
    private bool isOpen = false;

    void Start()
    {
        // Création des slots
        for (int i = 0; i < 9; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            slots.Add(slot);
        }

        inventoryPanel.SetActive(false); // L'inventaire est fermé au début
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isOpen = !isOpen;
            inventoryPanel.SetActive(isOpen);
            Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isOpen;

            if (playerMovement != null)
            {
                playerMovement.SetInventoryState(isOpen); // 🔹 Désactive les contrôles du joueur
            }

            if (isOpen) RefreshUI();
        }



        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) // Vérifie les touches 1 à 9
            {
                EquipItem(i);
            }
        }
    }

    // 🔹 Fonction pour équiper un objet
    void EquipItem(int index)
    {
        PlayerEquipment playerEquipment = FindObjectOfType<PlayerEquipment>();

        if (index >= InventorySystem.instance.itemModels.Count) // Vérifie si la case est vide
        {
            Debug.Log("❌ Aucun objet à cet emplacement !");
            
            if (playerEquipment != null && playerEquipment.HasEquippedItem()) 
            {
                Debug.Log("🔄 Déséquipement de l'objet actuel.");
                playerEquipment.UnequipItem(); // Déséquipe l'objet actuel
            }
            return;
        }

        // Récupère le modèle 3D depuis l'inventaire
        GameObject selectedItemPrefab = InventorySystem.instance.itemModels[index];

        if (selectedItemPrefab == null)
        {
            Debug.LogError("❌ Le modèle de l'objet est NULL !");
            return;
        }

        // Instancie l'objet dans la scène
        //GameObject instantiatedItem = Instantiate();
        
        // Appelle EquipItem avec l'objet instancié
        playerEquipment?.EquipItem(selectedItemPrefab);

        Debug.Log($"✅ Objet {selectedItemPrefab.name} équipé !");
    }



    public void RefreshUI()
    {
        // Vérifie si l'instance d'InventorySystem existe
        if (InventorySystem.instance == null)
        {
            Debug.LogError("❌ InventorySystem.instance est null !");
            return;
        }

        for (int i = 0; i < slots.Count; i++)
        {
            // Récupère l'image du slot
            Image img = slots[i].transform.Find("ItemIcon").GetComponent<Image>();

            if (img == null)
            {
                Debug.LogError($"❌ Slot {i} n'a pas d'Image attachée !");
                continue;
            }

            if (i < InventorySystem.instance.itemIcons.Count)
            {
                Debug.Log($"🎨 Icone trouvée pour le slot {i} : {InventorySystem.instance.itemIcons[i].name}");
                img.sprite = InventorySystem.instance.itemIcons[i];
                img.enabled = true;
            }
            else
            {
                img.enabled = false;
            }
        }
    }
}

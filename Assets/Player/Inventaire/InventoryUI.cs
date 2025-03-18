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
        if (index >= InventorySystem.instance.items.Count)
        {
            Debug.Log("❌ Aucun objet à cet emplacement !");
            return;
        }

        GameObject selectedItemModel = InventorySystem.instance.itemModels[index]; // 🔹 Récupère le modèle 3D
        FindObjectOfType<PlayerEquipment>()?.EquipItem(selectedItemModel); // 🔹 Affiche l’objet équipé
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

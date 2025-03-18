using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Transform slotParent;
    public GameObject slotPrefab;

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
            if (isOpen) RefreshUI();
        }
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
            // Récupère l'image du slot (assure-toi que `slotPrefab` contient une `Image` !)
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

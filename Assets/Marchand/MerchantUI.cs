using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MerchantUI : MonoBehaviour
{
    public int playerCredits = 100; // 💰 Crédits du joueur (modifiable)
    public MerchantItemList merchantItems; // 🔹 Référence aux objets vendables
    public GameObject itemButtonPrefab; // 🔹 Bouton modèle pour afficher un objet
    public Transform itemListContainer; // 🔹 Conteneur où afficher les objets
    public int maxSlots = 9; // 🔹 Nombre total de slots dans l'inventaire du marchand

    void Start()
    {
        PopulateMerchantUI();
    }

    void PopulateMerchantUI()
    {
        // 🗑️ Supprimer les anciens objets de l'UI
        foreach (Transform child in itemListContainer)
        {
            Destroy(child.gameObject);
        }

        if (merchantItems == null || merchantItems.allItems == null || itemButtonPrefab == null || itemListContainer == null)
        {
            Debug.LogError("❌ Erreur dans `MerchantUI.cs`: Un élément est NULL !");
            return;
        }

        Debug.Log($"🔄 Génération aléatoire des objets du marchand...");

        for (int i = 0; i < maxSlots; i++)
        {
            int hasItem = Random.Range(1, 3); // 🛑 50% de chance d'avoir un objet (1 = vide, 2 = objet)
            
            if (hasItem == 2 && merchantItems.allItems.Count > 0)
            {
                int randomIndex = Random.Range(0, merchantItems.allItems.Count); // 🎲 Choix aléatoire d'un objet
                ItemData randomItem = merchantItems.allItems[randomIndex];

                Debug.Log($"🛒 Ajout de {randomItem.itemName} au slot {i + 1}");

                // 📌 Création du bouton d'objet
                GameObject newButton = Instantiate(itemButtonPrefab, itemListContainer);
                newButton.GetComponentInChildren<Text>().text = $"{randomItem.price} €";
                newButton.GetComponentInChildren<Image>().sprite = randomItem.itemIcon;

                // Ajout d'un événement pour l'achat
                newButton.GetComponent<Button>().onClick.AddListener(() => BuyItem(randomItem, newButton));
            }
            else
            {
                Debug.Log($"❌ Slot {i + 1} vide");


                GameObject newButton = Instantiate(itemButtonPrefab, itemListContainer);
                newButton.GetComponentInChildren<Text>().text = "";
                newButton.GetComponentInChildren<Image>().enabled = false;
                newButton.GetComponent<Button>().interactable = false;
            }
        }
    }

    void BuyItem(ItemData item, GameObject button)
    {
        if (playerCredits >= item.price) 
        {
            // ✅ Achat validé
            playerCredits -= item.price;
            Debug.Log($"💰 Achat de {item.itemName} pour {item.price} crédits ! Crédits restants : {playerCredits}");

            // ❌ Désactiver l'item au lieu de le détruire
            button.GetComponentInChildren<Text>().text = "";
            button.GetComponentInChildren<Image>().enabled = false;
            button.GetComponent<Button>().interactable = false;
        } 
        else 
        {
            // ❌ Achat refusé (pas assez d’argent)
            Debug.Log($"⛔ Pas assez de crédits pour acheter {item.itemName} !");
        }
    }
}

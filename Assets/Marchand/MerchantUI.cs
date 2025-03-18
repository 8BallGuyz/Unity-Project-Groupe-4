using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MerchantUI : MonoBehaviour
{
    public MerchantItemList merchantItems; // 🔹 Référence aux objets vendables
    public GameObject itemButtonPrefab; // 🔹 Bouton modèle pour afficher un objet
    public Transform itemListContainer; // 🔹 Conteneur où afficher les objets

    void Start()
    {
        PopulateMerchantUI();
    }

    void PopulateMerchantUI()
    {
        if (merchantItems == null) Debug.LogError("❌ `merchantItems` est NULL !");
        if (merchantItems != null && merchantItems.allItems == null) Debug.LogError("❌ `merchantItems.allItems` est NULL !");
        if (itemButtonPrefab == null) Debug.LogError("❌ `itemButtonPrefab` est NULL !");
        if (itemListContainer == null) Debug.LogError("❌ `itemListContainer` est NULL !");

        if (merchantItems == null || merchantItems.allItems == null || itemButtonPrefab == null || itemListContainer == null)
        {
            return; // On arrête ici pour éviter l’erreur
        }

        Debug.Log($"🔄 Génération des objets du marchand... Nombre d'objets : {merchantItems.allItems.Count}");

        foreach (var item in merchantItems.allItems)
        {
            Debug.Log($"🛒 Ajout de {item.itemName} à l'UI.");
            GameObject newButton = Instantiate(itemButtonPrefab, itemListContainer);
            newButton.GetComponentInChildren<Text>().text = $"{item.price} €";
            newButton.GetComponentInChildren<Image>().sprite = item.itemIcon;
        }
    }



}

using UnityEngine;
using System.Collections.Generic;

public class MerchantItemList : MonoBehaviour
{
    public List<ItemData> allItems = new List<ItemData>(); // 🔹 Tous les objets vendables

    void Start()
    {
        Debug.Log($"Marchand contient {allItems.Count} objets.");
    }
}

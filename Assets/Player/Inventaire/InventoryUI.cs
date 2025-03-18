using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;

    private bool isOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isOpen = !isOpen;
            inventoryPanel.SetActive(isOpen);
        }
    }
}

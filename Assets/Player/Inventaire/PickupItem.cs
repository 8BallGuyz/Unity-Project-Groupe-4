using UnityEngine;

public class PickupItem : MonoBehaviour
{

    public Sprite icon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (InventorySystem.instance.AddItem(gameObject, icon))
            {
                gameObject.SetActive(false);
            }
        }
    }
}

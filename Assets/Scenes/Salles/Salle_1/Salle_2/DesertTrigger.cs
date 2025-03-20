using UnityEngine;

public class DesertTrigger : MonoBehaviour
{
    public string direction; // 🔹 "Top", "Bottom", "Left", "Right" ou "Center"

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InfiniteDesert parentScript = GetComponentInParent<InfiniteDesert>();
            if (parentScript != null && direction != "Center")
            {
                parentScript.OnPlayerEnterTrigger(direction);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InfiniteDesert parentScript = GetComponentInParent<InfiniteDesert>();
            if (parentScript != null && direction == "Center")
            {
                // Debug.Log($"🚀 OnTriggerExit détecté sur {transform.parent.name} (Trigger: {name})");
                parentScript.OnPlayerExitTrigger();
            }
        }
    }


}

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Text creditsText; // Text displaying credits

    void Awake()
    {
        instance = this; // Singleton

        // Try to find the Text component if it's null
        if (creditsText == null)
        {
            creditsText = GameObject.Find("CreditsText").GetComponent<Text>();
            // Or use a more specific path if needed
            // creditsText = transform.Find("Canvas/Panel/CreditsText").GetComponent<Text>();

            if (creditsText == null)
                Debug.LogError("Credits Text reference not set and could not be found!");
        }
    }

    void Start()
    {
        // Wait a frame to ensure RoomManager is initialized
        Invoke("UpdateCreditsText", 0.1f);
    }

    public void UpdateCreditsText()
    {
        if (creditsText != null && RoomManager.instance != null)
        {
            creditsText.text = RoomManager.instance.GetCredits().ToString();
        }
        else
        {
            Debug.LogWarning("Unable to update credits text. Missing references.");
        }
    }
}
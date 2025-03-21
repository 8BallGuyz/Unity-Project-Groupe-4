using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Text creditsText; 

    void Awake()
    {
        instance = this;

        if (creditsText == null)
        {
            creditsText = GameObject.Find("CreditsText").GetComponent<Text>();

            if (creditsText == null)
                Debug.LogError("Credits Text reference not set and could not be found!");
        }
    }

    void Start()
    {
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
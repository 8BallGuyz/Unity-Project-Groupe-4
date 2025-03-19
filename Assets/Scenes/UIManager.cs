using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Text creditsText; // 🔹 Le texte affichant les crédits

    void Awake()
    {
        instance = this; // Singleton
    }

    void Start()
    {
        UpdateCreditsText(); // 🔹 Met à jour l'affichage au démarrage
    }

    public void UpdateCreditsText()
    {
        if (creditsText != null)
        {
            creditsText.text = RoomManager.instance.GetCredits().ToString();

        }
    }
}

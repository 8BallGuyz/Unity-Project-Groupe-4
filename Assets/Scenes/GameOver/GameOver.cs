using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{


    public GameObject Boutton_Reesayer; // Assigne ton bouton dans l'Inspector

    public void HideButton()
    {
        Boutton_Reesayer.SetActive(false); // Cache le bouton
    }

    public void ShowButton()
    {
        Boutton_Reesayer.SetActive(true); // Montre le bouton
    }

    public void Reesayer()
    {
        RoomManager roomManager = FindObjectOfType<RoomManager>();
        SceneManager.LoadScene(roomManager.GetCurrentRoom());
    }

    public void RetourMenu()
    {
        GameObject gameManager = GameObject.Find("GameManager"); // Trouve l'objet
        if (gameManager != null)
        {
            Destroy(gameManager); // Le détruit
        }

        SceneManager.LoadScene("Menu"); // Charge le menu
    }

    // Start is called before the first frame update
    void Start()
    {
        switch (DifficultyManager.CurrentDifficulty)
        {
            case DifficultyManager.DifficultyLevel.Facile:
                ShowButton();
                break;
            case DifficultyManager.DifficultyLevel.Moyen:
                ShowButton();
                break;
            case DifficultyManager.DifficultyLevel.Difficile:
                HideButton();
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

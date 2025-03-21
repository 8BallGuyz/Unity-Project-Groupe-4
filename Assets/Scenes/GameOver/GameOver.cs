using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{


    public GameObject Boutton_Reesayer; 

    public void HideButton()  
    {
        Boutton_Reesayer.SetActive(false); 
    }

    public void ShowButton()
    {
        Boutton_Reesayer.SetActive(true); 
    }

    public void Reesayer()
    {
        RoomManager roomManager = FindObjectOfType<RoomManager>();
        SceneManager.LoadScene(roomManager.GetCurrentRoom());
    }

    public void RetourMenu()
    {
        GameObject gameManager = GameObject.Find("GameManager");
        if (gameManager != null)
        {
            Destroy(gameManager); 
        }

        SceneManager.LoadScene("Menu");
    }

    
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

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
}

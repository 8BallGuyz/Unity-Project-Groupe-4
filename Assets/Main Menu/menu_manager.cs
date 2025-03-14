using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menu_manager : MonoBehaviour
{
    public Canvas mainMenu;
    public Canvas settingsMenu_volume;
    public Canvas settingsMenu_luminosite;
    public Canvas settingsMenu_touches;
    public Canvas credits;
    public Canvas Partie_menu;
    public Canvas Mode_menu;

    public void SetEasy() 
    { 
        DifficultyManager.SetDifficulty(0); 
        Main();
    }
    public void SetMedium() 
    { 
        DifficultyManager.SetDifficulty(1); 
        Main();
    }
    public void SetHard() 
    { 
        DifficultyManager.SetDifficulty(2); 
        Main();
    }

    void Start()
    {

        
        mainMenu.enabled = true;

        settingsMenu_volume.enabled = false;
        settingsMenu_luminosite.enabled = false;
        settingsMenu_touches.enabled = false;
        credits.enabled = false;
        Partie_menu.enabled = false;
        Mode_menu.enabled = false;
    }

    public void Main()
    {
        RoomManager roomManager = FindObjectOfType<RoomManager>();
        SceneManager.LoadScene(roomManager.GetCurrentRoom());

        // if (roomManager != null)
        // {
        //     roomManager.LoadNextRoom();
        // }
        // else
        // {
        //     Debug.LogError("RoomManager introuvable !");
        // }
    }


    public void Partie() // au cas ou si on a un boutton pour revenir au Main Menu
    {
        Partie_menu.enabled = true;

        mainMenu.enabled = false;
        settingsMenu_volume.enabled = false;
        settingsMenu_luminosite.enabled = false;
        settingsMenu_touches.enabled = false;
        credits.enabled = false;
        Mode_menu.enabled = false;
    }

    public void Mode() // au cas ou si on a un boutton pour revenir au Main Menu
    {

        Mode_menu.enabled = true;

        Partie_menu.enabled = false;
        mainMenu.enabled = false;
        settingsMenu_volume.enabled = false;
        settingsMenu_luminosite.enabled = false;
        settingsMenu_touches.enabled = false;
        credits.enabled = false;
    }


    public void Menu() // au cas ou si on a un boutton pour revenir au Main Menu
    {
        mainMenu.enabled = true;

        settingsMenu_volume.enabled = false;
        settingsMenu_luminosite.enabled = false;
        settingsMenu_touches.enabled = false;
        credits.enabled = false;
        Partie_menu.enabled = false;
        Mode_menu.enabled = false;
    }

    public void Settings_General()
    {
        settingsMenu_volume.enabled = true;

        mainMenu.enabled = false;
        settingsMenu_luminosite.enabled = false;
        settingsMenu_touches.enabled = false;
        credits.enabled = false;
        Partie_menu.enabled = false;
        Mode_menu.enabled = false;
    }

    public void Settings_Accessibility()
    {
        settingsMenu_luminosite.enabled = true;

        settingsMenu_volume.enabled = false;
        mainMenu.enabled = false;
        settingsMenu_touches.enabled = false;
        credits.enabled = false;
        Partie_menu.enabled = false;
        Mode_menu.enabled = false;
    }

    public void Settings_Controls()
    {
        settingsMenu_touches.enabled = true;

        settingsMenu_volume.enabled = false;
        mainMenu.enabled = false;
        settingsMenu_luminosite.enabled = false;
        credits.enabled = false;
        Partie_menu.enabled = false;
        Mode_menu.enabled = false;
    }

    public void Credits()
    {
        credits.enabled = true;

        settingsMenu_volume.enabled = false;
        mainMenu.enabled = false;
        settingsMenu_luminosite.enabled = false;
        settingsMenu_touches.enabled = false;
        Partie_menu.enabled = false;
        Mode_menu.enabled = false;
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player has left the game");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menu_manager : MonoBehaviour
{
    public Canvas mainMenu;
    public Canvas settingsMenu_general;
    public Canvas settingsMenu_accessibility;
    public Canvas settingsMenu_controls;

    void Start()
    {

        mainMenu.enabled = true;

        settingsMenu_general.enabled = false;
        settingsMenu_accessibility.enabled = false;
        settingsMenu_controls.enabled = false;
    }

    public void Main()
    {
        SceneManager.LoadScene("Salle_1");
    }

    public void Menu() // au cas ou si on a un boutton pour revenir au Main Menu
    {
        mainMenu.enabled = true;

        settingsMenu_general.enabled = false;
        settingsMenu_accessibility.enabled = false;
        settingsMenu_controls.enabled = false;
    }

    public void Settings_General()
    {
        settingsMenu_general.enabled = true;

        mainMenu.enabled = false;
        settingsMenu_accessibility.enabled = false;
        settingsMenu_controls.enabled = false;
    }

    public void Settings_Accessibility()
    {
        settingsMenu_accessibility.enabled = true;

        settingsMenu_general.enabled = false;
        mainMenu.enabled = false;
        settingsMenu_controls.enabled = false;
    }

    public void Settings_Controls()
    {
        settingsMenu_controls.enabled = true;

        settingsMenu_general.enabled = false;
        mainMenu.enabled = false;
        settingsMenu_accessibility.enabled = false;
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player has left the game");
    }
}

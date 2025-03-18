using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public GameObject Bouton_Replay;

    public void Replay(){
        SceneManager.LoadScene(RoomManager.GetSalle(1));
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
}

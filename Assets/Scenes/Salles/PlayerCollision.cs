using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private bool _cursorLocked;  

    private void Start()  
    {
        Cursor.lockState = CursorLockMode.Locked;  
        Cursor.visible = false;                    
        _cursorLocked = true;                      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Poison"))
        {
            LoadGameOverScene();
        }
    }
    private void LoadGameOverScene()
    {
   
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;

        SceneManager.LoadScene("GameOver");
    }
}

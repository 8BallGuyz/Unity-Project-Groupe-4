using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class RoomTimer : MonoBehaviour
{
    public float timerDuration = 30f;  
    private float timeRemaining;

    public TMP_Text timerText;  
    public Transform player;  
    public Transform door;   

    private bool playerReachedDoor = false; 

    void Start()
    {
        Time.timeScale = 1f; 
        timeRemaining = timerDuration;  
    }

    void Update()
    {
        if (!playerReachedDoor)
        {
            timeRemaining -= Time.deltaTime;

         
            if (timerText != null)
            {
                timerText.text = $"Temps restant: {Mathf.Ceil(timeRemaining)}s";

           
                if (timeRemaining <= 5f)
                {
                    timerText.color = Color.red;
                }
            }

            if (timeRemaining <= 0)
            {
                TriggerGameOver();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerReachedDoor = true; 
            Debug.Log("Player reached the door — Timer stopped!");
        }
    }

    void TriggerGameOver()
    {
        Debug.Log("Time’s up! Game Over!");


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        Time.timeScale = 1f;

        SceneManager.LoadScene("GameOver");
    }
}

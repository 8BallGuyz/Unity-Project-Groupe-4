using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key_checker : MonoBehaviour
{
    public DoorTrigger door;
    public PlayerMovement player;
    public GameObject keyUI;
    public GameObject interactUI; // UI "E pour ouvrir"
    private bool isPlayerNear = false;
    // Start is called before the first frame update
    void Start()
    {
        door.enabled = false;
        keyUI.SetActive(false);
        interactUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNear == true)
        {
            if (player.key == 0)
            {
                interactUI.SetActive(false);
                keyUI.SetActive(true);
                door.enabled = false;
            }
            else if (player.key == 1)
            {
                interactUI.SetActive(true);
                keyUI.SetActive(false);
                door.enabled = true;
            }
        }

        if (player.key == 1)
        {
            door.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && player.key == 0)
        {
            isPlayerNear = true;
            keyUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && player.key == 0)
        {
            isPlayerNear = false;
            keyUI.SetActive(false);
        }
    }
}

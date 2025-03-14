using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject TimeSwapOnPlayer;
    // Start is called before the first frame update
    void Start()
    {
        TimeSwapOnPlayer.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                this.gameObject.SetActive(false);
                
                TimeSwapOnPlayer.SetActive(true);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject PickupText;
    public GameObject ItemOnPlayer;

    // Start is called before the first frame update
    void Start()
    {
        ItemOnPlayer.SetActive(false);
        PickupText.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PickupText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                this.gameObject.SetActive(false);
                
                ItemOnPlayer.SetActive(true);

                PickupText.SetActive(false);
            }
        }
    }
}

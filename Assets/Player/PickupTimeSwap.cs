using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject PickupText;
    public GameObject ItemQuiSeraSurLeJoueur;

    // Start is called before the first frame update
    void Start()
    {
        ItemQuiSeraSurLeJoueur.SetActive(false);
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
                
                ItemQuiSeraSurLeJoueur.SetActive(true);

                PickupText.SetActive(false);
            }
        }
    }
}

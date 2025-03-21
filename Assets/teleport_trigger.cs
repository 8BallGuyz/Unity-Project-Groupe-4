using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport_trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<RoomManager>().LoadNextRoom(); // Change de salle
        }
    }
}

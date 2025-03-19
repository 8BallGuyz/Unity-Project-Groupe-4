using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP : MonoBehaviour
{
    private Vector3 savedPosition;
    private bool hasClicked = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Récupère le Rigidbody s'il y en a un
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasClicked) // Clic gauche (une seule fois)
        {
            savedPosition = transform.position; // Sauvegarde la position actuelle
            hasClicked = true; // Empêche plusieurs clics
            Debug.Log("✅ Position enregistrée : " + savedPosition);

            StartCoroutine(TeleportAfterDelay(10f)); // Lance la téléportation après 10s
        }
    }

    IEnumerator TeleportAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (rb != null) 
        {
            rb.velocity = Vector3.zero; // Arrête le mouvement du joueur
            rb.position = savedPosition; // Téléporte via Rigidbody
            Debug.Log("🚀 Téléportation via Rigidbody à : " + savedPosition);
        }
        else
        {
            transform.position = savedPosition; // Téléporte normalement
            Debug.Log("🚀 Téléportation via Transform à : " + savedPosition);
        }

        hasClicked = false; // Réactive le clic après la téléportation
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSlime : MonoBehaviour
{
    public GameObject pud1; // Référence à l'objet Pud
    public GameObject pud2; // Référence à l'objet Pud (1)

    private void Start()
    {
        // Assure-toi que les objets Pud sont bien assignés dans l'inspecteur
        if (pud1 == null || pud2 == null)
        {
            Debug.LogError("Les objets Pud1 et Pud2 doivent être assignés !");
            return;
        }

        // Appeler la fonction de téléportation toutes les 10 secondes
        InvokeRepeating("TeleportRandomly", 0f, 10f);
    }

    public void TeleportRandomly()
    {
        // Choisir aléatoirement entre pud1 et pud2
        Vector3 targetPosition = Random.value < 0.5f ? pud1.transform.position : pud2.transform.position;

        // Déplacer le Slime à la position choisie
        transform.position = targetPosition;
    }
}

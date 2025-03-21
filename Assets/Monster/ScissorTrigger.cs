using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeHeadTrigger : MonoBehaviour
{
    public MonsterAI scolopendre; // Référence au script qui gère le scolopendre

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Scissor")) // Vérifie que la tête touche la cisaille
        {
            Debug.Log("La tête a touché la cisaille ! Suppression d'un segment...");
            if (scolopendre != null)
            {
                scolopendre.RemoveSegment(); // Supprime un segment
            }
        }
    }
}

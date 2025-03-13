using UnityEngine;

public class AmbienceZone : MonoBehaviour
{
    public AudioClip ambienceClip; // Musique spécifique à cette zone

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Vérifie si c'est le joueur
        {
            AmbienceManager.Instance.ChangeAmbience(ambienceClip);
        }
    }
}

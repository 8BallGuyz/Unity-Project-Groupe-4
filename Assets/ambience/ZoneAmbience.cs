using UnityEngine;

public class AmbienceZone : MonoBehaviour
{
    public AudioClip ambienceClip; // Musique spécifique à cette zone

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Vérifie si c'est le joueur
        {
            AmbienceManager.Instance.ChangeAmbience(ambienceClip);
        }
    }
}

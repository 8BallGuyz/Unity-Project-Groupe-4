using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AidesScript : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>(); // Liste des sons
    public KeyCode startKey = KeyCode.Space; // Touche pour démarrer la lecture
    private AudioSource audioSource;
    private bool isPlaying = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

                if (!isPlaying)
        {
            StartCoroutine(PlaySoundsSequentially());
        }
    }

    void Update()
    {

    }

    IEnumerator PlaySoundsSequentially()
    {
        isPlaying = true;
        
        Debug.Log("Attente de 3 secondes avant de jouer les sons...");
        yield return new WaitForSeconds(3f); // Pause de 3 secondes avant de commencer

        foreach (AudioClip clip in audioClips)
        {
            if (clip != null)
            {
                audioSource.clip = clip;
                audioSource.Play();
                Debug.Log("Lecture de : " + clip.name);
                yield return new WaitForSeconds(clip.length ); // Attend la durée du son + 1 seconde
            }
        }

        isPlaying = false;
    }
}
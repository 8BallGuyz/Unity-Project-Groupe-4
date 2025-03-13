using UnityEngine;
using System.Collections;

public class AmbienceManager : MonoBehaviour
{
    public static AmbienceManager Instance; // Singleton
    public AudioClip defaultAmbience; // Musique par défaut

    private AudioSource ambienceSource;
    private AudioClip currentClip;
    private Coroutine fadeCoroutine;


    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            return;
        }


        // Ajouter un AudioSource et le configurer
        ambienceSource = gameObject.AddComponent<AudioSource>();
        ambienceSource.loop = true;
        ambienceSource.volume = 0.5f;
        ambienceSource.spatialBlend = 0;
        ambienceSource.clip = defaultAmbience;
        ambienceSource.Play();
        currentClip = defaultAmbience;
    }

    public void ChangeAmbience(AudioClip newClip)
    {
        if (newClip == currentClip) return;

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeToNewClip(newClip));
    }

    private IEnumerator FadeToNewClip(AudioClip newClip)
    {
        float fadeDuration = 1.5f;
        float startVolume = ambienceSource.volume;

        while (ambienceSource.volume > 0)
        {
            ambienceSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        ambienceSource.clip = newClip;
        ambienceSource.Play();
        currentClip = newClip;

        while (ambienceSource.volume < startVolume)
        {
            ambienceSource.volume += startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }
    }
}

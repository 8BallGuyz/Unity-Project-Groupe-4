using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;

public class ArcDeCercleLune : MonoBehaviour
{
    public Transform player; // Le joueur
    public Transform lune;   // La lune
    public AudioSource audioSource; // Son
    public AudioClip spookySound; // Son effrayant
    public PostProcessVolume postProcessVolume; // Volume de post-processing
    public float rayon = 200f; 
    public float hauteur = 200f; 
    private float angle = 0f; 
    private float vitesseRotation = Mathf.PI / 30f; 
    private float elapsedTime = 0f; 
    private bool effectTriggered = false; 
    private bool stopMovement = false;

    private Vignette vignette;
    private LensDistortion lensDistortion;
    private ChromaticAberration chromaticAberration;

    void Start()
    {
        // Récupère les effets de post-processing
        if (postProcessVolume != null)
        {
            postProcessVolume.profile.TryGetSettings(out vignette);
            postProcessVolume.profile.TryGetSettings(out lensDistortion);
            postProcessVolume.profile.TryGetSettings(out chromaticAberration);
        }
    }

    void Update()
    {
        if (player != null && !stopMovement)
        {
            elapsedTime += Time.deltaTime;
            angle += vitesseRotation * Time.deltaTime;
            float x = Mathf.Cos(angle) * rayon; 
            float y = Mathf.Sin(angle) * hauteur; 
            lune.position = player.position + new Vector3(x, y, 0);
            Vector3 direction = (player.position - lune.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            lune.rotation = Quaternion.Slerp(lune.rotation, targetRotation, Time.deltaTime * 5f);

            if (elapsedTime >= 32f && !effectTriggered)
            {
                TriggerDistortionEffect();
                stopMovement = true;
            }
        }
    }

    void TriggerDistortionEffect()
    {
        Debug.Log("🌑 Distorsion activée...");
        if (audioSource != null && spookySound != null)
        {
            audioSource.PlayOneShot(spookySound);
        }
        StartCoroutine(ApplyDistortionEffect());
    }

    private IEnumerator ApplyDistortionEffect()
    {
        float duration = 3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            if (vignette != null)
                vignette.intensity.value = Mathf.Lerp(0f, 0.6f, elapsed / duration);

            if (lensDistortion != null)
                lensDistortion.intensity.value = Mathf.Lerp(0f, -50f, elapsed / duration);

            if (chromaticAberration != null)
                chromaticAberration.intensity.value = Mathf.Lerp(0f, 1f, elapsed / duration);

            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}

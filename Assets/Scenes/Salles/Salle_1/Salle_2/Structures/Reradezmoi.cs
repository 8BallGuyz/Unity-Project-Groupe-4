using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;

public class ArcDeCercleLune : MonoBehaviour
{

    public static bool luneTombee = false; 
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


    public Light soleil; // Référence à la lumière Directional (le soleil)
    public float maxIntensity = 7f; // Intensité maximale du soleil
    public float minIntensity = 0f; // Intensité minimale du soleil
    private float zenithThreshold = 0.5f; // Seuil pour estimer que la lune est au zénith
    public Light luneLight; // Directional Light de la lune

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

            AjusterIntensiteSoleil(y);

            // Désactiver la lumière de la lune quand elle a terminé sa trajectoire
            if (angle >= Mathf.PI) // Condition : Quand la lune est au plus bas
            {
                if (luneLight != null)
                    luneLight.enabled = false;
            }
            else
            {
                if (luneLight != null)
                    luneLight.enabled = true;
            }


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

    void AjusterIntensiteSoleil(float hauteurLune)
    {
        if (soleil == null) return;

        // Normalise la hauteur de la lune entre 0 (plus bas) et 1 (plus haut)
        float hauteurNormale = Mathf.InverseLerp(-hauteur, hauteur, hauteurLune);

        // Inverse la logique : Soleil intense quand la lune est basse, faible quand elle est haute
        if (hauteurNormale < zenithThreshold) // Avant le zénith
        {
            soleil.intensity = Mathf.Lerp(minIntensity, maxIntensity, hauteurNormale / zenithThreshold);
        }
        else // Après le zénith
        {
            float hauteurDescente = Mathf.InverseLerp(zenithThreshold, 1f, hauteurNormale);
            soleil.intensity = Mathf.Lerp(maxIntensity, minIntensity, hauteurDescente);
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

        // Active le monstre après la chute de la lune
        luneTombee = true;
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

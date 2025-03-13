using UnityEngine;

public class ZoneLightTrigger : MonoBehaviour
{
    public Light pointLight; // Référence à la lumière du joueur

    private Color insideColor = new Color32(255, 255, 255, 255); // Blanc (FFFFFFFF)
    private Color outsideColor = new Color32(153, 36, 36, 255); // Rouge foncé (992424)

    private float insideIntensity = 5f;
    private float outsideIntensity = 2.5f;
    
    private float insideRange = 23f;  // Portée de la lumière à l'intérieur de la zone
    private float outsideRange = 12f;  // Portée de la lumière à l'extérieur de la zone

    private float transitionSpeed = 2f; // Vitesse de transition

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Vérifie si c'est le joueur
        {
            StopAllCoroutines();
            StartCoroutine(ChangeLight(insideIntensity, insideColor, insideRange));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Vérifie si c'est le joueur
        {
            StopAllCoroutines();
            StartCoroutine(ChangeLight(outsideIntensity, outsideColor, outsideRange));
        }
    }

    private System.Collections.IEnumerator ChangeLight(float targetIntensity, Color targetColor, float targetRange)
    {
        float elapsed = 0f;
        float startIntensity = pointLight.intensity;
        float startRange = pointLight.range;
        Color startColor = pointLight.color;

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * transitionSpeed;
            pointLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, elapsed);
            pointLight.range = Mathf.Lerp(startRange, targetRange, elapsed);
            pointLight.color = Color.Lerp(startColor, targetColor, elapsed);
            yield return null;
        }

        // S'assurer que les valeurs finales sont bien appliquées
        pointLight.intensity = targetIntensity;
        pointLight.range = targetRange;
        pointLight.color = targetColor;
    }
}

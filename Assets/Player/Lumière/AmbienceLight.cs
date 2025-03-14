using UnityEngine;

public class ZoneLightTrigger : MonoBehaviour
{
    public Light pointLight; // Référence à la lumière du joueur

    private Color insideColor = new Color32(255, 255, 255, 255); // Blanc
    private Color outsideColor = new Color32(153, 36, 36, 255); // Rouge foncé
    private Color labColor = new Color32(130, 36, 36, 255); // Rouge du Lab

    private float insideIntensity = 5f;
    private float labIntensity = 4f;
    private float outsideIntensity = 2.5f;
    
    private float insideRange = 23f;  // Portée de la lumière à l'intérieur
    private float labRange = 18f;  // Portée de la lumière dans le Lab
    private float outsideRange = 12f;  // Portée de la lumière à l'extérieur

    private float transitionSpeed = 2f; // Vitesse de transition
    private string currentZone = "Outside"; // Par défaut, le joueur est dehors

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            // Vérifie dans quelle zone il entre
            if (gameObject.CompareTag("LabZone") && currentZone != "Lab") 
            {
                currentZone = "Lab";
                StopAllCoroutines();
                StartCoroutine(ChangeLight(labIntensity, labColor, labRange));
            }
            else if (gameObject.CompareTag("InsideZone") && currentZone != "Inside") 
            {
                currentZone = "Inside";
                StopAllCoroutines();
                StartCoroutine(ChangeLight(insideIntensity, insideColor, insideRange));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            if (currentZone == "Lab" || currentZone == "Inside") 
            {
                currentZone = "Outside";
                StopAllCoroutines();
                StartCoroutine(ChangeLight(outsideIntensity, outsideColor, outsideRange));
            }
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

        pointLight.intensity = targetIntensity;
        pointLight.range = targetRange;
        pointLight.color = targetColor;
    }
}

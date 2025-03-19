using System.Collections;
using UnityEngine;

public class FlameActivation : MonoBehaviour
{
    private ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();

        if (particleSystem != null)
        {
            particleSystem.Stop();
        }

        // S'abonner à l'événement du LighterActivation
        LighterActivation.OnLighterActivated += ActivateParticles;
    }

    void OnDestroy()
    {
        // Se désabonner pour éviter les erreurs
        LighterActivation.OnLighterActivated -= ActivateParticles;
    }

    private void ActivateParticles()
    {
        if (particleSystem != null)
        {
            particleSystem.Play(); // Active les particules
            StartCoroutine(DisableParticlesAfterDelay());
        }
    }

    private IEnumerator DisableParticlesAfterDelay()
    {
        yield return new WaitForSeconds(1f); // Attendre 2 secondes
        if (particleSystem != null)
        {
            particleSystem.Stop(); // Désactive les particules
        }
    }
}

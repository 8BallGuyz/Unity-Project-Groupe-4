using UnityEngine;
using System.Collections;

public class LightningEffect : MonoBehaviour
{
    public Light lightningLight;
    public AudioSource thunderSound;
    public float minTime = 5f;
    public float maxTime = 15f;

    void Start()
    {
        StartCoroutine(LightningRoutine());
    }

    IEnumerator LightningRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));

            // Flash de l'éclair
            lightningLight.intensity = 50;
            yield return new WaitForSeconds(0.1f);
            lightningLight.intensity = 0;

            // Son du tonnerre avec un léger délai
            yield return new WaitForSeconds(Random.Range(0.2f, 1.5f));
            thunderSound.Play();
        }
    }
}

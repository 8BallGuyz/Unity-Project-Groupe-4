using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnKey : MonoBehaviour
{
    [System.Serializable]
    public class KeySoundPair
    {
        public KeyCode key;         // Touche à presser
        public AudioClip soundClip; // Son à jouer
    }

    public List<KeySoundPair> keySoundPairs = new List<KeySoundPair>(); // Liste de touches et sons
    private Dictionary<KeyCode, AudioClip> soundMap = new Dictionary<KeyCode, AudioClip>();
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Remplir le dictionnaire avec les touches et sons
        foreach (var pair in keySoundPairs)
        {
            if (!soundMap.ContainsKey(pair.key) && pair.soundClip != null)
            {
                soundMap.Add(pair.key, pair.soundClip);
            }
        }
    }

    void Update()
    {
        foreach (var pair in keySoundPairs)
        {
            if (Input.GetKeyDown(pair.key) && pair.soundClip != null)
            {
                PlaySound(pair.soundClip);
            }
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop(); // Arrête le son en cours si nécessaire
        }
        audioSource.clip = clip;
        audioSource.Play();
    }
}
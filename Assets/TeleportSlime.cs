using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSlime : MonoBehaviour
{
    public GameObject[] puds; // Positions possibles
    public GameObject slimePrefab; // Prefab du Slime

    private bool isOriginal = true; // Indique si c'est le Slime de base
    private int cloneCount = 0; // Nombre de clones créés
    private int maxClones = 20; // Limite de clones

    private void Start()
    {
        if (puds == null || puds.Length == 0)
        {
            Debug.LogError("Aucun point de téléportation assigné !");
            return;
        }

        if (slimePrefab == null)
        {
            Debug.LogError("Aucun prefab de Slime assigné !");
            return;
        }

        // Seul le Slime original lance la boucle de téléportation
        if (isOriginal)
        {
            StartCoroutine(TeleportLoop());
        }
    }

    private IEnumerator TeleportLoop()
    {
        while (cloneCount < maxClones) // Arrête la boucle après 20 clones
        {
            yield return new WaitForSeconds(1f);
            TeleportAndDuplicate();
        }
    }

    private void TeleportAndDuplicate()
    {
        if (puds.Length == 0 || cloneCount >= maxClones) return;

        int randomIndex = Random.Range(0, puds.Length);
        Vector3 newPosition = puds[randomIndex].transform.position;

        // Téléporte uniquement le Slime original
        transform.position = newPosition;

        // Crée un clone à la nouvelle position
        GameObject clone = Instantiate(slimePrefab, newPosition, Quaternion.identity);

        // Marque le clone comme non-original (il ne pourra pas se dupliquer)
        clone.GetComponent<TeleportSlime>().isOriginal = false;

        // Incrémente le compteur de clones
        cloneCount++;

        Debug.Log($"Slime téléporté et clone créé ({cloneCount}/{maxClones}) à : {newPosition}");
    }
}

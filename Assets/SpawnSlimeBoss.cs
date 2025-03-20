using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSlimeBoss : MonoBehaviour
{
    // Start is called before the 
    //     public GameObject slimeBossPrefab; // Le prefab du Slime Boss
    public Transform spawnPoint; // Point où le Slime Boss va apparaître
    private bool hasSpawned = false;

    public GameObject SlimeBoss;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        private void OnTriggerEnter(Collider other)
    {
        // Vérifie si c'est le joueur qui entre dans le trigger
        if (other.CompareTag("Player") && !hasSpawned)
        {
            SpawnBoss();
        }
    }

    private void SpawnBoss()
    {
        if (SlimeBoss  == null || spawnPoint == null)
        {
            Debug.LogError("Slime Boss Prefab ou Spawn Point non assigné !");
            return;
        }

        Instantiate(SlimeBoss, spawnPoint.position, Quaternion.identity);
        hasSpawned = true; // Empêche un spawn multiple
        Debug.Log("Slime Boss a spawn !");
    }

}

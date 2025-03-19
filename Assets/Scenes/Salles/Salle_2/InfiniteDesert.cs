using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteDesert : MonoBehaviour
{
    public GameObject desertPrefab; // 🏜️ La plaque à spawn
    public float tileSize = 10f; // 📏 Taille d'une plaque

    public GameObject structureA;
    public GameObject structureB;
    public GameObject structureC;
    public GameObject structureD;
    public GameObject structureE;
    public GameObject structureF;
    public GameObject structureG;
    public GameObject structureH;
    public GameObject structureI;
    public GameObject structureFinal;
    public GameObject structureX;

    private List<GameObject> spawnedStructures = new List<GameObject>(); // Liste des structures générées
    private float structureDespawnDistance = 150f; // Distance seuil pour supprimer les structures


    private static HashSet<Vector2> spawnedTiles = new HashSet<Vector2>(); // 📌 Liste des tuiles créées
    private static int tileCount = 0; // 🏗️ Nombre total de plaques générées

    void Start()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.z);
        spawnedTiles.Add(pos); // 📌 On enregistre cette tuile
        StartCoroutine(CheckAndDestroyFarStructures());
    }

    public void OnPlayerEnterTrigger(string direction)
    {
        Vector3 currentPos = transform.position; // 📌 Position actuelle de la plaque
        Vector3 newTilePos = Vector3.zero;

        // 🔹 Détermine où générer la nouvelle tuile
        switch (direction)
        {
            case "Top":
                newTilePos = new Vector3(currentPos.x, currentPos.y, currentPos.z + tileSize);
                break;
            case "Bottom":
                newTilePos = new Vector3(currentPos.x, currentPos.y, currentPos.z - tileSize);
                break;
            case "Left":
                newTilePos = new Vector3(currentPos.x - tileSize, currentPos.y, currentPos.z);
                break;
            case "Right":
                newTilePos = new Vector3(currentPos.x + tileSize, currentPos.y, currentPos.z);
                break;
        }

        // 📌 Vérifie si la tuile existe déjà avant de la spawn
        Vector2 tileCheckPos = new Vector2(newTilePos.x, newTilePos.z);
        if (!spawnedTiles.Contains(tileCheckPos))
        {
            SpawnTile(newTilePos);
        }
    }

    public void OnPlayerExitTrigger()
    {
        StartCoroutine(DestroyIfFar()); // ⏳ Détruit la plaque après un délai
    }

    void SpawnTile(Vector3 position)
    {
        GameObject newTile = Instantiate(desertPrefab, position, Quaternion.identity);
        newTile.name = "sol";
        Vector2 tileCheckPos = new Vector2(position.x, position.z);
        spawnedTiles.Add(tileCheckPos);
        tileCount++; // 🔢 Incrémente le compteur de plaques
        TrySpawnStructure(position);

        // 🔹 Renommer chaque enfant du clone pour éviter les conflits
        int index = 0;
        foreach (Transform child in newTile.transform)
        {
            child.name = $"Trigger_{index}_{position.x}_{position.z}";
            index++;
        }

        Debug.Log($"🛠️ Nouvelle plaque spawnée : {newTile.name}, enfants renommés.");
    }



    void TrySpawnStructure(Vector3 position)
    {
        if (tileCount % 15 == 0) // 📌 Une plaque sur 20
        {
            int roll = Random.Range(1, 101); // 🎲 Nombre entre 1 et 100

            if (roll <= 10) // 🎯 10 % de chance pour Structure_A
            {
                Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                GameObject newStructure = Instantiate(structureA, position + Vector3.up, randomRotation);
                Debug.Log("🏗️ Structure_A générée !");
                spawnedStructures.Add(newStructure);

            }
            else if (roll <= 20) // 🎯 10 % de chance pour Structure_B
            {
                Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                GameObject newStructure = Instantiate(structureB, position + Vector3.up, randomRotation);
                Debug.Log("🏗️ Structure_B générée !");
                spawnedStructures.Add(newStructure);

            }
            else if (roll <= 30) // 🎯 10 % de chance pour Structure_C
            {
                Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                GameObject newStructure = Instantiate(structureC, position + Vector3.up, randomRotation);
                Debug.Log("🏗️ Structure_C générée !");
                spawnedStructures.Add(newStructure);

            }
            else if (roll <= 40) // 🎯 10 % de chance pour Structure_D
            {
                Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                GameObject newStructure = Instantiate(structureD, position + Vector3.up, randomRotation);
                Debug.Log("🏗️ Structure_D générée !");
                spawnedStructures.Add(newStructure);

            }
            else if (roll <= 50) // 🎯 10 % de chance pour Structure_E
            {
                Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                GameObject newStructure = Instantiate(structureE, position + Vector3.up, randomRotation);
                Debug.Log("🏗️ Structure_E générée !");
                spawnedStructures.Add(newStructure);

            }
            else if (roll <= 60) // 🎯 10 % de chance pour Structure_F
            {
                Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                GameObject newStructure = Instantiate(structureF, position + Vector3.up, randomRotation);
                Debug.Log("🏗️ Structure_F générée !");
                spawnedStructures.Add(newStructure);

            }
            else if (roll <= 70) // 🎯 10 % de chance pour Structure_G
            {
                Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                GameObject newStructure = Instantiate(structureG, position + Vector3.up, randomRotation);
                Debug.Log("🏗️ Structure_G générée !");
                spawnedStructures.Add(newStructure);

            }
            else if (roll <= 80) // 🎯 10 % de chance pour Structure_H
            {
                Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                GameObject newStructure = Instantiate(structureH, position + Vector3.up, randomRotation);
                Debug.Log("🏗️ Structure_H générée !");
                spawnedStructures.Add(newStructure);

            }
            else if (roll <= 90) // 🎯 10 % de chance pour Structure_I
            {
                Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                GameObject newStructure = Instantiate(structureI, position + Vector3.up, randomRotation);
                Debug.Log("🏗️ Structure_I générée !");
                spawnedStructures.Add(newStructure);

            }
        }

        if (tileCount % 10 == 0) // 📌 Une plaque sur 100
        {
            int roll = Random.Range(1, 101);
            if (roll >= 90) // 🎯 10 % de chance pour Structure_Final
            {
                Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                GameObject newStructure = Instantiate(structureFinal, position + Vector3.up, randomRotation);
                Debug.Log("🔥 Structure_Final générée !");
                spawnedStructures.Add(newStructure);

            }
        }

        for (int i = 0; i < 10; i++)
        {
            // 🎲 Position aléatoire sur la plaque
            float offsetX = Random.Range(-tileSize / 2f, tileSize / 2f);
            float offsetZ = Random.Range(-tileSize / 2f, tileSize / 2f);
            Vector3 randomPosition = position + new Vector3(offsetX, 0, offsetZ);

            // 🎲 Rotation aléatoire
            Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

            // 🎲 Échelle aléatoire
            float randomScale = Random.Range(1.5f, 2.5f);

            // 🏗️ Instanciation de la structure
            GameObject newStructure = Instantiate(structureX, randomPosition, randomRotation);
            
            // 🔧 Appliquer l'échelle aléatoire
            newStructure.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            // 🔹 Faire de la structure un enfant de la tuile
            newStructure.transform.SetParent(transform);

            Debug.Log($"🛠️ Structure_X générée à {randomPosition} avec une échelle de {randomScale}");
        }

    }


    IEnumerator DestroyIfFar()
    {
        yield return new WaitForSeconds(0f); // ⏳ Délai pour éviter la suppression instantanée

        Vector2 myPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 playerPos = new Vector2(GameObject.FindGameObjectWithTag("Player").transform.position.x,
                                        GameObject.FindGameObjectWithTag("Player").transform.position.z);

        float dist = Vector2.Distance(myPos, playerPos);

        Debug.Log($"🔍 Vérification destruction : Plaque {name} | Distance : {dist} | Seuil : {tileSize * 1.5f}");


        Debug.Log($"🔥 Suppression de la plaque : {name}");
        spawnedTiles.Remove(myPos);
        Destroy(gameObject);


    }

    IEnumerator CheckAndDestroyFarStructures()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f); // Vérification toutes les 5 secondes

            Vector2 playerPos = new Vector2(GameObject.FindGameObjectWithTag("Player").transform.position.x,
                                            GameObject.FindGameObjectWithTag("Player").transform.position.z);

            for (int i = spawnedStructures.Count - 1; i >= 0; i--)
            {
                if (spawnedStructures[i] == null) continue;

                Vector2 structurePos = new Vector2(spawnedStructures[i].transform.position.x,
                                                spawnedStructures[i].transform.position.z);
                float dist = Vector2.Distance(playerPos, structurePos);

                if (dist > structureDespawnDistance)
                {
                    Debug.Log($"🔥 Suppression structure {spawnedStructures[i].name} (Distance: {dist})");
                    Destroy(spawnedStructures[i]);
                    spawnedStructures[i] = null;
                }
            }

            // Nettoyage de la liste après suppression
            spawnedStructures.RemoveAll(item => item == null);
        }
    }



}

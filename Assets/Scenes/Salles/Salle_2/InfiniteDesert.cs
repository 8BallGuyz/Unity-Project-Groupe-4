using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteDesert : MonoBehaviour
{
    public GameObject desertPrefab; // 🏜️ La plaque à spawn
    public float tileSize = 10f; // 📏 Taille d'une plaque

    private static HashSet<Vector2> spawnedTiles = new HashSet<Vector2>(); // 📌 Liste des tuiles créées

    void Start()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.z);
        spawnedTiles.Add(pos); // 📌 On enregistre cette tuile
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

        // 🔹 Renommer chaque enfant du clone pour éviter les conflits
        int index = 0;
        foreach (Transform child in newTile.transform)
        {
            child.name = $"Trigger_{index}_{position.x}_{position.z}";
            index++;
        }

        Debug.Log($"🛠️ Nouvelle plaque spawnée : {newTile.name}, enfants renommés.");
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

}

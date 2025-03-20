using UnityEngine;

public class DesertTerrain : MonoBehaviour
{
    public float noiseScale = 0.1f; // 📏 Plus c'est petit, plus les ondulations sont grandes
    public float heightMultiplier = 2f; // 🔺 Hauteur maximale des dunes

    void Start()
    {
        GenerateWaves();
    }

    void GenerateWaves()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null) return;
        Mesh mesh = Instantiate(meshFilter.sharedMesh); // 📌 Création d'un nouveau Mesh pour éviter les conflits
        meshFilter.mesh = mesh;
        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            float x = transform.position.x + vertices[i].x; 
            float z = transform.position.z + vertices[i].z;

            // 🏜️ Ajout d'une variation de hauteur avec Perlin Noise
            float yOffset = Mathf.PerlinNoise(x * noiseScale, z * noiseScale) * heightMultiplier;
            vertices[i] += new Vector3(0, yOffset, 0);
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals(); // 🌞 Corrige l'éclairage pour que ça rende bien
    }
}

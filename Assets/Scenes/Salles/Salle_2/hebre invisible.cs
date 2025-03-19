using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassEffect : MonoBehaviour
{
    public Transform player; // 🎯 Référence au joueur
    public float minDistance = 1f;  // 📏 Distance où l'herbe est totalement invisible
    public float maxDistance = 10f; // 📏 Distance où l'herbe est totalement visible

    public float maxTilingX = 10f; // 📏 Tiling X maximal lorsque le joueur est loin
    public float minTilingX = 0f;  // 📏 Tiling X minimal lorsque le joueur est proche

    private Renderer grassRenderer;
    private Color originalColor;

    void Start()
    {
        grassRenderer = GetComponent<Renderer>();

        if (grassRenderer != null)
        {
            originalColor = grassRenderer.material.color;
        }

        // Trouver le joueur si non assigné
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (grassRenderer == null || player == null) return;

        // 🏃 Distance entre le joueur et l'herbe
        float distance = Vector3.Distance(transform.position, player.position);

        // 🎚️ Calcul de la transparence
        float alpha;
        if (distance <= minDistance)
        {
            alpha = 0f; // Totalement invisible
        }
        else if (distance >= maxDistance)
        {
            alpha = 1f; // Totalement visible
        }
        else
        {
            // Interpolation entre invisible et visible
            alpha = (distance - minDistance) / (maxDistance - minDistance);
        }

        // 🎨 Appliquer la transparence
        Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        grassRenderer.material.color = newColor;

        // 🎚️ Modifier le tiling X de la texture
        float newTilingX = Mathf.Lerp(minTilingX, maxTilingX, alpha);
        Vector2 newTiling = new Vector2(newTilingX, grassRenderer.material.mainTextureScale.y);
        grassRenderer.material.mainTextureScale = newTiling;
    }
}

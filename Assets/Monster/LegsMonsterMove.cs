using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegMovement : MonoBehaviour
{
    public Transform[] legs; // Pattes assignées depuis l’Inspector
    public float speed = 5f; // Vitesse du mouvement
    public float amplitude = 0.2f; // Hauteur du mouvement

    private Vector3[] initialPositions;

    void Start()
    {
        initialPositions = new Vector3[legs.Length];
        for (int i = 0; i < legs.Length; i++)
        {
            initialPositions[i] = legs[i].localPosition;
        }
    }

    void Update()
    {
        for (int i = 0; i < legs.Length; i++)
        {
            float offset = i * Mathf.PI * 0.5f; // Décalage progressif des pattes
            legs[i].localPosition = initialPositions[i] + new Vector3(0, Mathf.Sin(Time.time * speed + offset) * amplitude, 0);
        }
    }
}

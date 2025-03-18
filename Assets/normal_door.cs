using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normal_door : MonoBehaviour
{
    public GameObject interactUI; // UI "E pour ouvrir"
    public Animator doorAnimator; // Référence à l'Animator de la porte
    private bool isPlayerNear = false;
    public BoxCollider cl;

    public float time = 0;
    public float end = 3f;
    private bool cooldown = false;

    private void Start()
    {
        interactUI.SetActive(false); // Cache le message au début
        cl = GetComponent<BoxCollider>();
        cl.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            interactUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            interactUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && cooldown == false)
        {
            interactUI.SetActive(false); // Cache le message

            // Joue l'animation d'ouverture de porte
            doorAnimator.SetTrigger("Open");

            cooldown = true;

            cl.enabled = false;
        }

        if (cooldown == true)
        {
            time = time + Time.deltaTime;
            if (time >= end)
            {
                cooldown = false;
                cl.enabled = true;
                interactUI.SetActive(false);
                time = 0;
            }
        }
    }
}

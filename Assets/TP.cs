using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP : MonoBehaviour
{
    PlayerMovement playerController;
    CharacterController characterController;
    Rigidbody rb;

    private Vector3 savedPosition; 

    void Start()
    {
        playerController = gameObject.GetComponent<PlayerMovement>();
        characterController = gameObject.GetComponent<CharacterController>();
        rb = gameObject.GetComponent<Rigidbody>();


        AnimationSwitcher.OnTurningStarted += StartTeleportSequence;
    }

    void OnDestroy()
    {

        AnimationSwitcher.OnTurningStarted -= StartTeleportSequence;
    }

    void StartTeleportSequence()
    {
        Debug.Log("Animation détectée ! Position sauvegardée.");
        savedPosition = transform.position;
        StartCoroutine(DelayedTeleport());
    }

    IEnumerator DelayedTeleport()
    {
        Debug.Log("Téléportation dans 10 secondes...");
        yield return new WaitForSeconds(10f);

        Debug.Log("Téléportation vers la position sauvegardée : " + savedPosition);

        if (characterController != null) characterController.enabled = false;
        if (rb != null) rb.isKinematic = true;

        playerController.disable = true;
        transform.position = savedPosition;
        yield return new WaitForSeconds(1f);
        playerController.disable = false;

        if (characterController != null) characterController.enabled = true;
        if (rb != null) rb.isKinematic = false;

        Debug.Log("Fin de la téléportation.");
    }
}


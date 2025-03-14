using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSwitcher : MonoBehaviour
{
    private Animator animator;
    private bool hasClickedLeft = false; // Empêche de cliquer plusieurs fois

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasClickedLeft) // Clic gauche (une seule fois)
        {
            animator.Play("Turning", 0, 0f);
            animator.SetBool("isPlaying", true);
            hasClickedLeft = true; // Désactive le clic gauche après une utilisation
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMedKit : MonoBehaviour
{
    private Animator animator;
    private bool hasClickedLeft = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasClickedLeft) 
        {
            Debug.Log("MedKit utilisé ------------------------");
            animator.SetBool("isPlaying", true);
            hasClickedLeft = true; 

        }
    }
}


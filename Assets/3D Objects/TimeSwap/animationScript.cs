using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSwitcher : MonoBehaviour
{
    private Animator animator;
    private bool hasClickedLeft = false;

    public delegate void AnimationStarted(); 
    public static event AnimationStarted OnTurningStarted; 

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasClickedLeft) 
        {
            animator.Play("Turning", 0, 0f);
            animator.SetBool("isPlaying", true);
            hasClickedLeft = true; 
            
            if (OnTurningStarted != null)
            {
                OnTurningStarted();
            }
        }
    }
}


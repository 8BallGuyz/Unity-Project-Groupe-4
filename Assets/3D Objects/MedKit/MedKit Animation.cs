using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMedKit : MonoBehaviour
{

    public PlayerMovement player2;
    public stats_manager stats2;
    private Animator animator;
    private bool hasClickedLeft = false;

    void Start()
    {
        player2 = FindObjectOfType<PlayerMovement>();
        stats2 = FindObjectOfType<stats_manager>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasClickedLeft) 
        {
            Debug.Log("MedKit utilisé ------------------------");
            animator.SetBool("isPlaying", true);
            hasClickedLeft = true; 
            player2.hp += 50;
            if (player2.hp >= 100)
            {
                player2.hp = 100;
            }
            stats2.HpUI(player2.hp);
        }
    }
}


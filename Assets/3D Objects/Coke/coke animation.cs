using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CokeAnimation : MonoBehaviour
{
    private Animator animator;
    private bool hasClickedLeft = false;

    public PlayerMovement player3;
    public stats_manager stats3;

    private bool Activator = false;

    private float timer = 0f;
    private float endTimer = 5f;

    public PlayerEquipment playerEquipment;

    public InventorySystem inventorySystem;



    private float CurrentWalkSpeed;

    private float CurrentSprintSpeed;

    private int equippedIndex;


    void Start()
    {
        animator = GetComponent<Animator>();
        player3 = FindObjectOfType<PlayerMovement>();
        stats3 = FindObjectOfType<stats_manager>();
        CurrentWalkSpeed = player3.walkSpeed;
        CurrentSprintSpeed = player3.defaultSprintSpeed;

        inventorySystem = FindObjectOfType<InventorySystem>();
        equippedIndex = FindObjectOfType<InventoryUI>().GetEquippedItemIndex();
    }


    void Update()
    {
        if (Activator == true) {
            timer = timer + Time.deltaTime;
            player3.stamina = 100;
            player3.walkSpeed = CurrentWalkSpeed + 2;
            player3.defaultSprintSpeed = CurrentSprintSpeed + 4;
            if (timer >= endTimer)
            {
                inventorySystem.RemoveItem(equippedIndex);
                timer = 0;
                player3.walkSpeed = CurrentWalkSpeed;
                player3.defaultSprintSpeed = CurrentSprintSpeed;
                Activator = false;
            }
        }

        if (Input.GetMouseButtonDown(0) && !hasClickedLeft) 
        {
            Activator = true;
            animator.SetBool("isPlaying", true);
            hasClickedLeft = true; 
            

        }
    }
}


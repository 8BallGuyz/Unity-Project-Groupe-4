using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMedKit : MonoBehaviour
{
    public PlayerMovement player2;
    public stats_manager stats2;
    private Animator animator;
    private bool hasClickedLeft = false;
    public InventorySystem inventorySystem2;

    void Start()
    {
        player2 = FindObjectOfType<PlayerMovement>();
        stats2 = FindObjectOfType<stats_manager>();
        animator = GetComponent<Animator>();
        inventorySystem2 = FindObjectOfType<InventorySystem>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasClickedLeft) 
        {
            hasClickedLeft = true;
            int equippedIndex = FindObjectOfType<InventoryUI>().GetEquippedItemIndex(); // ✅ Récupération ici

            Debug.Log("MedKit utilisé ------------------------");
            animator.SetBool("isPlaying", true);

            // Soigne le joueur
            player2.hp += 50;
            if (player2.hp > 100)
            {
                player2.hp = 100;
            }
            stats2.HpUI(player2.hp);

            StartCoroutine(RemoveItemAfterDelay(4f, equippedIndex)); // ✅ Passe l'index correct à la coroutine
        }
    }

    IEnumerator RemoveItemAfterDelay(float delay, int itemIndex)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Suppression du MedKit après 4 secondes");
        inventorySystem2.RemoveItem(itemIndex);
    }
}

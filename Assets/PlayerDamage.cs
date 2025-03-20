using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public PlayerMovement player;
    public stats_manager stats;   
    private bool canTakeDamage = true; 

    private void Start()
    {
        if (player == null)
        {
            player = GetComponent<PlayerMovement>(); 
        }

        if (stats == null)
        {
            stats = FindObjectOfType<stats_manager>(); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canTakeDamage) 
        {
            if (other.CompareTag("Slime"))
            {
                StartCoroutine(TakeDamage(20)); 
            }
            else if (other.CompareTag("SlimeBoss"))
            {
                StartCoroutine(TakeDamage(100)); 
            }
        }
    }

    IEnumerator TakeDamage(int damage)
    {
        canTakeDamage = false;
        player.hp -= damage;
        player.hp = Mathf.Max(player.hp, 0); 


        stats.HpUI(player.hp);

        Debug.Log($"Touché par {damage} dégâts ! HP restants : {player.hp}");

        yield return new WaitForSeconds(2f); 

        canTakeDamage = true; 
    }
}


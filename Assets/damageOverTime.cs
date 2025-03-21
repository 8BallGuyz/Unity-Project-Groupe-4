using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageOverTime : MonoBehaviour
{
    public PlayerMovement player;
    public stats_manager stats;

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
        
        StartCoroutine(TakeDamageOverTime());
    }

    IEnumerator TakeDamageOverTime()
    {
        while (true)
        {
            player.hp -= 5;
            player.hp = Mathf.Max(player.hp, 0);
            stats.HpUI(player.hp);
            Debug.Log($"Dégâts constants : -5 HP. HP restants : {player.hp}");
            yield return new WaitForSeconds(1f);
        }
    }
}
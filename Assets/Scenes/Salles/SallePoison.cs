using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SallePoison : MonoBehaviour
{
    //public GameObject poison; 
    private Transform player;
    private GameObject poison;
    public float activationDistance = 1f; 

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        poison = transform.GetChild(0).gameObject;
        // Masquer le poison au début
        poison.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        Debug.Log(distance);
    
        if (distance < activationDistance)
        {
            poison.SetActive(true); 
        }
        else
        {
            poison.SetActive(false); 
        }
    }
}

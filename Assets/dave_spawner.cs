using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dave_spawner : MonoBehaviour
{
    public GameObject dave;
    public PlayerMovement player;
    public Transform spawnPoint;
    public Collider cl;
    public Canvas warning;

    public float timer = 0;
    public float end = 30;

    public float timer2 = 0;
    public float end2 = 3;
    private bool Activated = false;
    private bool Activated2 = false;
    // Start is called before the first frame update
    void Start()
    {
        cl = GetComponent<Collider>();
        warning.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Activated == true)
        {
            timer = timer + Time.deltaTime;
            if(timer >= end)
            {
                Instantiate(dave, spawnPoint.position, Quaternion.identity);
                Activated = false;
                timer = 0;
            }
        }

        if (Activated2 == true)
        {
            timer2 = timer2 + Time.deltaTime;
            if (timer2 >= end2)
            {
                warning.enabled = false;
                timer2 = 0;
                Activated2 = false;
            }
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            cl.enabled = false;
            Activated = true;
            warning.enabled = true;
            Activated2 = true;
            player.walkSpeed = 6;
            player.sprintSpeed = 8;
        }
    }
}

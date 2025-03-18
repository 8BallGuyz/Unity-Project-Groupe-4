using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level1_manager : MonoBehaviour
{
    public PlayerMovement player;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;

    public GameObject ghost;

    public float timer = 0;
    public float end = 3;
    private bool cooldown = false;

    private bool popup1 = true;
    private bool popup2 = false;
    private bool popup3 = false;
    private bool popupfinal = false;

    // Start is called before the first frame update
    void Start()
    {
        text1.SetActive(false);
        text2.SetActive(false);
        text3.SetActive(false);

        ghost.SetActive(false);

        player.movementManager = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown == false && popup1 == true)
        {
            timer = timer + Time.deltaTime;
            if (timer >= end)
            {
                ghost.SetActive(true);

                text1.SetActive(true);
                text2.SetActive(false);
                text3.SetActive(false);
                timer = 0;
                popup1 = false;
                popup2 = true;
                popup3 = false;
                popupfinal = false;

                player.movementManager = false;
            }
        }

        if (cooldown == false && popup2 == true)
        {
            timer = timer + Time.deltaTime;
            if (timer >= end)
            {
                ghost.SetActive(true);

                text1.SetActive(false);
                text2.SetActive(true);
                text3.SetActive(false);
                timer = 0;
                popup1 = false;
                popup2 = false;
                popup3 = true;
                popupfinal = false;

                player.movementManager = false;
            }
        }

        if (cooldown == false && popup3 == true)
        {
            timer = timer + Time.deltaTime;
            if (timer >= end)
            {
                ghost.SetActive(true);

                text1.SetActive(false);
                text2.SetActive(false);
                text3.SetActive(true);
                timer = 0;
                popup1 = false;
                popup2 = false;
                popup3 = false;
                popupfinal = true;

                player.movementManager = false;
            }
        }

        if (cooldown == false && popupfinal == true)
        {
            timer = timer + Time.deltaTime;
            if (timer >= end)
            {
                ghost.SetActive(false);

                text1.SetActive(false);
                text2.SetActive(false);
                text3.SetActive(false);
                timer = 0;
                popup1 = false;
                popup2 = false;
                popup3 = false;
                popupfinal = false;

                player.movementManager = true;
            }
        }
    }
}

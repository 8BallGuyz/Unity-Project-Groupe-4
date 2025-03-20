using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key_script : MonoBehaviour
{
    public PlayerMovement player;
    public Canvas KeyCollected;
    public float timer = 0;
    public float end = 1.5f;
    private bool Interracted = false;
    // Start is called before the first frame update
    void Start()
    {
        KeyCollected.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 1);

        if (Interracted == true)
        {
            timer = timer + Time.deltaTime;
            if (timer >= end)
            {
                KeyCollected.enabled = false;
                Destroy(gameObject);
            }
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.position = new Vector3(0, -100f, 0);
            player.key = 1;
            Interracted = true;
            KeyCollected.enabled = true;
        }
    }
}

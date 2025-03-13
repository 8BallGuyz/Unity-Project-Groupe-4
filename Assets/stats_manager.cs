using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stats_manager : MonoBehaviour
{
    public PlayerMovement player;
    public Text hp;
    public Text stamina;
    public Text sound;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        hp.text = " " + player.hp.ToString();
        stamina.text = " " + player.stamina.ToString();
        sound.text = " " + player.sound.ToString();
    }
}

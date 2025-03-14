using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stats_manager : MonoBehaviour
{
    public PlayerMovement player;

    public Slider staminaSlider;
    public Slider soundSlider;
    public Slider hpSlider;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        player = FindObjectOfType<PlayerMovement>();
    }


    // STAMINA
    public void SetMaxStaminaUI(float stamina)
    {
        staminaSlider.maxValue = player.staminaCap;
        staminaSlider.value = player.stamina;
    }

    public void StaminaUI(float stamina)
    {
        staminaSlider.value = player.stamina;
    }

    // SOUND
    public void SetMaxSoundUI(float sound)
    {
        soundSlider.maxValue = player.soundCap;
        soundSlider.value = player.sound;
    }

    public void SoundUI(float sound)
    {
        soundSlider.value = player.sound;
    }

    // HP
    public void SetMaxHpUI(float hp)
    {
        hpSlider.maxValue = player.maxhp;
        hpSlider.value = player.hp;
    }

    public void HpUI(float hp)
    {
        hpSlider.value = player.hp;
    }
}

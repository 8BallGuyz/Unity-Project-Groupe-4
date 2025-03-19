using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeAmbiance : MonoBehaviour
{
    public Slider VolumeSlider;
    public AudioSource PlayAmbiance;

    public void start(){
        if (PlayerPrefs.HasKey("soundVolume")){
            LoadVolume();
        } else {
            PlayerPrefs.SetFloat("soundVolume", 1);
            LoadVolume();
        }
    }

    public void SetVolume(){
        PlayAmbiance.volume = VolumeSlider.value;
        SaveVolume();
    }

    public void SaveVolume(){
        PlayerPrefs.SetFloat("soundVolume", VolumeSlider.value);
    }

    public void LoadVolume(){
        VolumeSlider.value = PlayerPrefs.GetFloat("soundVolume");
    }
}

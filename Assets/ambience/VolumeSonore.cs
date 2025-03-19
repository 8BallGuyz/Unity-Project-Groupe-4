using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSonore : MonoBehaviour
{
    public Slider VolumeMusic;
    public AudioSource Play;

    public void start(){
        if (PlayerPrefs.HasKey("musicVolume")){
            LoadVolume();
        } else {
            PlayerPrefs.SetFloat("musicVolume", 1);
            LoadVolume();
        }
    }

    public void SetVolume(){
        Play.volume = VolumeMusic.value;
        SaveVolume();
    }

    public void SaveVolume(){
        PlayerPrefs.SetFloat("musicVolume", VolumeMusic.value);
    }

    public void LoadVolume(){
        VolumeMusic.value = PlayerPrefs.GetFloat("musicVolume");
    }
}

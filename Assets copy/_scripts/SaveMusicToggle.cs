using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveMusicToggle : MonoBehaviour
{
    public int music_toggle;
    public AudioSource sound;
    public Sprite On;
    public Sprite Off;
    public Image ButtonImage;

    private void Start()
    {
        if (sound == null)
        {
            sound = GameObject.Find("Audio Manager").GetComponent<AudioSource>();
        }
        music_toggle = PlayerPrefs.GetInt("MusicVolume", 1); //get this setting - if it doesn't exsist set it to 1
        SoundCheck();
    }

    private void Update()
    {
        if(sound == null)
        {
            sound = GameObject.Find("Audio Manager").GetComponent<AudioSource>();
            SoundCheck();
        }
    }

    public void SaveSound()
    {
        if (music_toggle == 1)
        {
            PlayerPrefs.SetInt("MusicVolume", 0);
            music_toggle = 0;
            SoundCheck();
            return;
        }

        if (music_toggle == 0)
        {
            PlayerPrefs.SetInt("MusicVolume", 1);
            music_toggle = 1;
            SoundCheck();
            return;
        }
    }


    public void SoundCheck()
    {
        if (music_toggle == 1)
        {
            sound.UnPause();
            ButtonImage.sprite = On;
        }
        if (music_toggle == 0)
        {
            sound.Pause();
            ButtonImage.sprite = Off;
        }
    }
}

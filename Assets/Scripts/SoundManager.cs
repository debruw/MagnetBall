using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public static SoundManager Instance { get { return _instance; } }

    public AudioSource[] gameSoundsList;
    public AudioSource[] characterSoundsList;


    //Sound Enums
    public enum GameSounds
    {
        Music
    }

    public enum CharacterSounds
    {

    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        if (!PlayerPrefs.HasKey("SOUND"))
        {
            PlayerPrefs.SetInt("SOUND", 1);
        }
        if (!PlayerPrefs.HasKey("VIBRATION"))
        {
            PlayerPrefs.SetInt("VIBRATION", 1);
        }
    }


    public void playSound(GameSounds soundType)
    {
        if (PlayerPrefs.GetInt("SOUND").Equals(1))
        {
            gameSoundsList[(int)soundType].Play();
        }
    }

    public void stopSound(GameSounds soundType)
    {
        gameSoundsList[(int)soundType].Stop();
    }
    
    public void playSound(CharacterSounds soundType)
    {
        if (PlayerPrefs.GetInt("SOUND").Equals(1))
        {
            characterSoundsList[(int)soundType].Play();
        }
    }

    public void stopSound(CharacterSounds soundType)
    {
        characterSoundsList[(int)soundType].Stop();
    }
}

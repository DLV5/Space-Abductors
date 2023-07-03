using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource PlayerSource;

    //public AudioData Data;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        Weapon.Shooted += PlayShootSound;
        InputHandler.ReleasingShootButton += StopSoundLoop;
    }

    private void PlayShootSound()
    {
        //if (PlayerSource.isPlaying)
        PlayerSource.Play();
    }

    private void StopSoundLoop()
    {
        if (PlayerSource.loop)
            PlayerSource.Stop();
    }

    public void ChangeSound(AudioClip clip, bool loop = false)
    {
        PlayerSource.clip = clip;
        if (loop)
            PlayerSource.loop = true;
        else
            PlayerSource.loop = false; // in case previous weapon was looping
    }
}

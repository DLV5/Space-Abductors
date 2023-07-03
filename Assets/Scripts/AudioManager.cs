using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource PlayerSource;

    [SerializeField] private AudioData _audioData;

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
    }

    private void PlayShootSound()
    {
        if (PlayerSource.isPlaying)
            PlayerSource.Play();
    }
}

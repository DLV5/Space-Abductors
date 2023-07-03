using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private bool _looping;

    private void Start()
    {
        SetSound();
    }

    private void OnEnable()
    {
        SetSound();
    }

    private void SetSound()
    {
        AudioManager.Instance?.ChangeSound(_audioClip, _looping);
    }
}

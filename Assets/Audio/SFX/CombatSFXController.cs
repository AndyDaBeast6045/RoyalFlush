using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSFXController : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundEffects;
    [SerializeField] private AudioSource audioSource;
    private bool sfxFlip = true;
    
    public void PlayBlunt()
    {
        if (sfxFlip)
        {
            audioSource.PlayOneShot(soundEffects[2], 1.0f);
            sfxFlip = false;
        }
        else
        {
            audioSource.PlayOneShot(soundEffects[3], 1.0f);
            sfxFlip = true;
        }
    }

    public void PlaySlash()
    {
        if (sfxFlip)
        {
            audioSource.PlayOneShot(soundEffects[0], 1.0f);
            sfxFlip = false;
        }
        else
        {
            audioSource.PlayOneShot(soundEffects[1], 1.0f);
            sfxFlip = true;
        }
    }

    public void PlayHeal()
    {

    }

    public void PlayBurn()
    {
        audioSource.PlayOneShot(soundEffects[5], 1.0f);
    }

    public void PlayShield()
    {

    }
}

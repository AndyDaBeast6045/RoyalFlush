using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class volume : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void setMasterVolume(float level)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(level)*20f);
    }
    public void setMusicVolume(float level)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
    }
    public void setSFXVolume(float level)
    {
        audioMixer.SetFloat("sfxVolume", Mathf.Log10(level)*20f);
    }

}

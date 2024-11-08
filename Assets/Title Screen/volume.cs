using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class volume : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;


    private void Start()
    {

        if (PlayerPrefs.HasKey("volume"))
        {
            loadVolume();
        }
        else
        {
            PlayerPrefs.SetFloat("volume", 1);
            loadVolume();
        }
    }
    public void setVolume()
    {
        AudioListener.volume = musicSlider.value;
    }

    public void saveVolume()
    {
        PlayerPrefs.SetFloat("volume", musicSlider.value);
    }

    public void loadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("volume");
    }
}

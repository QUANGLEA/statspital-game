using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicManager : MonoBehaviour
{
    private static MusicManager musicManagerInstance;

    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI volumeText;

    public AudioSource backgroundMusic;

    private void Start()
    {
        LoadValues();
    }

    // Check if the Music Player is currently playing something
    // If already then continue playing the music between scenes, if not destroy the object 
    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (musicManagerInstance == null)
        {
            musicManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void VolumnSlider(float volume)
    {
        volumeText.text = volume.ToString("0.0");
    }

    public void SaveVolumeButton()
    {
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadValues();
    }

    void LoadValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
        backgroundMusic.volume = volumeValue;
    }
}

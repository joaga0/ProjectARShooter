using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class tmp : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider audioSlider;

    public void AudioControl()
    {
        if (audioSlider == null)
        {
            Debug.LogError("audioSlider is not assigned!");
            return;
        }

        if (masterMixer == null)
        {
            Debug.LogError("masterMixer is not assigned!");
            return;
        }

        float sound = audioSlider.value;

        if (sound == -40f) masterMixer.SetFloat("BGM", -80);
        else masterMixer.SetFloat("BGM", sound);
    }

    public void ToggleAudioVolume()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (audioSlider != null)
        {
            audioSlider.onValueChanged.AddListener(delegate { AudioControl(); });
        }
        else
        {
            Debug.LogError("audioSlider is not assigned in Start!");
        }

        if (masterMixer == null)
        {
            Debug.LogError("masterMixer is not assigned in Start!");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

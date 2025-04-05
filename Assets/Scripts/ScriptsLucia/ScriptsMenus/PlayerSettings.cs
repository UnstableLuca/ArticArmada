using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sensibilidad;

    public GameObject PantallaAjustes;

    [SerializeField] private Slider sfxSlider;
    // Start is called before the first frame update
    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("Slidermusica");
        sensibilidad.value = PlayerPrefs.GetFloat("SliderSensibilidad", 2f);
        sfxSlider.value = PlayerPrefs.GetFloat("SliderSFX", 1f);
        SetSFXVolume();
    }
    public void SetVolumePref()
    {
        PlayerPrefs.SetFloat("Slidermusica", musicSlider.value);
    }
    public void SetSensibilidadPref()
    {
        PlayerPrefs.SetFloat("SliderSensibilidad", sensibilidad.value);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        PlayerPrefs.SetFloat("SliderSFX", volume);

        GameObject[] sfxObjects = GameObject.FindGameObjectsWithTag("SFX");
        foreach (var obj in sfxObjects)
        {
            AudioSource source = obj.GetComponent<AudioSource>();
            if (source != null)
            {
                source.volume = volume;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (PantallaAjustes.active))
        {
            PantallaAjustes.SetActive(false);
        }

    }
}

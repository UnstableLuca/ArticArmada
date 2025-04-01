using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ControlMusica : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioMixer audioMixer;

    public void ControldeMusica(float slidermusica)
    {
        audioMixer.SetFloat("VolumenMusica", Mathf.Log10(slidermusica) * 20);
    }

    public void Start()
    {
        audioMixer.SetFloat("VolumenMusica", 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    // Start is called before the first frame update
    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("Slidermusica");
    }
    public void SetVolumePref()
    {
        PlayerPrefs.SetFloat("Slidermusica", musicSlider.value);
    } 
   
}

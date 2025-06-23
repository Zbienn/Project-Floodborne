using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private GameObject boat;
    [SerializeField] private Sprite spriteA;
    [SerializeField] private Sprite spriteB;
    [SerializeField] private Sprite spriteC;
    [SerializeField] private Sprite spriteD;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = boat.GetComponentInChildren<SpriteRenderer>();

		float audioVolume = PlayerPrefs.GetFloat("AudioVolume", 0f);
        Slider slider = GetComponentInChildren<Slider>();
        slider.value = audioVolume;
    }

    public void ChangeBoatColor(int option)
    {
        sr.sprite = option switch
        {
            0 => spriteA,
            1 => spriteB,
            2 => spriteC,
            _ => spriteD,
        };
        PlayerPrefs.SetInt("BoatSprite", option);
        PlayerPrefs.Save();
    }

    public void SetVolume(float volume) 
    {
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("AudioVolume", volume);
        PlayerPrefs.Save();
    }
}

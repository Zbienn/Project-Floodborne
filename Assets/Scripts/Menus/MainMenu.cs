using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        if (!PlayerPrefs.HasKey("StatsArray"))
        {
            StatsForJSON[] array = 
            {
                new("MaxHealth", 0, 20, 20),
                new("Cooldown", 0, 90, 90),
                new("Area", 0, 30, 30),
                new("Speed", 0, 30, 30),
                new("Duration", 0, 30, 30),
                new("Amount", 0, 500, 500),
                new("MoveSpeed", 0, 30, 30),
                new("Magnet", 0, 30, 30),
                new("Luck", 0, 60, 60),
                new("Damage", 0, 20, 20)
            };
            PlayerPrefs.SetString("StatsArray", JsonHelper.ToJson(array));
            PlayerPrefs.Save();
        }
    }

    public void PlayGame() => SceneManager.LoadScene("Level1");

    public void OpenMainMenu() => SceneManager.LoadScene("MainMenu");

    public void ResetTime() => Time.timeScale = 1f;

    public void QuitGame()
    {
        Debug.Log("SAIR DO JOGO");
        Application.Quit();
    }
}


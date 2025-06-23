using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private int currentCoins;
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private GameObject[] upgrades;
    [SerializeField] private AudioClip[] audios;

    private AudioSource sfx;
    private StatsForJSON[] array;
    private int totalLvls;
    private PlayerMover playerMover;

    void Start()
    {
        sfx = GetComponent<AudioSource>();
        array = JsonHelper.FromJson<StatsForJSON>(PlayerPrefs.GetString("StatsArray", ""));
        currentCoins = PlayerPrefs.GetInt("Coins", 0);
        playerMover = FindFirstObjectByType<PlayerMover>();
        UpdateTexts();
    }

    public void BuyUpgrade(int index)
    {
        int amount = array[index].nextLvlCost;
        if (currentCoins - amount < 0)
        {
            sfx.clip = audios[1];
            sfx.Play();
            return;
        }

        currentCoins -= amount;
        array[index].level++;
        int extra = (int)Math.Round(2 * Math.Pow(1.3, totalLvls));

        foreach (var item in array)
            item.nextLvlCost = (item.baseCost * (item.level + 1)) + extra;

        PlayerPrefs.SetInt("Coins", currentCoins);
        PlayerPrefs.SetString("StatsArray", JsonHelper.ToJson(array));
        PlayerPrefs.Save();

        sfx.clip = audios[0];
        sfx.Play();

        UpdateTexts();

        if (index == 6)
            playerMover.MoveSpeed += array[index].level * 0.5f;
    }

    public void UpdateTexts()
    {
        totalLvls = 0;
        coinText.text = currentCoins.ToString();

        for (int i = 0; i < upgrades.Length; i++)
        {
            GameObject upgrade = upgrades[i];
            StatsForJSON info = array[i];

            if (info.level != 0)
                upgrade.GetComponentsInChildren<TMP_Text>()[1].text = "Lv. " + info.level;
            upgrade.GetComponentsInChildren<TMP_Text>()[2].text = info.nextLvlCost.ToString();

            totalLvls += info.level;
        }
    }

    public void ResetEverything()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainMenu");
    }
}

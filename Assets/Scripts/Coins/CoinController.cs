using UnityEngine;
using TMPro;

public class CoinController : MonoBehaviour
{
    [SerializeField] private int currentCoins;
    private TMP_Text coinText;
    private AudioSource sfx;

    public int CurrentCoins => currentCoins;

    private void Start()
    {
        currentCoins = PlayerPrefs.GetInt("Coins", 0);
        coinText = GetComponent<TMP_Text>();
        coinText.text = currentCoins.ToString();
        sfx = GetComponent<AudioSource>();
    }

    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
        coinText.text = currentCoins.ToString();
    }

    public void PlaySound(AudioClip audio)
    {
        sfx.clip = audio;
        sfx.Play();
    }

    public void SaveCoins()
    {
        PlayerPrefs.SetInt("Coins", currentCoins);
        PlayerPrefs.Save();
    }
}

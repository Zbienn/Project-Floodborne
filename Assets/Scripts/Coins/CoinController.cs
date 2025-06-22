using UnityEngine;
using TMPro;

public class CoinController : MonoBehaviour
{
    [SerializeField] private int currentCoins;
    private TMP_Text coinText;

    public int CurrentCoins => currentCoins;

    private void Start()
    {
        coinText = GetComponent<TMP_Text>();
    }

    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
        coinText.text = currentCoins.ToString();
    }

    public void SaveCoins()
    {
        int coins = int.Parse(PlayerPrefs.GetString("Coins", "0"));
        PlayerPrefs.SetString("Coins", (coins + currentCoins).ToString());
        PlayerPrefs.Save();
    }
}

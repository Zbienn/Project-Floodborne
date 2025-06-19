using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private int currentCoins;

    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
    }

}

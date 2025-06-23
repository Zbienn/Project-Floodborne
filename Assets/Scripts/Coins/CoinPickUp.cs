using System;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] private int coinAmount = 1;

    private bool movingToPlayer;
    private float pullSpeed = 10f;

    [SerializeField] private float timeBetweenChecks = 2f;
    [SerializeField] private float pickupRange;
    private float checkCounter;

    private PlayerMover player;
    private CoinController coinController;

    [SerializeField] private AudioClip sfx;

    void Start()
    {
        if (player == null) player = FindFirstObjectByType<PlayerMover>();
        if (coinController == null) coinController = FindFirstObjectByType<CoinController>();
        StatsForJSON magnetOffset = JsonHelper.FromJson<StatsForJSON>(PlayerPrefs.GetString("StatsArray", ""))[7];
        pullSpeed += magnetOffset.level * 0.5f;
    }

    void Update()
    {
        if(movingToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, pullSpeed * Time.deltaTime);
        } 
        else
        {
            checkCounter -= Time.deltaTime;
            if(checkCounter <= 0)
            {
                checkCounter = timeBetweenChecks;
                if(Vector3.Distance(transform.position, player.transform.position) < pickupRange)
                    movingToPlayer = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            coinController.AddCoins(coinAmount);
            coinController.PlaySound(sfx);
            Destroy(gameObject);
        }
    }
}

using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerHealthData healthData;
    [SerializeField] private Slider _healthUi;
    private bool isInvulnerable = false;
    [SerializeField] private float _iFrameDuration;

    [SerializeField] private AudioClip[] audios;
    private AudioSource sound;
    private CoinController coinController; 

    [SerializeField] private GameObject gameOver;

    void Awake()
    {
        sound = GetComponent<AudioSource>();
        coinController = FindFirstObjectByType<CoinController>();
        ResetHealth();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isInvulnerable) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {            
            int damage = collision.gameObject.GetComponent<EnemyScript>().DamageAmount;
            TakeDamage(damage);
            _healthUi.value = healthData.CurrentHealth;
            StartCoroutine(InvulnerabilityCoroutine());
        }
    }

    private void TakeDamage(int amount)
    {
        if (healthData.IsDead) return;

        healthData.CurrentHealth -= amount;

        if (healthData.IsDead)
        {
            sound.clip = audios[0];
            sound.Play();
            Die();
        }
        else
        {
            sound.clip = audios[1];
            sound.Play();
        }
    }

    private void ResetHealth() => healthData.CurrentHealth = healthData.MaxHealth;

    private void Die()
    {
        Debug.Log("Jogador morreu!");

        coinController.SaveCoins();

        Time.timeScale = 0f;
        gameOver.SetActive(true);
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(_iFrameDuration);
        isInvulnerable = false;
    }
}

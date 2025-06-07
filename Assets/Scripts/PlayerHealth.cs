using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerHealthData healthData;
    [SerializeField] private Slider _healthUi;
    private bool isInvulnerable = false;

    void Awake()
    {
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
            Die();
    }

    private void ResetHealth()
    {
        healthData.CurrentHealth = healthData.MaxHealth;
    }

    private void Die()
    {
        Debug.Log("Jogador morreu!");
        // Aqui colocas a lógica de game over ou respawn
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(1f);
        isInvulnerable = false;
    }
}

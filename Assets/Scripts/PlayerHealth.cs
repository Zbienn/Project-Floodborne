using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerHealthData healthData;

    void Awake()
    {
        ResetHealth();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                int damage = enemy.DamageAmount;
                TakeDamage(damage);
                Debug.Log($"Player levou {damage} de dano!");
            }
            else
            {
                Debug.LogWarning("Colidiu com Enemy mas não encontrou componente Enemy!");
            }
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
        // Lógica de fim de jogo aqui
    }
}

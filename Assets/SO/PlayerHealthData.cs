using UnityEngine;

[CreateAssetMenu(menuName = "Game/Player Health Data")]
public class PlayerHealthData : ScriptableObject
{
    [SerializeField] [Range(0,100)] private int maxHealth = 100;
    [SerializeField][Range(0, 100)] private int currentHealth;


    public int CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = Mathf.Clamp(value, 0, MaxHealth);
    }

    public bool IsDead => currentHealth <= 0;

    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
}

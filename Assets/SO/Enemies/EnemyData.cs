using UnityEngine;

[CreateAssetMenu(menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private int damageAmount;
    [SerializeField] private float speed;
    [SerializeField] private float health;

    public int DamageAmount
    {
        get => damageAmount;
        set => damageAmount = Mathf.Max(0, value);
    }
    public float Speed { get => speed; set => speed = value; }
    public float Health { get => health; set => health = value; }
}
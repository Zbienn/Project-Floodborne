using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsData", menuName = "Game/PlayerStatsData")]
public class PlayerStatsData : ScriptableObject
{
    [SerializeField] private float pickupRange;
    [SerializeField] private float weaponspeed;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float cooldown;
    [SerializeField] private float amount;
    [SerializeField] private float duration;
    public float PickupRange => pickupRange;

    public float WeaponSpeed => weaponspeed;
    public float Damage => damage;
    public float Range => range;
    public float Cooldown => cooldown;
    public float Amount => amount;
    public float Duration => duration;
}

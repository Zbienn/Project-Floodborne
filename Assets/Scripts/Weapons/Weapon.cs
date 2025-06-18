using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private List<WeaponStats> stats;
    [SerializeField] private int weaponLevel;
    private bool statsUpdated;

    public List<WeaponStats> Stats => stats;
    public int WeaponLevel => weaponLevel;
    public bool StatsUpdated { get => statsUpdated; set => statsUpdated = value; }

    public void LevelUp()
    {
        if (weaponLevel < stats.Count - 1)
        {
            weaponLevel++;
            statsUpdated = true;
        }
    }
}


[System.Serializable]
public class WeaponStats {
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private int amount;
    [SerializeField] private float duration;

    public float Speed { get => speed; set => speed = Mathf.Max(0, value); }
    public float Damage { get => damage; set => damage = Mathf.Max(0, value); }
    public float Range { get => range; set => range = Mathf.Max(0, value); }
    public float TimeBetweenAttacks { get => timeBetweenAttacks; set => timeBetweenAttacks = Mathf.Max(0, value); }
    public int Amount { get => amount; set => amount = Mathf.Max(0, value); }
    public float Duration { get => duration; set => duration = Mathf.Max(0, value); }

}
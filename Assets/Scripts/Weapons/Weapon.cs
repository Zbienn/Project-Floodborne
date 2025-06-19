using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private List<WeaponStats> stats;
    [SerializeField] private int weaponLevel;
    private bool statsUpdated;

    [SerializeField] private Sprite icon;


    public List<WeaponStats> Stats => stats;
    public int WeaponLevel => weaponLevel;
    public bool StatsUpdated { get => statsUpdated; set => statsUpdated = value; }
    public Sprite Icon { get => icon; }

    private PlayerWeapon playerWeapon;

    public void LevelUp()
    {
        if (weaponLevel < stats.Count - 1)
        {
            weaponLevel++;
            statsUpdated = true;

            if (weaponLevel >= stats.Count - 1) {
                if (playerWeapon == null)
                    playerWeapon = FindFirstObjectByType<PlayerWeapon>();

                if (playerWeapon != null)
                {
                    playerWeapon.FullyLeveledWeapons.Add(this);
                    playerWeapon.CurrentWeapons.Remove(this);
                }
                else
                {
                    Debug.LogWarning("PlayerWeapon não foi encontrado ao tentar remover a arma do inventário.");
                }
            }
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
    [SerializeField] private string upgradeText;

    public float Speed { get => speed; set => speed = Mathf.Max(0, value); }
    public float Damage { get => damage; set => damage = Mathf.Max(0, value); }
    public float Range { get => range; set => range = Mathf.Max(0, value); }
    public float TimeBetweenAttacks { get => timeBetweenAttacks; set => timeBetweenAttacks = Mathf.Max(0, value); }
    public int Amount { get => amount; set => amount = Mathf.Max(0, value); }
    public float Duration { get => duration; set => duration = Mathf.Max(0, value); }
    public string UpgradeText { get => upgradeText; set => upgradeText = value; }
}
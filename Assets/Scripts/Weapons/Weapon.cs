using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private int weaponLevel = 1;

    protected WeaponData.WeaponLevelStats currentStats;

    protected virtual void Start()
    {
        UpdateStats();
    }

    protected void UpdateStats()
    {
        currentStats = weaponData.GetStatsForLevel(weaponLevel);
    }

    public void LevelUp()
    {
        if (weaponLevel < weaponData.MaxLevel)
        {
            weaponLevel++;
            UpdateStats();
        }
    }
}

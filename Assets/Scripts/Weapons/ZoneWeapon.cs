using UnityEngine;

public class ZoneWeapon : Weapon
{
    [SerializeField] private EnemyDamager damager;
    private float spawnTime, spawnCounter;

    private void Start()
    {
        SetStats();
    }

    private void Update()
    {
        if (StatsUpdated == true)
        {
            StatsUpdated = false;
            SetStats();
        }

        spawnCounter -= Time.deltaTime;
        if(spawnCounter <= 0f)
        {
            spawnCounter = spawnTime;

            Instantiate(damager, damager.transform.position, Quaternion.identity, transform).gameObject.SetActive(true);
        }
    }

    void SetStats()
    {
        damager.BaseDamage = Stats[WeaponLevel].Damage;
        damager.Duration = Stats[WeaponLevel].Duration;
        damager.TimeBetweenDamage = Stats[WeaponLevel].Speed;
        damager.transform.localScale = Vector3.one * Stats[WeaponLevel].Range;
        spawnTime = Stats[WeaponLevel].TimeBetweenAttacks;
        spawnCounter = 0f;
    }

}

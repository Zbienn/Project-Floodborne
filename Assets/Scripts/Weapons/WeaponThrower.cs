using UnityEngine;

public class WeaponThrower : Weapon
{
    [SerializeField] private EnemyDamager damager;
    private float throwCounter;

    private AudioSource sfx;

    void Start()
    {
        sfx = GetComponent<AudioSource>();
        SetStats();
    }
    
    void Update()
    {
        if (StatsUpdated == true)
        {
            StatsUpdated = false;
            SetStats();
        }

        throwCounter -= Time.deltaTime;
        if (throwCounter <= 0) { 
            throwCounter = Stats[WeaponLevel].TimeBetweenAttacks;

            sfx.Play();
            for (int i = 0; i < Stats[WeaponLevel].Amount; i++) {
                Instantiate(damager, damager.transform.position, damager.transform.rotation).gameObject.SetActive(true);
            }
        }
    }

    void SetStats()
    {
        damager.BaseDamage = Stats[WeaponLevel].Damage;
        damager.Duration = Stats[WeaponLevel].Duration;
        damager.transform.localScale = Vector3.one * Stats[WeaponLevel].Range;
        
        throwCounter = 0f;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class WeaponThrower : Weapon
{
    [SerializeField] private EnemyDamager damager;
    private float throwCounter;

    private AudioSource sfx;
    private StatsForJSON[] array;
    private Dictionary<string, float> offsets;
    private int amountOffset = 0;

    void Start()
    {
        sfx = GetComponent<AudioSource>();
        array = JsonHelper.FromJson<StatsForJSON>(PlayerPrefs.GetString("StatsArray", ""));
        offsets = new Dictionary<string, float>()
        {
            { "Cooldown", array[1].level * 0.2f },
            { "Area", array[2].level * 0.5f },
            { "Speed", array[3].level * 0.2f },
            { "Duration", array[4].level * 0.3f },
            { "Damage", array[9].level * 0.5f }
        };
        amountOffset += array[5].level;
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
        Stats[WeaponLevel].TimeBetweenAttacks -= offsets["Cooldown"];
        damager.BaseDamage = Stats[WeaponLevel].Damage + offsets["Damage"];
        damager.Duration = Stats[WeaponLevel].Duration + offsets["Duration"];
        Stats[WeaponLevel].Range += offsets["Area"];
        damager.transform.localScale = Vector3.one * Stats[WeaponLevel].Range;
        Stats[WeaponLevel].Amount += amountOffset;
        throwCounter = 0f;
    }
}
